namespace LivingMessiahAdmin.Features.Sukkot.MasterDetail;

public struct CrudAndIdArgs
{
	public Enums.Crud Crud { get; set; }
	public string EMail { get; set; }
	public int Id { get; set; }
	public string FullName { get; set; }
}