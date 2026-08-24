namespace Admin.Features.Sukkot.CRUD.Donations;

public class DonationVM
{
	public decimal Amount { get; set; }
	public string? Notes { get; set; }
	public string? ReferenceId { get; set; }
}