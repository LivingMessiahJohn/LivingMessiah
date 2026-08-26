namespace Admin.Features.Users.Data;

public sealed class UserListItem
{
	public required string UserId { get; init; }
	public string? Email { get; init; }
	public string? Name { get; init; }
	public bool? EmailVerified { get; init; }
	public string Roles { get; init; } = string.Empty;
	public DateTime? LastLogin { get; init; }
	public int? LoginsCount { get; init; }
	public DateTime? CreatedAt { get; init; }
	public bool? Blocked { get; init; }

	public string EmailOrEmpty => Email ?? string.Empty;
	public string NameOrEmpty => Name ?? string.Empty;
	public string EmailVerifiedDisplay => EmailVerified switch
	{
		true => "Yes",
		false => "No",
		null => string.Empty
	};
	public string BlockedDisplay => Blocked == true ? "Blocked" : string.Empty;
}
