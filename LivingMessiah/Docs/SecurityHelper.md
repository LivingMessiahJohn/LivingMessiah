# Security Helper Design

I need helper / util class that can do many of the same security related issues for multiple Sukkot related scenarios (e.g. pages)

- replace `[CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }`	//ToDo: port to SecurityHelper

## Requirements

#### 1. Assumption: 

Any user out in the wild can go to a LMM Sukkot page, so it's the responsibility of the developer to make sure only the appropriate person can only view the appropriate data.

##### `@pages` in Sukkot

```html
@page "/Sukkot/Payment/{Id:int}"     <!-- Payment\Index.razor -->
@page "/Sukkot/PaymentCanceled"	     <!-- Payment\PaymentCanceled.razor -->
@page "/Sukkot/PaymentConfirm"       <!-- Payment\PaymentConfirm.razor -->
@page "/Sukkot/Print/{Id:int}/[fn1]" <!-- Print\Index.razor -->
@page "/Sukkot/RegistrationSteps"    <!-- RegistrationSteps\Index.razor -->

<!-- 
- C:\Source\repos\LivingMessiah\LivingMessiah\Features\Sukkot\
- fn1 {showPrintInstructionMessage:bool}
-->
```


#### 2. A user must be <mark>validated</mark>

```csharp
// This must return a non empty and non null value for an email
state.User.FindFirst(ClaimTypes.Email)?.Value;`
```

### 3. A user must be <mark>validated</mark>

## Sample Code to emulate
This example is from Sukkot/Print/Index.razor

- `IsAuthorized`

### `Index.razor`

```html
@page "/Sukkot/Print/{Id:int}/{showPrintInstructionMessage:bool}"

@using Microsoft.AspNetCore.Components.Authorization

<AuthorizeView Policy=@Auth0.Policy.Name>
  <Authorized>
    <LoadingComponent IsLoading="vwRegistration == null" TurnSpinnerOff=TurnSpinnerOff>

      @if (IsAuthorized)
      {
        <Details vwRegistration="@vwRegistration" />
      }
      else
      {
        <p class="fs-3 bg-danger text-center text-white mt-5 mx-5">NOT Authorized to view content</p>
      }
    </LoadingComponent>
  </Authorized>
</AuthorizeView>
```

#### `Program.cs`

```csharp
//...
  builder.Services.AddAuthorizationBuilder()
      .AddPolicy(Policy.Name, policy =>
        policy.RequireClaim(Policy.Claim, "true"));
//...

/*
public static class Policy
{
  public const string Name = "EmailVerified";
  public const string Claim = "email_verified";
}
*/
```

###  `Index.razor.cs`

#### `OnInitializedAsync`
- 1st, call `GetEmail` and if empty call `DoToastLog and then bail.

```csharp
	protected bool TurnSpinnerOff = false; // finally {	TurnSpinnerOff = true; }
	protected bool IsAuthorized = false;   // turned on in `DoPassed`

  private string[] OverrideRoles = 
    new[] { Auth0.Roles.Sukkot, Auth0.Roles.Admin };

  protected override async Task OnInitializedAsync()
  {
    try
    {
      string? email = await GetEmail();
      if (String.IsNullOrEmpty(email)) { DoToastLog("Email is empty");   }
      else
      {
        vwRegistration = await db!.ById(Id);

        if (vwRegistration is not null)
        {
          var (passed, errorMsg, securityOverride) = 
            await DoAuthentication(email!, vwRegistration!.EMail!, OverrideRoles);
          if (passed) { DoPassed(securityOverride);  }
          else { DoToastLog($"Failed DoAuthentication | {errorMsg}"); }
      // ...
```

#### `GetEmail`
```csharp
  private async Task<string?> GetEmail()
  {
    if (authenticationState is null) return string.Empty;

    var state = await authenticationState;
    if (state!.User.Identity is null) return string.Empty;

    return state.User.FindFirst(ClaimTypes.Email)?.Value;
  }
```

#### `DoAuthentication`
- Checks that `email` == `vwEmail`, if the same then Passed is true
- Checks if `OverrideRoles` _roles.Any 

```csharp
  private async Task<(bool Passed, string ErrorMsg, bool SecurityOverride)> DoAuthentication(string email, string vwEmail, string[] roles)
  {
    if (authenticationState is null) { return (false, "Authentication state is null.", false); }

    var state = await authenticationState;
    if (state!.User.Identity is null) { return (false, "User identity is null.", false); }

    if (email == vwEmail) { return (true, string.Empty, false); }

    string[] _roles = state.User.Claims
    .Where(c => c.Type == Auth0.MicrosoftSchemaIdentityClaimsRole)
    .Select(c => c.Value)
    .ToArray();

    return _roles.Any() ? (true, string.Empty, true) : (false, "Security override also failed", false);
  }
```


#### `DoToastLog`
```csharp
  private void DoToastLog(string message)
  {
    Logger!.LogWarning("{Method} {Message}", nameof(DoToastLog), message);
    Toast!.ShowWarning(message);
  }
```

#### `DoPassed`

```csharp
  private void DoPassed(bool securityOverride)
  {
    IsAuthorized = true;
    // Do post-authorized processing
  }
```

```html
  <NotAuthorized>
    <div class="card border-warning my-5">
      <div class="card-header">Not Authorized</div>
      <div class="card-body">
        <h5 class="">To view <b>@Page.Profile.Title</b> you need to be logged in.</h5>
        <LoginRedirectButton ReturnUrl="@Page.Profile.Index" />
      </div>
    </div>
  </NotAuthorized>
```  
