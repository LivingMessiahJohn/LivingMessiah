using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Admin.Features.Users.Settings;
using Microsoft.Extensions.Options;

namespace Admin.Features.Users.Data;

public sealed class Auth0UserDirectory : IAuth0UserDirectory
{
	private const int MaxUsers = 20;
	public const string HttpClientName = "Auth0Management";

	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	private readonly IHttpClientFactory _httpClientFactory;
	private readonly Auth0M2M _settings;
	private readonly ILogger<Auth0UserDirectory> _logger;
	private readonly SemaphoreSlim _tokenLock = new(1, 1);
	private string? _accessToken;
	private DateTimeOffset _accessTokenExpiresAt;
	private int _rolesScopeWarningLogged;

	public Auth0UserDirectory(
		IHttpClientFactory httpClientFactory,
		IOptions<Auth0M2M> options,
		ILogger<Auth0UserDirectory> logger)
	{
		_httpClientFactory = httpClientFactory;
		_settings = options.Value;
		_logger = logger;
	}

	public async Task<IReadOnlyList<UserListItem>> GetUsersAsync(CancellationToken cancellationToken = default)
	{
		_logger.LogDebug("{Method}: fetching up to {MaxUsers} users by last_login desc", nameof(GetUsersAsync), MaxUsers);

		var users = await ListRecentUsersAsync(cancellationToken);
		var result = new List<UserListItem>(users.Count);

		foreach (var u in users)
		{
			var userId = u.UserId ?? string.Empty;
			var roleNames = string.IsNullOrWhiteSpace(userId)
				? []
				: await GetRoleNamesForUserAsync(userId, cancellationToken);

			var roles = roleNames.Count > 0
				? string.Join(", ", roleNames.OrderBy(r => r, StringComparer.OrdinalIgnoreCase))
				: string.Empty;

			result.Add(new UserListItem
			{
				UserId = userId,
				Email = u.Email,
				Name = u.Name ?? u.Nickname,
				EmailVerified = u.EmailVerified,
				Roles = roles,
				LastLogin = u.LastLogin,
				LoginsCount = u.LoginsCount,
				CreatedAt = u.CreatedAt,
				Blocked = u.Blocked
			});
		}

		return result;
	}

	private async Task<List<Auth0UserDto>> ListRecentUsersAsync(CancellationToken cancellationToken)
	{
		// include_totals=true returns { users: [...] }; false/default returns a raw array.
		// Parse both shapes — Auth0.ManagementApi v10 only accepts the object and throws on arrays.
		var path =
			$"users?page=0&per_page={MaxUsers}&include_totals=true&sort={Uri.EscapeDataString("last_login:-1")}";
		using var doc = await GetJsonAsync(path, cancellationToken);
		return ReadUserArray(doc.RootElement).Take(MaxUsers).ToList();
	}

	private async Task<List<string>> GetRoleNamesForUserAsync(string userId, CancellationToken cancellationToken)
	{
		var path = $"users/{Uri.EscapeDataString(userId)}/roles?per_page=50&include_totals=true";
		try
		{
			using var doc = await GetJsonAsync(path, cancellationToken);
			return ReadRoleNames(doc.RootElement);
		}
		catch (Auth0ManagementApiException ex) when (ex.StatusCode == 403)
		{
			// Users still load; Roles column stays empty until M2M scopes include read:roles (or read:role_members).
			if (Interlocked.Exchange(ref _rolesScopeWarningLogged, 1) == 0)
			{
				_logger.LogWarning(
					ex,
					"Auth0 M2M token lacks role scopes. In Auth0 Dashboard → Applications → APIs → Auth0 Management API → Machine to Machine, enable read:users and read:roles (or read:role_members), then restart Admin to refresh the cached token.");
			}

			return [];
		}
	}

	private async Task<JsonDocument> GetJsonAsync(string relativePath, CancellationToken cancellationToken)
	{
		var client = _httpClientFactory.CreateClient(HttpClientName);
		var token = await GetAccessTokenAsync(cancellationToken);

		using var request = new HttpRequestMessage(HttpMethod.Get, relativePath);
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

		using var response = await client.SendAsync(request, cancellationToken);
		var body = await response.Content.ReadAsStringAsync(cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError(
				"Auth0 Management API {Path} failed: {Status} {Body}",
				relativePath,
				(int)response.StatusCode,
				Truncate(body, 500));
			throw new Auth0ManagementApiException(
				(int)response.StatusCode,
				$"Auth0 Management API call failed ({(int)response.StatusCode}). Check Auth0M2M credentials and scopes (read:users and read:roles, or read:role_members).");
		}

		return JsonDocument.Parse(body);
	}

	private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
	{
		if (!string.IsNullOrEmpty(_accessToken) && DateTimeOffset.UtcNow < _accessTokenExpiresAt)
		{
			return _accessToken;
		}

		await _tokenLock.WaitAsync(cancellationToken);
		try
		{
			if (!string.IsNullOrEmpty(_accessToken) && DateTimeOffset.UtcNow < _accessTokenExpiresAt)
			{
				return _accessToken;
			}

			var domain = NormalizeDomain(_settings.Domain);
			var tokenClient = _httpClientFactory.CreateClient();
			using var tokenResponse = await tokenClient.PostAsync(
				$"https://{domain}/oauth/token",
				new FormUrlEncodedContent(new Dictionary<string, string>
				{
					["grant_type"] = "client_credentials",
					["client_id"] = _settings.ClientId!,
					["client_secret"] = _settings.ClientSecret!,
					["audience"] = $"https://{domain}/api/v2/"
				}),
				cancellationToken);

			var tokenBody = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
			if (!tokenResponse.IsSuccessStatusCode)
			{
				_logger.LogError(
					"Auth0 token request failed: {Status} {Body}",
					(int)tokenResponse.StatusCode,
					Truncate(tokenBody, 500));
				throw new InvalidOperationException(
					$"Auth0 M2M token request failed ({(int)tokenResponse.StatusCode}). Check Auth0M2M:Domain/ClientId/ClientSecret.");
			}

			var token = JsonSerializer.Deserialize<TokenResponse>(tokenBody, JsonOptions)
				?? throw new InvalidOperationException("Auth0 token response was empty.");

			if (string.IsNullOrWhiteSpace(token.AccessToken))
			{
				throw new InvalidOperationException("Auth0 token response did not include access_token.");
			}

			// Refresh one minute before expiry.
			var lifetime = TimeSpan.FromSeconds(Math.Max(60, token.ExpiresIn - 60));
			_accessToken = token.AccessToken;
			_accessTokenExpiresAt = DateTimeOffset.UtcNow.Add(lifetime);
			return _accessToken;
		}
		finally
		{
			_tokenLock.Release();
		}
	}

	private static List<Auth0UserDto> ReadUserArray(JsonElement root)
	{
		JsonElement usersElement = root.ValueKind switch
		{
			JsonValueKind.Array => root,
			JsonValueKind.Object when root.TryGetProperty("users", out var users) => users,
			_ => throw new InvalidOperationException(
				$"Unexpected Auth0 users payload (kind={root.ValueKind}). Expected array or object with 'users'.")
		};

		if (usersElement.ValueKind != JsonValueKind.Array)
		{
			throw new InvalidOperationException("Auth0 users payload 'users' was not an array.");
		}

		var list = new List<Auth0UserDto>();
		foreach (var item in usersElement.EnumerateArray())
		{
			var user = item.Deserialize<Auth0UserDto>(JsonOptions);
			if (user is not null)
			{
				list.Add(user);
			}
		}

		return list;
	}

	private static List<string> ReadRoleNames(JsonElement root)
	{
		JsonElement? rolesElement = null;

		if (root.ValueKind == JsonValueKind.Array)
		{
			rolesElement = root;
		}
		else if (root.ValueKind == JsonValueKind.Object)
		{
			if (root.TryGetProperty("roles", out var roles) && roles.ValueKind == JsonValueKind.Array)
			{
				rolesElement = roles;
			}
			else
			{
				foreach (var prop in root.EnumerateObject())
				{
					if (prop.Value.ValueKind == JsonValueKind.Array)
					{
						rolesElement = prop.Value;
						break;
					}
				}
			}
		}

		var names = new List<string>();
		if (rolesElement is not { ValueKind: JsonValueKind.Array } element)
		{
			return names;
		}

		foreach (var item in element.EnumerateArray())
		{
			if (item.TryGetProperty("name", out var name) && name.ValueKind == JsonValueKind.String)
			{
				var value = name.GetString();
				if (!string.IsNullOrWhiteSpace(value))
				{
					names.Add(value);
				}
			}
		}

		return names;
	}

	internal static string NormalizeDomain(string? domain)
	{
		if (string.IsNullOrWhiteSpace(domain))
		{
			throw new InvalidOperationException("Auth0M2M:Domain is missing.");
		}

		var d = domain.Trim();
		if (d.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
		{
			d = d["https://".Length..];
		}
		else if (d.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
		{
			d = d["http://".Length..];
		}

		return d.TrimEnd('/');
	}

	private static string Truncate(string value, int max) =>
		value.Length <= max ? value : value[..max] + "…";

	private sealed class TokenResponse
	{
		[JsonPropertyName("access_token")]
		public string? AccessToken { get; init; }

		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; init; } = 86000;
	}

	private sealed class Auth0UserDto
	{
		[JsonPropertyName("user_id")]
		public string? UserId { get; init; }

		[JsonPropertyName("email")]
		public string? Email { get; init; }

		[JsonPropertyName("name")]
		public string? Name { get; init; }

		[JsonPropertyName("nickname")]
		public string? Nickname { get; init; }

		[JsonPropertyName("email_verified")]
		public bool? EmailVerified { get; init; }

		[JsonPropertyName("last_login")]
		public DateTime? LastLogin { get; init; }

		[JsonPropertyName("logins_count")]
		public int? LoginsCount { get; init; }

		[JsonPropertyName("created_at")]
		public DateTime? CreatedAt { get; init; }

		[JsonPropertyName("blocked")]
		public bool? Blocked { get; init; }
	}
}

internal sealed class Auth0ManagementApiException : InvalidOperationException
{
	public Auth0ManagementApiException(int statusCode, string message) : base(message)
	{
		StatusCode = statusCode;
	}

	public int StatusCode { get; }
}
