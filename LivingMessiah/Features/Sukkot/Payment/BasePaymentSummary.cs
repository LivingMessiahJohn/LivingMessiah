using LivingMessiah.Features.Sukkot.Domain;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Features.Sukkot;

// ToDo: this abstraction seems unnecessary
public abstract class BasePaymentSummary : ComponentBase
{
	[Parameter] public RegistrationSummary? RegistrationSummary { get; set; }
}
