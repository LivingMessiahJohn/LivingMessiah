using System.ComponentModel.DataAnnotations;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Agreement;

public class AgreementEditFormVM
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email address")]
	public string? Email { get; set; }
}
