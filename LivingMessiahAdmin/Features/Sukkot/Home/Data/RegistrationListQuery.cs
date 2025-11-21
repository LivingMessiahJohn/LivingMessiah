using LivingMessiahAdmin.Helpers;

using StepEnums = RCL.Features.Sukkot.Enums.Step;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Data;

public class RegistrationListQuery
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }

	public string FullNameOrNA
	{
		get
		{
			return StatusId == StepEnums.Registration.Value ? "N/A" : FullName!;
		}
	}

	public string FullNameOrNAColor
	{
		get
		{
			return StatusId == StepEnums.Registration.Value ? "bg-secondary-subtle text-black" : ""; // text-center
		}
	}

	public int StatusId { get; set; }

	public string StatusName
	{
		get
		{
			return $"{StepEnums.FromValue(StatusId).Value}. {StepEnums.FromValue(StatusId).Heading}"; // .Text
		}
	}

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

	private decimal GetRegistrationFee()
	{
		return Adults == 1 ? 50.0m : 100.0m;
	}

	public decimal TotalDonation { get; set; }

	private string GetTotalDonationFormatted(decimal amount)
	{
		return string.Format("{0:C2}", TotalDonation - amount);
	}

	public string TotalDonationNoCents
	{
		get
		{
			if (StatusId == StepEnums.Registration.Value) return "N/A";

			return Adults == 1
			 ? TotalDonation == 50.0m ? "✓" : GetTotalDonationFormatted(-50.0m)
			 : TotalDonation == 100.0m ? "✓" : GetTotalDonationFormatted(-100.0m);
		}
	}

	public string TotalDonationClass
	{
		get
		{
			if (StatusId == StepEnums.Registration.Value)
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
			if (StatusId == StepEnums.Registration.Value) return "badge bg-secondary text-white";

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
	public string? AdminNotes { get; set; }

	public string AdminNotesShort
	{
		get
		{
			return string.IsNullOrEmpty(AdminNotes) == true ? "" : AdminNotes!.Truncate(25);
		}
	}

	public int IdHra { get; set; }
}
