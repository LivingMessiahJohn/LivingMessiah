namespace Sukkot.Playwright;

public abstract class SukkotPageTest : PageTest
{
	internal static string BaseUrl =>
		(Environment.GetEnvironmentVariable("SUKKOT_BASE_URL") ?? "http://localhost:5000")
			.TrimEnd('/');

	public override BrowserNewContextOptions ContextOptions()
	{
		return new BrowserNewContextOptions
		{
			BaseURL = BaseUrl,
			IgnoreHTTPSErrors = true,
			ViewportSize = new ViewportSize { Width = 1280, Height = 800 }
		};
	}

	protected async Task OpenAsync(string path = "/")
	{
		await Page.GotoAsync(path, new PageGotoOptions { WaitUntil = WaitUntilState.Load });
	}
}
