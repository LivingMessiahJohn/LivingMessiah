using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LivingMessiahAdmin.Data;

public abstract class BaseRepositoryAsync
{
	private readonly string ConnectionStringsKey;
	private readonly IConfiguration config;
	protected readonly ILogger Logger;
	protected BaseRepositoryAsync(IConfiguration config, ILogger<BaseRepositoryAsync> logger, string connectionStringsKey)
	{
		this.config = config;
		Logger = logger;
		ConnectionStringsKey = connectionStringsKey;
	}

	/// <summary>
	/// This method is responsible for ensuring that the connection is opened and closed safely and also ensures that we are always using an asynchronous connection.
	/// We open and close the connection with each method since SQL is going to manage our connection pooling and optimize this for us anyway
	/// We'll use a delegate here that matches a method that takes an argument of type IDbConnection and returns a Task of type T.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="getData">Delegate that matches a method that takes an argument of type IDbConnection and returns a Task of type T</param>
	/// <returns>Task of type T - we'll be using this to build and execute our query</returns>
	protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData)
	{
		string connectionString = config[ConnectionStringsKey]!;

		var match = Regex.Match(connectionString, @"Server\s*=\s*([^;]+)", RegexOptions.IgnoreCase);
		if (match.Success)
		{
			ServerId = match.Groups[1].Value;
		}
		else
		{
			ServerId = "N/A i.e. Test";
		}

		string errMsg = "";

		try
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				string err = $"Inside {GetType().FullName}.{nameof(WithConnectionAsync)}; Connection string is null or empty; ConnectionStringsKey={ConnectionStringsKey}";
				throw new ArgumentException(err);
			}

			using (var connect = new SqlConnection(connectionString))
			{
				await connect.OpenAsync();
				return await getData(connect);  // name of the `Func of T, see above
			}
		}
		catch (TimeoutException ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} experienced a Sql Timeout <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			Logger.LogError(ex, errMsg);
			throw new Exception(errMsg, ex);
		}

		catch (SqlException ex)
		{
			// Detect Azure SQL firewall / IP block errors and provide a clear, actionable message
			if (TryDetectAzureFirewallBlock(ex, out var clientIp))
			{
				errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} blocked by Azure SQL firewall; Server={ServerId}; ClientIP={(clientIp ?? "unknown")}. Sql...<br />[{SqlDump}] <br /><br />";
				Logger.LogError(ex, errMsg);
				// Throw a clear InvalidOperationException so callers can specifically detect a firewall/IP block if desired
				throw new InvalidOperationException(errMsg, ex);
			}

			errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} experienced a Sql Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			Logger.LogError(ex, errMsg);
			if (ex.Message != null) { errMsg += "<br /> ex.Message:" + ex.Message; }
			throw new Exception(errMsg, ex);
		}
		catch (InvalidOperationException ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} experienced a Invalid Operation Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			Logger.LogError(ex, errMsg);
			throw new Exception(errMsg, ex);
		}
		catch (Exception ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} Generic Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			Logger.LogError(ex, errMsg);
			throw new Exception(errMsg, ex);
		}
	}

	/// <summary>
	/// Tries to detect an Azure SQL firewall/IP block from the SqlException.
	/// Returns true and the extracted client IP when detected.
	/// Detection uses:
	///  - the Azure firewall message pattern ("Client with IP address 'x.x.x.x' is not allowed to access the server")
	///  - a fallback search for "not allowed to access the server"
	///  - known Azure SQL error number 40615 (best-effort)
	/// </summary>
	private static bool TryDetectAzureFirewallBlock(SqlException? ex, out string? clientIp)
	{
		clientIp = null;
		if (ex == null) return false;

		// 1) Exact Azure firewall message with quoted IP
		var m = Regex.Match(ex.Message ?? 
			string.Empty, @"Client with IP address\s*'(?<ip>[\d\.]+)'\s*is not allowed to access the server", RegexOptions.IgnoreCase);
		if (m.Success)
		{
			clientIp = m.Groups["ip"].Value;
			return true;
		}

		// 2) Fallback: message contains both phrases; try to extract any IPv4
		var msg = ex.Message ?? string.Empty;
		if (msg.IndexOf("not allowed to access the server", StringComparison.OrdinalIgnoreCase) >= 0 &&
			msg.IndexOf("Client with IP address", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			var ipMatch = Regex.Match(msg, @"(?<ip>\d{1,3}(?:\.\d{1,3}){3})");
			if (ipMatch.Success) clientIp = ipMatch.Groups["ip"].Value;
			return true;
		}

		// 3) Known Azure SQL error number (best-effort; provider may vary)
		if (ex.Number == 40615) return true;

		return false;
	}

	public string? Sql { get; set; }
	public DynamicParameters? Parms { get; set; }  // using Dapper; Note, only place dependent on Dapper
	private string? ServerId { get; set; }

	public string? SqlDump
	{
		get
		{
			string s = "";
			s = Sql ?? "SQL IS NULL";
			if (Parms != null)
			{
				/* See Notes in LivingMessiahAdmin.Data!BaseRepositoryAsync*/
			}
			return s;
		}
	}

	public string GetServerId()
	{
		return ServerId!;
	}

}
// Ignore Spelling: parms