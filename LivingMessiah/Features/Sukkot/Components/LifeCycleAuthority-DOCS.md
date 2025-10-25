
# LifeCycleAuthority-DOCS

If I was enforcing the phase "Registration Closed" for Sukkot, then these component would be used"

## `RegistrationClosed.razor`


```html
@using LivingMessiah.Features.Sukkot.Constants;

<div class="p-5 mb-4 bg-danger rounded-3">
	<div class="container-fluid py-5">
		<h1 class="display-5 fw-bold">Registration is CLOSED</h1>
		<h3>
			<span class="text-warning">
				If you would like to join us for Sukkot, please	contact @Constants.EmailClosed.Name at
				<a href="mailto:@Constants.EmailClosed.EmailTo?Subject=@Constants.EmailClosed.Subject"
					 target="_top">
					@Constants.EmailClosed.EmailTo
				</a> &nbsp;<i class="far fa-envelope"></i>.
			</span>
		</h3>
	</div>
</div>
```

## RegistrationMeta

If I were enforcing different registration fees and deadlines, I would use this code:
As of 2025-10-24, this is similar code is a reflection of the Sql Server Table `Sukkot.Constants`
In it's current state the business rule is that if there are two or more adults the fee is $100 per adult else (just 1 adult) it's $50

## `RegistrationMeta.cs`
```csharp
using LivingMessiah.Features.Sukkot.Constants;
namespace LivingMessiah.Features.Sukkot.LandingPage.Constants;

//ToDo: this only used by RegistrationFeeTableNOTUSING which is not used
public static class RegistrationMeta
{
	public static bool IsThereEarlyRegistration { get; set; } = false;
	public static System.DateTime EarlyRegistrationLastDay = new System.DateTime(Year.Int, 9, 16);
	public const decimal EarlyRegistrationFee = 75.0m;
	public static System.DateTime RegistrationLastDay = new System.DateTime(Year.Int, 10, 1);
	public const decimal RegistrationFee = 100.0m;
}
```

### `RegistrationFeeTableNOTUSING.razor`

```html

@* THIS IS NOT BEING USED *@

<div class="card-body">
	<h5 class="card-title mb-1">Registration Fee</h5>
	@if (Constants.RegistrationMeta.IsThereEarlyRegistration)
	{
		<dl class="row ms-1">
			<dt class="col-8">Early Registration Fee:</dt>
			<dd class="col-4"><span class="float-end">@Constants.RegistrationMeta.EarlyRegistrationFee.ToString("C0")</span></dd>
			<dt class="col-8">Early Registration Last Day:</dt>
			<dd class="col-4"><span class="float-end">@Constants.RegistrationMeta.EarlyRegistrationLastDay</span></dd>
		</dl>
	}
	<dl class="row ms-1">
		<dt class="col-8">Regular Registration Fee:</dt>
		<dd class="col-4"><span class="float-end">@Constants.RegistrationMeta.RegistrationFee.ToString("C0")</span></dd>
		<dt class="col-8">Registration Last Day:</dt>
		<dd class="col-4"><span class="float-end">@Constants.RegistrationMeta.RegistrationLastDay.ToShortDateString()</span></dd>
	</dl>
</div>
```