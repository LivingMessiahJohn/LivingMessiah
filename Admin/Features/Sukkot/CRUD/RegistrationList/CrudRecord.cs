using Admin.Features.Sukkot.CRUD.RegistrationList.Enums;

namespace Admin.Features.Sukkot.CRUD.RegistrationList;

public struct CrudRecord
{
	public Crud Crud { get; set; }
	public string EMail { get; set; }
	public int Id { get; set; }
	public string FullName { get; set; }
}