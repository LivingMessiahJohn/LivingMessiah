using LivingMessiahAdmin.Helpers;
using LivingMessiahAdmin.Features.Sukkot.RegistrationSteps.Enums;  //Ref. was from LivingMessiah
using System;

namespace LivingMessiahAdmin.Features.Sukkot.ManageRegistration.MasterDetail;

public class ManageRegistrationQuery
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }

	public string FullNameOrNA
	{
		get
		{
			return StatusId == Step.Registration.Value ? "N/A" : FullName!;
		}
	}

	public string FullNameOrNAColor
	{
		get
		{
			return StatusId == Step.Registration.Value ? "bg-secondary text-center text-white" : "";
		}
	}

	public int StatusId { get; set; }

	public string StatusName
	{
		get
		{
			return $"{Step.FromValue(StatusId).Value}. {Step.FromValue(StatusId).Text}";
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
			if (StatusId == Step.Registration.Value) return "N/A";

			return Adults == 1
			 ? TotalDonation == 50.0m ? "✓" : GetTotalDonationFormatted(-50.0m)
			 : TotalDonation == 100.0m ? "✓" : GetTotalDonationFormatted(-100.0m);
		}
	}

	public string TotalDonationClass
	{
		get
		{
			if (StatusId == Step.Registration.Value)
			{
				return "bg-secondary text-center text-white";
			}
			else
			{
				if (GetRegistrationFee() == TotalDonation)
				{
					return "bg-success text-center text-white";
				}
				else
				{
					if (TotalDonation > GetRegistrationFee())
					{
						return "bg-primary text-end text-white";
					}
					else
					{
						return "bg-danger text-end text-white";
					}
				}
			}
		}
	}

	public string TotalDonationBadgeCSS
	{
		get
		{
			if (StatusId == Step.Registration.Value) return "badge bg-secondary text-white";

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
			return String.IsNullOrEmpty(AdminNotes) == true ? "" : AdminNotes!.Truncate(25);
		}
	}

	public int IdHra { get; set; }

}
