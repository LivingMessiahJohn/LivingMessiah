using ShabbatPdf.Core.Extraction;
using ShabbatPdf.Core.Models;
using ShabbatPdf.Core.Options;
using ShabbatPdf.Core.Pipeline;
using ShabbatPdf.Tests.Storage;
using Microsoft.Extensions.Options;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace ShabbatPdf.Tests.Pipeline;

public class ParsePipelineBlobTests
{
    private const string SourceContainer = "shabbat-service";
    private const string DestContainer = "shabbat-service-md";
    private const string PdfName = "2026-07-04-Lev-16.pdf";

    [Fact]
    public async Task RunAsync_BlobMode_UploadsSameNameTeachingPdf_NotMarkdown()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(SourceContainer);
        await store.EnsureContainerExistsAsync(DestContainer);
        store.Seed(SourceContainer, PdfName, CreateAgendaPdf());

        var pipeline = CreatePipeline(store);
        var result = await pipeline.RunAsync(new ParseRequest(
            SourceName: PdfName,
            BlobMode: true,
            RequireStandardBlobName: true));

        Assert.True(result.Success, result.Message);
        Assert.NotNull(result.Anchors);
        Assert.Equal(3, result.Anchors!.ContentStartPage);
        Assert.Null(result.Markdown);
        Assert.Contains($"{DestContainer}/{PdfName}", result.TeachingPdfUri);
        Assert.True(store.Blobs.ContainsKey($"{DestContainer}/{PdfName}"));
        Assert.False(store.Blobs.ContainsKey($"{DestContainer}/2026-07-04-Lev-16.md"));
        Assert.False(store.Blobs.ContainsKey($"{SourceContainer}/2026-07-04-Lev-16-teaching.pdf"));

        using var teachDoc = PdfDocument.Open(store.Blobs[$"{DestContainer}/{PdfName}"]);
        Assert.Equal(1, teachDoc.NumberOfPages);
        Assert.Contains("Jude 6 teaching text", teachDoc.GetPage(1).Text);
    }

    [Fact]
    public async Task RunAsync_BlobMode_SkipExisting_DoesNotReuploadTeaching()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(SourceContainer);
        await store.EnsureContainerExistsAsync(DestContainer);
        store.Seed(SourceContainer, PdfName, CreateAgendaPdf());
        var existingTeaching = CreateTeachingOnlyPdf("blob existing teaching");
        store.Seed(DestContainer, PdfName, existingTeaching);

        var pipeline = CreatePipeline(store);
        var result = await pipeline.RunAsync(new ParseRequest(
            SourceName: PdfName,
            BlobMode: true,
            SkipIfDestinationExists: true,
            RequireStandardBlobName: true));

        Assert.True(result.Success, result.Message);
        Assert.Equal(existingTeaching, store.Blobs[$"{DestContainer}/{PdfName}"]);
        Assert.Contains($"{DestContainer}/{PdfName}", result.TeachingPdfUri);
        Assert.False(store.Blobs.ContainsKey($"{DestContainer}/2026-07-04-Lev-16.md"));
    }

    [Fact]
    public async Task RunAsync_BlobMode_TeachingOnly_UploadsTeaching_NotMarkdown()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(SourceContainer);
        await store.EnsureContainerExistsAsync(DestContainer);
        store.Seed(SourceContainer, PdfName, CreateAgendaPdf());

        var pipeline = CreatePipeline(store);
        var result = await pipeline.RunAsync(new ParseRequest(
            SourceName: PdfName,
            BlobMode: true,
            TeachingOnly: true,
            RequireStandardBlobName: true));

        Assert.True(result.Success, result.Message);
        Assert.Null(result.Markdown);
        Assert.Null(result.DestinationUri);
        Assert.Contains($"{DestContainer}/{PdfName}", result.TeachingPdfUri);
        Assert.True(store.Blobs.ContainsKey($"{DestContainer}/{PdfName}"));
        Assert.False(store.Blobs.ContainsKey($"{DestContainer}/2026-07-04-Lev-16.md"));
        Assert.False(store.Blobs.ContainsKey($"{SourceContainer}/2026-07-04-Lev-16-teaching.pdf"));
    }

    [Fact]
    public async Task RunAsync_BlobMode_MissingSource_ReturnsSourceNotFound()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(SourceContainer);
        await store.EnsureContainerExistsAsync(DestContainer);

        var pipeline = CreatePipeline(store);
        var result = await pipeline.RunAsync(new ParseRequest(
            SourceName: PdfName,
            BlobMode: true));

        Assert.False(result.Success);
        Assert.StartsWith(ParseErrorCodes.SourceNotFound, result.Message);
    }

    [Fact]
    public async Task RunAsync_BlobMode_EnsureContainer_CreatesDestination()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(SourceContainer);
        store.Seed(SourceContainer, PdfName, CreateAgendaPdf());

        var pipeline = CreatePipeline(store);
        var result = await pipeline.RunAsync(new ParseRequest(
            SourceName: PdfName,
            BlobMode: true,
            EnsureDestinationContainer: true));

        Assert.True(result.Success, result.Message);
        Assert.True(store.Blobs.ContainsKey($"{DestContainer}/{PdfName}"));
    }

    [Fact]
    public async Task RunAsync_BlobMode_InvalidName_FailsWhenRequired()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(SourceContainer);
        await store.EnsureContainerExistsAsync(DestContainer);
        store.Seed(SourceContainer, "notes.pdf", CreateAgendaPdf());

        var pipeline = CreatePipeline(store);
        var result = await pipeline.RunAsync(new ParseRequest(
            SourceName: "notes.pdf",
            BlobMode: true,
            RequireStandardBlobName: true));

        Assert.False(result.Success);
        Assert.StartsWith(ParseErrorCodes.InvalidName, result.Message);
    }

    private static ParsePipeline CreatePipeline(InMemoryBlobStore store)
    {
        var parseOpts = Options.Create(new ParseOptions());
        var blobOpts = Options.Create(new BlobOptions
        {
            SourceContainer = SourceContainer,
            DestinationContainer = DestContainer
        });

        return new ParsePipeline(
            new PdfPigPageSource(parseOpts.Value),
            new AnchorLocator(parseOpts.Value),
            new MarkdownBuilder(),
            parseOpts,
            blobOpts,
            store);
    }

    private static byte[] CreateAgendaPdf()
    {
        var builder = new PdfDocumentBuilder();
        var font = builder.AddStandard14Font(Standard14Font.Helvetica);

        var p1 = builder.AddPage(612, 792);
        p1.AddText("Welcome", 24, new PdfPoint(72, 700), font);
        p1.AddText("Bienvenido", 24, new PdfPoint(72, 400), font);

        var p2 = builder.AddPage(612, 792);
        p2.AddText("Fair Use Policy and Legal Disclaimer", 14, new PdfPoint(72, 700), font);

        var p3 = builder.AddPage(612, 792);
        p3.AddText("Jude 6 teaching text", 14, new PdfPoint(72, 700), font);

        var p4 = builder.AddPage(612, 792);
        p4.AddText("The Avinu Prayer", 24, new PdfPoint(72, 700), font);

        return builder.Build();
    }

    private static byte[] CreateTeachingOnlyPdf(string bodyLine)
    {
        var builder = new PdfDocumentBuilder();
        var font = builder.AddStandard14Font(Standard14Font.Helvetica);
        var page = builder.AddPage(612, 792);
        page.AddText(bodyLine, 14, new PdfPoint(72, 700), font);
        return builder.Build();
    }
}
