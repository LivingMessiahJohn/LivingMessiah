
using LivingMessiahAdmin.Features.Sukkot.Enums;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Registrant;

public class FormVM
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

	public int StatusId { get; set; } // The ManageRegistration!EntryForm needs this
	public Step? Status { get; set; }  // Status? Status

	public int AttendanceBitwise { get; set; } // does the VM need this?
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }

	public string? Notes { get; set; }
	public string? AdminNotes { get; set; }
	public bool DidNotAttend { get; set; }
	public decimal LmmDonation { get; set; }
}
