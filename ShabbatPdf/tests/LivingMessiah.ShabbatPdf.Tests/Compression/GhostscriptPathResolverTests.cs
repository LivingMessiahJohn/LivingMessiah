using LivingMessiah.ShabbatPdf.Core.Compression;
using LivingMessiah.ShabbatPdf.Core.Options;

namespace LivingMessiah.ShabbatPdf.Tests.Compression;

public class GhostscriptPathResolverTests
{
    [Fact]
    public void TryResolveExistingFile_ConfiguredMissingPath_ReturnsFalse()
    {
        var opts = new PdfCompressOptions
        {
            GhostscriptPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "no-such-gs.exe")
        };

        var found = GhostscriptPathResolver.TryResolveExistingFile(opts, out var path);

        Assert.False(found);
        Assert.Equal(string.Empty, path);
    }

    [Fact]
    public void TryResolveExistingFile_ConfiguredExistingFile_ReturnsTrue()
    {
        var temp = Path.Combine(Path.GetTempPath(), $"gs-fake-{Guid.NewGuid():N}.exe");
        File.WriteAllText(temp, "fake");
        try
        {
            var opts = new PdfCompressOptions { GhostscriptPath = temp };

            var found = GhostscriptPathResolver.TryResolveExistingFile(opts, out var path);

            Assert.True(found);
            Assert.Equal(temp, path);
        }
        finally
        {
            File.Delete(temp);
        }
    }

    [Fact]
    public void TryResolveExistingFile_EmptyConfig_MayFindInstalledGhostscript()
    {
        // On developer machines with Ghostscript installed this is true; CI without GS is fine either way.
        var opts = new PdfCompressOptions { GhostscriptPath = string.Empty };
        var found = GhostscriptPathResolver.TryResolveExistingFile(opts, out var path);
        if (found)
        {
            Assert.True(File.Exists(path));
        }
    }
}
