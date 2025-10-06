namespace LivingMessiahAdmin.Features.Sukkot.Home.Data;

/* 
- Called only by `Report.razor` `OnParametersSetAsync`
- LivingMessiahAdmin\Features\Sukkot\Home\RegistrationDetail\Report.razor
*/
public record DonationQuery(decimal Amount,	string? ReferenceId,	DateTime CreateDate);
