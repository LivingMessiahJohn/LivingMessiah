namespace LivingMessiah.Features.Sukkot.NormalUser;

public class EntryFormVM
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

	public int StepId { get; set; } // The ManageRegistration!EntryForm needs this
	public RegistrationSteps.Enums.Step? Step { get; set; }

	public int AttendanceBitwise { get; set; } // does the VM need this?
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }

	public string? Notes { get; set; }
	public string? Avatar { get; set; }
	public Decimal LmmDonation { get; set; }

	/* 
	Logger!.LogDebug("{Method}, AttendanceDates: {VM.AttendanceDates} AttendanceDates2ndMonth: {VM.DumpAttendanceDates2ndMonth}"
	  , nameof(Dump), VM.DumpAttendanceDates, VM.DumpAttendanceDates2ndMonth});
	*/
	public string DumpAttendanceDates
	{
		get
		{
		
			return this.AttendanceDateList is not null ?
				String.Join(", ", this.AttendanceDateList.Select(date => date.ToString(DateFormat.YYYY_MM_DD)))
				: "";
		}
	}

	public string DumpAttendanceDates2ndMonth
	{
		get
		{
			
			return this.AttendanceDateList2ndMonth is not null ?
				String.Join(", ", this.AttendanceDateList2ndMonth.Select(date => date.ToString(DateFormat.YYYY_MM_DD)))
				: "";
		}
	}

}
