using ShabbatPdf.Functions;

namespace ShabbatPdf.Tests.Functions;

public class EventGridBlobParserTests
{
    [Fact]
    public void TryParseBlobUrl_SplitsContainerAndName()
    {
        var ok = EventGridBlobParser.TryParseBlobUrl(
            "https://livingmessiahstorage.blob.core.windows.net/shabbat-service-staging/2026-07-04-Lev-16.pdf",
            out var blobName,
            out var container);

        Assert.True(ok);
        Assert.Equal("shabbat-service-staging", container);
        Assert.Equal("2026-07-04-Lev-16.pdf", blobName);
    }
}
