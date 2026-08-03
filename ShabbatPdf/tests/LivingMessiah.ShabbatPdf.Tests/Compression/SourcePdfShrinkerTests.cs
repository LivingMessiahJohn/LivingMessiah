using LivingMessiah.ShabbatPdf.Core.Compression;
using LivingMessiah.ShabbatPdf.Core.Options;
using LivingMessiah.ShabbatPdf.Tests.Storage;
using Microsoft.Extensions.Options;

namespace LivingMessiah.ShabbatPdf.Tests.Compression;

public class SourcePdfShrinkerTests
{
    private const string Container = "shabbat-service";
    private const string BlobName = "2026-07-18-Lev-18.pdf";

    [Fact]
    public async Task EnsureUnderMaxSize_Disabled_Skips()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Container);
        store.Seed(Container, BlobName, new byte[100]);

        var shrinker = CreateShrinker(
            store,
            new FakeCompressor(),
            new PdfCompressOptions { Enabled = false, MaxBytes = 50 });

        var result = await shrinker.EnsureUnderMaxSizeAsync(Container, BlobName);

        Assert.True(result.Success);
        Assert.False(result.Compressed);
        Assert.Contains("disabled", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(100, store.Blobs[$"{Container}/{BlobName}"].Length);
    }

    [Fact]
    public async Task EnsureUnderMaxSize_AlreadySmall_DoesNotCompress()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Container);
        var payload = new byte[1_000];
        store.Seed(Container, BlobName, payload);

        var fake = new FakeCompressor();
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 10_000 });

        var result = await shrinker.EnsureUnderMaxSizeAsync(Container, BlobName);

        Assert.True(result.Success);
        Assert.False(result.Compressed);
        Assert.Equal(0, fake.CallCount);
        Assert.Equal(payload.Length, result.FinalBytes);
    }

    [Fact]
    public async Task EnsureUnderMaxSize_Oversized_CompressesAndOverwrites()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Container);
        var large = new byte[20_000];
        Array.Fill(large, (byte)0xAB);
        store.Seed(Container, BlobName, large);

        var compressed = new byte[500];
        Array.Fill(compressed, (byte)0xCD);
        var fake = new FakeCompressor { OutputBytes = compressed };

        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 5_000 });

        var result = await shrinker.EnsureUnderMaxSizeAsync(Container, BlobName);

        Assert.True(result.Success, result.Message);
        Assert.True(result.Compressed);
        Assert.Equal(1, fake.CallCount);
        Assert.Equal(large.Length, result.OriginalBytes);
        Assert.Equal(compressed.Length, result.FinalBytes);
        Assert.Equal(compressed, store.Blobs[$"{Container}/{BlobName}"]);
    }

    [Fact]
    public async Task EnsureUnderMaxSize_StillOverLimitAfterCompress_Fails()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Container);
        store.Seed(Container, BlobName, new byte[20_000]);

        var fake = new FakeCompressor { OutputBytes = new byte[15_000] };
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 5_000 });

        var result = await shrinker.EnsureUnderMaxSizeAsync(Container, BlobName);

        Assert.False(result.Success);
        Assert.Contains("still over limit", result.Message, StringComparison.OrdinalIgnoreCase);
        // Original blob left in place
        Assert.Equal(20_000, store.Blobs[$"{Container}/{BlobName}"].Length);
    }

    [Fact]
    public async Task EnsureUnderMaxSize_MissingBlob_Fails()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Container);

        var shrinker = CreateShrinker(
            store,
            new FakeCompressor(),
            new PdfCompressOptions { Enabled = true, MaxBytes = 100 });

        var result = await shrinker.EnsureUnderMaxSizeAsync(Container, BlobName);

        Assert.False(result.Success);
        Assert.Contains("not found", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task EnsureUnderMaxSize_CompressorFails_Propagates()
    {
        var store = new InMemoryBlobStore();
        await store.EnsureContainerExistsAsync(Container);
        store.Seed(Container, BlobName, new byte[20_000]);

        var fake = new FakeCompressor { FailMessage = "Ghostscript exploded" };
        var shrinker = CreateShrinker(
            store,
            fake,
            new PdfCompressOptions { Enabled = true, MaxBytes = 5_000 });

        var result = await shrinker.EnsureUnderMaxSizeAsync(Container, BlobName);

        Assert.False(result.Success);
        Assert.Contains("exploded", result.Message, StringComparison.OrdinalIgnoreCase);
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
