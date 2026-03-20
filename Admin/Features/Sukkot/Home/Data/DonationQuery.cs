namespace Admin.Features.Sukkot.Home.Data;

/* 
- Called only by `Report.razor` `OnParametersSetAsync`
- Admin\Features\Sukkot\Home\RegistrationDetail\Report.razor
*/
public record DonationQuery(decimal Amount,	string? ReferenceId,	DateTime CreateDate);
