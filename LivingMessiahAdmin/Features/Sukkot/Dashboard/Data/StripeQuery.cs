namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;

public class StripeQuery
{
	public int Id { get; set; }
	public string Email { get; set; } = default!;
	public int RegistrationId { get; set; }
	public int ModificationCount { get; set; }
	public DateTime LastModifiedDate { get; set; }
	public string? FirstName { get; set; }
	public string? FamilyName { get; set; }
}