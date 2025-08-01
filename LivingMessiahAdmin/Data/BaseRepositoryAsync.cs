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

			//Obsolete, 'Use the Microsoft.Data.SqlClient package instead.'
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

		//Obsolete, 'Use the Microsoft.Data.SqlClient package instead.'
		catch (SqlException ex)
		{
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
				/* See Notes in LivingMessiah.Data!BaseRepositoryAsync*/
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