
namespace LivingMessiah.Features.Sukkot.Domain;

public class vwRegistration
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
	public int AttendanceTotal { get; set; }
	public int StepId { get; set; }
	public string? StepName => RegistrationSteps.Enums.Step.FromValue(StepId).Name;
	public int StepValue => RegistrationSteps.Enums.Step.FromValue(StepId);
	public decimal RegistrationFeeAdjusted { get; set; }
	public decimal LmmDonation { get; set; }
	public string? Notes { get; set; }
	public int AttendanceBitwise { get; set; }
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }
	public string? PayWithCheckMessage { get; set; }
	public string? HouseRulesAgreementDate { get; set; }
	public string FullName(bool includeOthers)
	{
		string? s = FirstName;
		if (!string.IsNullOrEmpty(SpouseName)) { s += " and " + SpouseName; }
		s += " " + FamilyName;
		if (includeOthers) { s += " and " + OtherNames; }
		return s;
	}
}
