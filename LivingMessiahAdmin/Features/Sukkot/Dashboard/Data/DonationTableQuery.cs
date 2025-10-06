namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;


public record DonationTableQuery(
	int Detail, 
	decimal Amount, 
	string Notes,	
	string? ReferenceId,	
	DateTime CreateDate
	, string? CreatedBy);
