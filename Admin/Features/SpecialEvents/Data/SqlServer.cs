namespace Admin.Features.SpecialEvents.Data;

// This technique is not used by Sukkot
// ToDo: What's the best way to handle return values from Sql Server?
public static class SqlServer
{
	public const int ReturnValueOk = 0;
	public const int ReturnValueViolationInUniqueIndex = 2601;
	public const string ReturnValueName = "ReturnValue";
	public const string ReturnValueParm = "@ReturnValue";
}

// Ignore Spelling: Parm

/*
use master
select * from sysmessages
*/