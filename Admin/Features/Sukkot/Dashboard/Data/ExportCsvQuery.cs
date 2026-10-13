using StepEnums = RCL.Features.Sukkot.Enums.Step;

namespace Admin.Features.Sukkot.Dashboard.Data;

public class ExportCsvQuery
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }
	public string? OtherNames { get; set; }
	public int StepId { get; set; }
	public string? StepDescription { get; set; }
	public bool DidNotAttend { get; set; }
	public int Adults { get; set; }
	public int Children { get; set; }
	public decimal Donation { get; set; }
	public string? Phone { get; set; }
	public string? Notes { get; set; }
	public string? AdminNotes { get; set; }
	public int AttendanceBitwise { get; set; }
	public string? HraAcceptedDate { get; set; }

	public static ExportCsvQuery From(GridQuery item, string? hraAcceptedDate)
	{
		return new ExportCsvQuery
		{
			Id = item.Id,
			EMail = item.EMail,
			FullName = item.FullName,
			OtherNames = item.OtherNames,
			StepId = item.StepId,
			StepDescription = DescribeStep(item.StepId),
			DidNotAttend = item.DidNotAttend,
			Adults = item.Adults,
			Children = item.Children,
			Donation = item.TotalDonation,
			Phone = item.Phone,
			Notes = item.Notes,
			AdminNotes = item.AdminNotes,
			AttendanceBitwise = item.AttendanceBitwise,
			HraAcceptedDate = hraAcceptedDate
		};
	}

	private static string DescribeStep(int stepId)
	{
		return StepEnums.TryFromValue(stepId, out var step) ? step.Text : stepId.ToString();
	}
}
