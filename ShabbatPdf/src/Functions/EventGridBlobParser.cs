using System.Text.Json;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;

namespace ShabbatPdf.Functions;

/// <summary>
/// Reads container + blob name from a Storage BlobCreated Event Grid event.
/// </summary>
public static class EventGridBlobParser
{
    public static bool TryGetBlobName(
        EventGridEvent eventGridEvent,
        out string blobName,
        out string? container)
    {
        blobName = string.Empty;
        container = null;

        if (eventGridEvent.TryGetSystemEventData(out object? systemEvent)
            && systemEvent is StorageBlobCreatedEventData created
            && !string.IsNullOrWhiteSpace(created.Url))
        {
            return TryParseBlobUrl(created.Url, out blobName, out container);
        }

        try
        {
            if (eventGridEvent.Data is not null)
            {
                using var doc = JsonDocument.Parse(eventGridEvent.Data.ToString());
                if (doc.RootElement.TryGetProperty("url", out var urlProp)
                    && urlProp.ValueKind == JsonValueKind.String)
                {
                    var url = urlProp.GetString();
                    if (!string.IsNullOrWhiteSpace(url)
                        && TryParseBlobUrl(url, out blobName, out container))
                    {
                        return true;
                    }
                }
            }
        }
        catch (JsonException)
        {
            // fall through to subject parse
        }

        var subject = eventGridEvent.Subject ?? string.Empty;
        const string marker = "/blobs/";
        var idx = subject.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (idx >= 0)
        {
            blobName = Uri.UnescapeDataString(subject[(idx + marker.Length)..]);
            const string containers = "/containers/";
            var cIdx = subject.IndexOf(containers, StringComparison.OrdinalIgnoreCase);
            if (cIdx >= 0)
            {
                var start = cIdx + containers.Length;
                var end = subject.IndexOf('/', start);
                if (end > start)
                {
                    container = subject[start..end];
                }
            }

            return !string.IsNullOrWhiteSpace(blobName);
        }

        return false;
    }

    public static bool TryParseBlobUrl(string url, out string blobName, out string? container)
    {
        blobName = string.Empty;
        container = null;

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            return false;
        }

        var parts = uri.AbsolutePath.Trim('/').Split('/', 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            return false;
        }

        container = Uri.UnescapeDataString(parts[0]);
        blobName = Uri.UnescapeDataString(parts[1]);
        return !string.IsNullOrWhiteSpace(blobName);
    }
}
