using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Features.Liturgy
{
	public partial class Index
	{
		[CascadingParameter] public CascadingAppState? State { get; set; }

		protected Enums.Content? CurrentContent;
		protected int Section = 1;
		protected bool ShowImages = false;

		protected override void OnInitialized()
		{
			//CurrentContent = Enums.Content.List.FirstOrDefault(w => w.Value == Section);
			CurrentContent = GetContent();
		}

		private Enums.Content GetContent()
		{
			int id = State!.AppState!.LiturgyState!.Get();
			return Enums.Content.TryFromValue(id, out _) 
			? Enums.Content.FromValue(id) 
			: Enums.Content.CallToService;
		}

		private async Task ReturnedSection(int section)
		{
			Section = section;
			CurrentContent = Enums.Content.List.FirstOrDefault(w => w.Value == Section);

			await State!.AppState!.LiturgyState!.UpdateContent(section);
		}

		protected Enums.ModalMenuItem? CurrentModalToShow { get; set; }
		private void ReturnedModalMenuItem(Enums.ModalMenuItem modalMenuItem)
		{
			CurrentModalToShow = modalMenuItem;
			if (CurrentModalToShow == Enums.ModalMenuItem.ShowImages) { ShowImages = !ShowImages; }
		}

		private void ReturnedCloseEvent()
		{
			CurrentModalToShow = null;
			//Logger!.LogInformation("{Method}, CurrentModalToShow set to null", nameof(ReturnedCloseEvent));
		}
	}
}