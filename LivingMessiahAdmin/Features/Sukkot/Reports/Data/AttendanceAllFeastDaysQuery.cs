namespace LivingMessiahAdmin.Features.Sukkot.Reports.Data;

public class AttendanceAllFeastDaysQuery
{
	public int Id { get; set; }
	public string? FeastDay2 { get; set; }
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }
	public int TotalPeeps { get; set; }
}

