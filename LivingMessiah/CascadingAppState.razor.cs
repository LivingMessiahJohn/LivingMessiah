using Microsoft.AspNetCore.Components;
using LivingMessiah.State;

namespace LivingMessiah
{
	public partial class CascadingAppState
	{
		[Inject] public AppState? AppState { get; set; }
		[Parameter] public RenderFragment? ChildContent { get; set; }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await AppState!.Initialize();
			}
		}
	}
}