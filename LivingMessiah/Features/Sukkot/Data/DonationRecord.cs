namespace LivingMessiah.Features.Sukkot.Data;

public class DonationRecord
{
	public int RegistrationId { get; set; } 
	public decimal Amount { get; set; }
	public string? Notes { get; set; }
	public string? Email { get; set; }
	public string? ReferenceId { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime CreateDate { get; set; }
}
