namespace Sukkot.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LandingPageTests : SukkotPageTest
{
	[Test]
	public async Task HasTitleAndHeading()
	{
		await OpenAsync("/");
		await Expect(Page).ToHaveTitleAsync("Sukkot");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Sukkot", Level = 1 }))
			.ToBeVisibleAsync();
	}

	[Test]
	public async Task ShowsBannerDatesAndFees()
	{
		await OpenAsync("/");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Celebrating Sukkot 2026" }))
			.ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "From: September 25th" }))
			.ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "To: October 3rd" }))
			.ToBeVisibleAsync();
		await Expect(Page.GetByText("Family")).ToBeVisibleAsync();
		await Expect(Page.GetByText("$100")).ToBeVisibleAsync();
		await Expect(Page.GetByText("Single: $50")).ToBeVisibleAsync();
	}

	[Test]
	public async Task ShowsDocumentPacketLinks()
	{
		await OpenAsync("/");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Document Packet" }))
			.ToBeVisibleAsync();

		var waiver = Page.GetByRole(AriaRole.Link, new() { Name = "Liability Waiver" });
		var houseRules = Page.GetByRole(AriaRole.Link, new() { Name = "Sukkot 2026 House Rules" });
		var pocketMod = Page.GetByRole(AriaRole.Link, new() { Name = "PocketMod Scheduled Events" });

		await Expect(waiver).ToBeVisibleAsync();
		await Expect(houseRules).ToBeVisibleAsync();
		await Expect(pocketMod).ToBeVisibleAsync();

		await Expect(waiver).ToHaveAttributeAsync(
			"href",
			"https://livingmessiahstorage.blob.core.windows.net/sukkot-content/liability-waiver.pdf");
		await Expect(houseRules).ToHaveAttributeAsync(
			"href",
			"https://livingmessiahstorage.blob.core.windows.net/sukkot-content/sukkot-2026-house-rules.pdf");
		await Expect(pocketMod).ToHaveAttributeAsync(
			"href",
			"https://livingmessiahstorage.blob.core.windows.net/sukkot-content/scheduled-events-pocketmod.pdf");
	}

	[Test]
	public async Task ExpandsRegistrationWalkThrough()
	{
		await OpenAsync("/");
		await Page.GetByRole(AriaRole.Button, new() { Name = "Registration Walk Through Guide" })
			.ClickAsync();
		await Expect(Page.GetByText("Sign Up for an account or use one previously created."))
			.ToBeVisibleAsync();
	}

	[Test]
	public async Task AdvancesScheduleToNextDay()
	{
		await OpenAsync("/");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Schedule" }))
			.ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Heading, new() { NameRegex = new Regex("^Day 0") }))
			.ToBeVisibleAsync();

		await Page.GetByTitle("Next day").ClickAsync();

		await Expect(Page.GetByRole(AriaRole.Heading, new() { NameRegex = new Regex("^Day 1") }))
			.ToBeVisibleAsync();
	}

	[Test]
	public async Task DesktopNavShowsAboutAndSteps()
	{
		await OpenAsync("/");
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "About" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Steps" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Banner).GetByRole(AriaRole.Button, new() { Name = "Login" }))
			.ToBeVisibleAsync();
	}
}
