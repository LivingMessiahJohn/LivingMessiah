namespace Sukkot.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class StepsPageTests : SukkotPageTest
{
	[Test]
	public async Task ShowsNotAuthorizedWhenLoggedOut()
	{
		await OpenAsync("/Steps");
		await Expect(Page).ToHaveTitleAsync("Steps");
		await Expect(Page.GetByText("Not Authorized")).ToBeVisibleAsync();
		await Expect(Page.GetByText("user not logged in")).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).First)
			.ToBeVisibleAsync();
	}
}
