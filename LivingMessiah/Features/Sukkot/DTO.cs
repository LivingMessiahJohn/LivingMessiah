namespace LivingMessiah.Features.Sukkot;

public record DTO
{
	public int Id { get; set; }
	public string? FamilyName { get; set; }
	public string? FirstName { get; set; }
	public string? SpouseName { get; set; }
	public string? OtherNames { get; set; }
	public string? EMail { get; set; }
	public string? Phone { get; set; }
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }

	public int StatusId { get; set; } // ToDo Convert to StepId

	public int AttendanceBitwise { get; set; }

	public string? Notes { get; set; }
	public string? Avatar { get; set; }
	public decimal LmmDonation { get; set; }

}
