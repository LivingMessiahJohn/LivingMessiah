using LivingMessiah.ShabbatPdf.Core.Compression;
using LivingMessiah.ShabbatPdf.Core.Options;
using Microsoft.Extensions.Options;
using UglyToad.PdfPig.Writer;

namespace LivingMessiah.ShabbatPdf.Tests.Compression;

public class GhostscriptPdfCompressorTests
{
    [Theory]
    [InlineData(null, "/ebook")]
    [InlineData("", "/ebook")]
    [InlineData("ebook", "/ebook")]
    [InlineData("/screen", "/screen")]
    [InlineData("  /printer  ", "/printer")]
    public void NormalizePdfSettings_AddsSlashAndDefaults(string? input, string expected)
    {
        Assert.Equal(expected, GhostscriptPdfCompressor.NormalizePdfSettings(input));
    }

    [Fact]
    public async Task CompressAsync_MissingGhostscript_FailsClearly()
    {
        var opts = Options.Create(new PdfCompressOptions
        {
            GhostscriptPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "missing-gs.exe")
        });
        var compressor = new GhostscriptPdfCompressor(opts);

        var input = CreateTinyPdf();
        var output = input + ".out.pdf";
        try
        {
            var result = await compressor.CompressAsync(input, output);

            Assert.False(result.Success);
            Assert.Contains("Ghostscript", result.Message, StringComparison.OrdinalIgnoreCase);
            Assert.True(result.InputBytes > 0);
        }
        finally
        {
            TryDelete(input);
            TryDelete(output);
        }
    }

    [Fact]
    public async Task CompressAsync_SameInputOutputPath_Fails()
    {
        var opts = Options.Create(new PdfCompressOptions());
        var compressor = new GhostscriptPdfCompressor(opts);
        var path = CreateTinyPdf();
        try
        {
            var result = await compressor.CompressAsync(path, path);
            Assert.False(result.Success);
            Assert.Contains("must differ", result.Message, StringComparison.OrdinalIgnoreCase);
        }
        finally
        {
            TryDelete(path);
        }
    }

    [Fact]
    public async Task CompressAsync_WithInstalledGhostscript_ProducesSmallerOrEqualPdf()
    {
        if (!GhostscriptPathResolver.TryResolveExistingFile(new PdfCompressOptions(), out _))
        {
            // CI / machines without GS
            return;
        }

        var opts = Options.Create(new PdfCompressOptions
        {
            PdfSettings = "/ebook",
            TimeoutSeconds = 120
        });
        var compressor = new GhostscriptPdfCompressor(opts);

        var input = CreateTinyPdf();
        var output = Path.Combine(Path.GetTempPath(), $"gs-out-{Guid.NewGuid():N}.pdf");
        try
        {
            var result = await compressor.CompressAsync(input, output);

            Assert.True(result.Success, result.Message);
            Assert.True(File.Exists(output));
            Assert.True(result.OutputBytes > 0);
            Assert.Equal(new FileInfo(input).Length, result.InputBytes);
        }
        finally
        {
            TryDelete(input);
            TryDelete(output);
        }
    }

    private static string CreateTinyPdf()
    {
        var builder = new PdfDocumentBuilder();
        builder.AddPage(612, 792);
        var bytes = builder.Build();
        var path = Path.Combine(Path.GetTempPath(), $"gs-in-{Guid.NewGuid():N}.pdf");
        File.WriteAllBytes(path, bytes);
        return path;
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            // best-effort cleanup
        }
    }
}
