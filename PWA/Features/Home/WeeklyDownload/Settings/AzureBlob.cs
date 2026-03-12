namespace PWA.Features.Home.WeeklyDownload.Settings;

/*
Called by 
- PWA\Program.cs `builder.Services.Configure<AzureBlob>(builder.Configuration.GetSection("AzureBlob"));`
  - PWA\Features\Home\ServiceCollectionExtensions.cs
*/
public class AzureBlob
{
  public string? ConnectionString { get; set; } // = string.Empty;
  public string? ContainerName { get; set; } // = string.Empty;
}