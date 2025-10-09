namespace LivingMessiahAdmin.Features.Sukkot.Home.Donations.Data;

public record DonationRecord (
//int Id, 
int RegistrationId, 
//int Detail, 
decimal Amount, 
string? Notes,
string? ReferenceId, 
string? CreatedBy, 
DateTime CreateDate,
string? Email);

