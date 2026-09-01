using ShabbatPdf.Core.Compression;
using ShabbatPdf.Core.Options;
using ShabbatPdf.Tests.Storage;
using Microsoft.Extensions.Options;

namespace ShabbatPdf.Tests.Compression;

public class SourcePdfShrinkerTests
{
    private const string Staging = "shabbat-service-staging";
    private const string Service = "shabbat-service";
    private const string BlobName = "2026-07-18-Lev-18.pdf";

    [Fact]
    public async Task Publish_Disabled_CopiesUncompressed()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Staging);
        await store.EnsureContainerExistsAsync(Service);
        var payload = new byte[100];
        Array.Fill(payload, (byte)0xAA);
        store.Seed(Staging, BlobName, payload);

        var shrinker = CreateShrinker(
            store,
            new FakeCompressor(),
            new PdfCompressOptions { Enabled = false, MaxBytes = 50 });

        var result = await shrinker.PublishAsync(Staging, Service, BlobName);

        Assert.True(result.Success, result.Message);
        Assert.False(result.Compressed);
        Assert.True(result.Copied);
        Assert.Contains("disabled", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(payload, store.Blobs[$"{Staging}/{BlobName}"]);
        Assert.Equal(payload, store.Blobs[$"{Service}/{BlobName}"]);
    }

    [Fact]
    public async Task Publish_AlreadySmall_CopiesAsIs()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Staging);
        await store.EnsureContainerExistsAsync(Service);
        var payload = new byte[1_000];
        store.Seed(Staging, BlobName, payload);

        var fake = new FakeCompressor();
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 10_000 });

        var result = await shrinker.PublishAsync(Staging, Service, BlobName);

        Assert.True(result.Success, result.Message);
        Assert.False(result.Compressed);
        Assert.True(result.Copied);
        Assert.Equal(0, fake.CallCount);
        Assert.Equal(payload.Length, result.FinalBytes);
        Assert.Equal(payload, store.Blobs[$"{Service}/{BlobName}"]);
        Assert.Equal(payload, store.Blobs[$"{Staging}/{BlobName}"]);
    }

    [Fact]
    public async Task Publish_Oversized_CompressesToService_LeavesStaging()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Staging);
        await store.EnsureContainerExistsAsync(Service);
        var large = new byte[20_000];
        Array.Fill(large, (byte)0xAB);
        store.Seed(Staging, BlobName, large);

        var compressed = new byte[500];
        Array.Fill(compressed, (byte)0xCD);
        var fake = new FakeCompressor { OutputBytes = compressed };

        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 5_000 });

        var result = await shrinker.PublishAsync(Staging, Service, BlobName);

        Assert.True(result.Success, result.Message);
        Assert.True(result.Compressed);
        Assert.False(result.Copied);
        Assert.Equal(1, fake.CallCount);
        Assert.Equal(large.Length, result.OriginalBytes);
        Assert.Equal(compressed.Length, result.FinalBytes);
        Assert.Equal(compressed, store.Blobs[$"{Service}/{BlobName}"]);
        Assert.Equal(large, store.Blobs[$"{Staging}/{BlobName}"]);
    }

    [Fact]
    public async Task Publish_StillOverLimitAfterCompress_Fails_DoesNotWriteService()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Staging);
        await store.EnsureContainerExistsAsync(Service);
        store.Seed(Staging, BlobName, new byte[20_000]);

        var fake = new FakeCompressor { OutputBytes = new byte[15_000] };
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 5_000 });

        var result = await shrinker.PublishAsync(Staging, Service, BlobName);

        Assert.False(result.Success);
        Assert.Contains("still over limit", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(20_000, store.Blobs[$"{Staging}/{BlobName}"].Length);
        Assert.False(store.Blobs.ContainsKey($"{Service}/{BlobName}"));
    }

    [Fact]
    public async Task Publish_MissingBlob_Fails()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Staging);
        await store.EnsureContainerExistsAsync(Service);

        var shrinker = CreateShrinker(
            store,
            new FakeCompressor(),
            new PdfCompressOptions { Enabled = true, MaxBytes = 100 });

        var result = await shrinker.PublishAsync(Staging, Service, BlobName);

        Assert.False(result.Success);
        Assert.Contains("not found", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Publish_CompressorFails_LeavesStaging()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Staging);
        await store.EnsureContainerExistsAsync(Service);
        store.Seed(Staging, BlobName, new byte[20_000]);

        var fake = new FakeCompressor { FailMessage = "Ghostscript exploded" };
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 5_000 });

        var result = await shrinker.PublishAsync(Staging, Service, BlobName);

        Assert.False(result.Success);
        Assert.Contains("exploded", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(20_000, store.Blobs[$"{Staging}/{BlobName}"].Length);
        Assert.False(store.Blobs.ContainsKey($"{Service}/{BlobName}"));
    }

    [Fact]
    public async Task Publish_SameContainerAlreadySmall_DoesNotReupload()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Service);
        var payload = new byte[100];
        store.Seed(Service, BlobName, payload);

        var fake = new FakeCompressor();
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 1_000 });

        var result = await shrinker.PublishAsync(Service, Service, BlobName);

        Assert.True(result.Success, result.Message);
        Assert.False(result.Copied);
        Assert.False(result.Compressed);
        Assert.Equal(0, fake.CallCount);
        Assert.Equal(payload, store.Blobs[$"{Service}/{BlobName}"]);
    }

    private static SourcePdfShrinker CreateShrinker(
        InMemoryBlobStore store,
        IPdfCompressor compressor,
        PdfCompressOptions options) =>
        new(store, compressor, Options.Create(options));

    private sealed class FakeCompressor : IPdfCompressor
    {
        public int CallCount { get; private set; }

        public byte[] OutputBytes { get; init; } = [1, 2, 3];

        public string? FailMessage { get; init; }

        public Task<PdfCompressResult> CompressAsync(
            string inputPath,
            string outputPath,
            CancellationToken cancellationToken = default)
        {
            CallCount++;
            var inputBytes = new FileInfo(inputPath).Length;

            if (FailMessage is not null)
            {
                return Task.FromResult(PdfCompressResult.Fail(FailMessage, inputBytes));
            }

            File.WriteAllBytes(outputPath, OutputBytes);
            return Task.FromResult(
                PdfCompressResult.Ok(outputPath, inputBytes, OutputBytes.LongLength));
        }
    }
}
