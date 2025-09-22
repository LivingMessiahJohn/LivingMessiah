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
	private readonly IJSRuntime _jsRuntime;

	public ExportCSV(ILogger<ExportCSV> logger, IJSRuntime jsRuntime)
	{
		_logger = logger;
		_jsRuntime = jsRuntime;
	}

	public async Task DownloadCSV(List<GridQuery>? items)
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
				await _jsRuntime.InvokeVoidAsync(ExportCSVHelper.JavaScriptMethod, ExportCSVHelper.DownloadFileName, csvData);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{Method} {Message}", nameof(DownloadCSV), $"Constants: {ExportCSVHelper.Dump()}");
			}
		}
	}
}