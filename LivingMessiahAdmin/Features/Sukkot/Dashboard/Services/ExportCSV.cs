using CsvHelper;
using System.Globalization;
using System.Text;
using Microsoft.JSInterop;
using LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;
using LivingMessiahAdmin.Features.Sukkot.Dashboard.Constants;

namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Services;

public class ExportCSV
{
	private readonly ILogger<ExportCSV> _logger;

	public ExportCSV(ILogger<ExportCSV> logger)
	{
		_logger = logger;
	}

	// Accept IJSRuntime as a parameter so it's provided by the calling component at runtime.
	public async Task DownloadCSV(List<GridQuery>? items, IJSRuntime jsRuntime)
	{
		if (items is not null)
		{
			try
			{
				using var memoryStream = new MemoryStream();
				using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
				using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

				csv.WriteRecords(items);
				await writer.FlushAsync();
				var csvData = Encoding.UTF8.GetString(memoryStream.ToArray());
				await jsRuntime.InvokeVoidAsync(ExportCSVHelper.JavaScriptMethod, ExportCSVHelper.DownloadFileName, csvData);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{Method} {Message}", nameof(DownloadCSV), $"Constants: {ExportCSVHelper.Dump()}");
			}
		}
	}
}