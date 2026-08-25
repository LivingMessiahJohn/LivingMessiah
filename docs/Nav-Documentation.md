# Nav Documentation

## `Nav.cs`
This is the name of the file that defines all navigation for a project

It uses `Ardalis.SmartEnum`, here is a template that uses `region`s which are used in all my `SmartEnum`s

```cs
using Ardalis.SmartEnum
using RoleEnum = Admin.Security.Enums.Role;

public abstract class Nav : SmartEnum<Nav>

	#region Id's
	private static class Id
	{
  internal const int Home = 1;    
  //...
	}
	#endregion

	#region Declared Public Instances
	public static readonly Nav Home = new HomeSE();
  //...
	#endregion

	private Nav(string name, int value) : base(name, value)	{	}   // Constructor

	#region Extra Fields
    // Common fields
  	public abstract string Index { get; }
    public abstract string Title { get; }
    public abstract string Icon { get; }

		public override int Parent => 0; // used to help define hierarchy for e.g. Home\NavTree.razor

    public abstract int RequiredRoles { get; } // The Id/Value of `Enums/Role.cs` which uses bitwise logic
//...

	#endregion  
//...

	#region Private Instantiation

	private sealed class HomeSE : Nav
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Home";
		public override string Icon => "fas fa-home";

		public override int Parent => 0; // ToDo: give another example 
	
  	public override int RequiredRoles => 0; // No specific role required for Home
    /*
    Here's another example where multiple roles are assigned
    public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
    */
    
	}
 	
  #endregion
```

### Location
for each application, It's scope is global and `Nav.cs` in the Enums folder (e.g. for `Admin` it's in `Admin\Enums\Nav.cs`)

### Hierarchy (`Parent`)
Admin uses a single `Nav` SmartEnum (no separate `NavGroup`). Hierarchy is expressed with `Parent`:

- `Parent == 0` — top-level roots used outside the tree (`Home`, `Profile`)
- `Parent == Nav.Home.Value` — Home sitemap children (WeeklyDownload, Sukkot, Alerts, …)
- Nested leaves set `Parent` to their folder (`Sukkot`, `Alerts`, `HealthChecks`)

`Nav.IsFolder` is true when any other `Nav` has `Parent == this.Value`. Folders often use an empty `Index` (expand/collapse only in `NavTree`). Sukkot leaf routes delegate to `Admin.Features.Sukkot.Home.Enums.Tab` (`/SukkotHome/{TabName}`).

### Security
For those apps that have authentication and authorization it manages 

## Features
Because it's declarative and centralized the use of LINQ makes it easy to do things ...
1. populate a NavTree.razor component (Admin builds the tree from `Nav.List` by `Parent` — no handwritten `PopulateTree`)
2. populate a Sitemap.razor component
3. populate Layout navbar dropdown menu component
4. Help AI document the flow of the app

### referenced by components
#### 1. `PageHeader.razor` 
to populate `<PageTitle>` with Nav.Title `[Parameter, EditorRequired] public Nav PageEnum { get; set; } = Nav.Home;`

#### 2. `LoginRedirectButton.razor`
used by global `LoginRedirectCard.razor` that redirects the user to the page they're on not just to home

```html
@using AccountENum = Admin.Enums.Account;
@inject NavigationManager NavigationManager

<button @onclick="@(() => RedirectToLoginClick())"
				type="button" class="btn btn-primary btn-sm">
	@AccountENum.Login.Title <i class='@AccountENum.Login.Icon'></i>
</button>
@code 
```

```cs
{
	[Parameter] public string? ReturnUrl { get; set; }
	void RedirectToLoginClick()
	{
		NavigationManager!.NavigateTo($"{AccountENum.Login.Index}?returnUrl={ReturnUrl}", true);
	}
}
```   
