namespace LivingMessiah.Features.Sukkot.Domain;

public class Donation
{
	public int RegistrationId { get; set; }
	public int Detail { get; set; }
	public decimal Amount { get; set; }
	public string? Notes { get; set; }
	public string? Email { get; set; } // DonorEmail
	public string? ReferenceId { get; set; } // StripePaymentIntentId { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime CreateDate { get; set; }
}
