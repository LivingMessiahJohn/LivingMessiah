namespace Sukkot.Playwright;

[SetUpFixture]
public class SukkotAppMustBeRunning
{
	[OneTimeSetUp]
	public async Task PingAsync()
	{
		var baseUrl = SukkotPageTest.BaseUrl;
		using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

		try
		{
			using var response = await http.GetAsync(baseUrl);
			if ((int)response.StatusCode < 500)
			{
				return;
			}

			Assert.Fail(
				$"Sukkot at {baseUrl} returned {(int)response.StatusCode}. " +
				"Start it with: dotnet run --project Sukkot/Sukkot.csproj --launch-profile http");
		}
		catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
		{
			Assert.Fail(
				$"Sukkot is not running at {baseUrl}. " +
				"Start it with: dotnet run --project Sukkot/Sukkot.csproj --launch-profile http" +
				$"{Environment.NewLine}{ex.Message}");
		}
	}
}
