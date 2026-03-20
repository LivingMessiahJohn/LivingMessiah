```html
@page "/quickgrid-export"
@using Microsoft.AspNetCore.Components.QuickGrid
@using CsvHelper
@using System.Globalization
@using System.IO
@rendermode InteractiveServer
@inject IJSRuntime JSRuntime

<QuickGrid Items="@forecasts.AsQueryable()" TGridItem="WeatherForecast">
    <PropertyColumn Property="@(item => item.Date)" Title="Date" Sortable="true" />
    <PropertyColumn Property="@(item => item.TemperatureC)" Title="Temp. (C)" Sortable="true" />
    <PropertyColumn Property="@(item => item.Summary)" Title="Summary" />
</QuickGrid>

<button @onclick="ExportToCsv">Export to CSV</button>

@code {
```
```sharp
    private List<WeatherForecast> forecasts = new(); // Your data source, populated in OnInitialized

    private async Task ExportToCsv()
    {
        // Get the data to export (apply filters/sorts here if needed, e.g., via LINQ)
        var exportData = forecasts; // Or filtered: forecasts.Where(f => f.TemperatureC > 0).ToList();

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(exportData);

        var csvContent = writer.ToString();
        await JSRuntime.InvokeVoidAsync("downloadFile", "weather-forecasts.csv", csvContent);
    }

    // JavaScript interop for download (add to wwwroot/index.html or _Host.cshtml)
    // <script>
    //     window.downloadFile = (fileName, content) => {
    //         const blob = new Blob([content], { type: 'text/csv' });
    //         const url = URL.createObjectURL(blob);
    //         const a = document.createElement('a');
    //         a.href = url;
    //         a.download = fileName;
    //         a.click();
    //         URL.revokeObjectURL(url);
    //     };
    // </script>
}
```