using Admin.Features.Sukkot.Home.RegistrationList.Enums;

namespace Admin.Features.Sukkot.Home.RegistrationList;

public struct CrudRecord
{
	public Crud Crud { get; set; }
	public string EMail { get; set; }
	public int Id { get; set; }
	public string FullName { get; set; }
}