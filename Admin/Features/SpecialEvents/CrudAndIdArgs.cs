namespace Admin.Features.SpecialEvents;

public struct CrudAndIdArgs
{
	public Enums.Crud Crud { get; set; }
	public int Id { get; set; }
	public string Title { get; set; }
}