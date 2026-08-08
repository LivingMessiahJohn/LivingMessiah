namespace RCL.Features.Sukkot;

/// <summary>
/// Host apps (Admin, Sukkot) implement this with their own repository/SQL.
/// RCL only depends on the abstraction — no DB access in the library.
/// </summary>
public interface IScheduleQueryLoader
{
	Task<ScheduleQuery?> GetAsync();
}
