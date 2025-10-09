using LivingMessiahAdmin.Helpers;

using StepEnums = LivingMessiahAdmin.Features.Sukkot.Enums.Step;
using Attendance = LivingMessiahAdmin.Features.Sukkot.Enums.AttendanceDate;

namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;

public class GridQuery
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }

	public string FullNameOrNA
	{
		get
		{
			return StepId == StepEnums.Registration.Value ? "N/A" : FullName!;
		}
	}

	public string FullNameOrNAColor
	{
		get
		{
			return StepId == StepEnums.Registration.Value ? "bg-secondary-subtle text-black" : ""; // text-center
		}
	}

	public string? OtherNames { get; set; }

	//ToDo: rename to StepId or get rid of it and rename Step to StepId
	public int StepId { get; set; }

	//public string StepName => $"{StepEnums.FromValue(StatusId).Value}. {StepEnums.FromValue(StatusId).Heading}";

	public bool DidNotAttend { get; set; }
	public string NoShow
	{
		get
		{
			return DidNotAttend ? "✓" : "";
		}
	}

	public int Adults { get; set; }
	public int Children { get; set; }

	public string People => $"{Adults}-{Children}";


	private decimal GetRegistrationFee()
	{
		return Adults == 1 ? 50.0m : 100.0m;
	}

	public decimal TotalDonation { get; set; }

	private string GetTotalDonationFormatted(decimal amount)
	{
		return string.Format("{0:C0}", TotalDonation - amount);
	}

	public string StepHeading => StepEnums.FromValue(StepId).Heading;

	public string TotalDonationNoCents
	{
		get
		{
			if (StepId == StepEnums.Registration.Value) return "N/A";

			return Adults == 1
			 ? TotalDonation == 50.0m ? "✓" : GetTotalDonationFormatted(-50.0m)
			 : TotalDonation == 100.0m ? "✓" : GetTotalDonationFormatted(-100.0m);
		}
	}

	public string TotalDonationClass
	{
		get
		{
			if (StepId == StepEnums.Registration.Value)
			{
				return "bg-secondary-subtle text-center text-black";
			}
			else
			{
				if (GetRegistrationFee() == TotalDonation)
				{
					return "bg-success-subtle text-center text-black";
				}
				else
				{
					if (TotalDonation > GetRegistrationFee())
					{
						return "bg-primary text-end text-white";
					}
					else
					{
						return "bg-danger-subtle text-end text-black";
					}
				}
			}
		}
	}

	public string TotalDonationBadgeCSS
	{
		get
		{
			if (StepId == StepEnums.Registration.Value) return "badge bg-secondary text-white";

			if (GetRegistrationFee() == TotalDonation)
			{
				return "badge bg-success text-white";
			}
			else
			{
				if (TotalDonation > GetRegistrationFee())
				{
					return "badge bg-primary text-white";
				}
				else
				{
					return "badge bg-danger text-white";
				}
			}
		}
	}

	public int DonationRowCount { get; set; }

	public string? Phone { get; set; }

	public string? Notes { get; set; }
	public bool HasNotes => !string.IsNullOrEmpty(Notes);

	public string? AdminNotes { get; set; }
	public bool HasAdminNotes => !string.IsNullOrEmpty(AdminNotes);

	public int AttendanceBitwise { get; set; }
	public string AttendanceColumnValue
	{
		get
		{
			return LivingMessiahAdmin.Features.Sukkot.Enums.Helper.GetAttendanceDatesColumnValue(AttendanceBitwise);
		}
	}

	/*
	This information is gotten from LivingMessiahAdmin\Features\Sukkot\Dashboard\Constants\GridHelper.cs
	public string AttendanceColumnHeader
	{
		get
		{
			return LivingMessiahAdmin.Features.Sukkot.Enums.Helper.GetAttendanceDatesColumnHeader(AttendanceBitwise);
		}
	}
	*/


	/*
	public string AdminNotesShort
	{
		get
		{
			return string.IsNullOrEmpty(AdminNotes) == true ? "" : AdminNotes!.Truncate(25);
		}
	}
	*/

	public int IdHra { get; set; }
}
