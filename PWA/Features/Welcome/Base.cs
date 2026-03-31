using Microsoft.AspNetCore.Components;

namespace PWA.Features.Welcome;

public abstract class Base : ComponentBase
{
	[Parameter, EditorRequired] public bool IsXs { get; set; }
	protected string Center => IsXs ? " text-center" : "";
}
