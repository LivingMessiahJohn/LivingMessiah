using System.Runtime.InteropServices;
using LivingMessiah.ShabbatPdf.Core.Options;

namespace LivingMessiah.ShabbatPdf.Core.Compression;

/// <summary>
/// Resolves the Ghostscript executable path from config, PATH, or common install locations.
/// </summary>
public static class GhostscriptPathResolver
{
    /// <summary>
    /// True when a real executable file was found on disk (config, common install dirs, or PATH).
    /// </summary>
    public static bool TryResolveExistingFile(PdfCompressOptions options, out string path)
    {
        path = string.Empty;
        ArgumentNullException.ThrowIfNull(options);

        if (!string.IsNullOrWhiteSpace(options.GhostscriptPath))
        {
            var configured = options.GhostscriptPath.Trim();
            if (File.Exists(configured))
            {
                path = configured;
                return true;
            }

            return false;
        }

        foreach (var candidate in EnumerateCandidates())
        {
            if (File.Exists(candidate))
            {
                path = candidate;
                return true;
            }
        }

        // Search PATH
        var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
        var names = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? new[] { "gswin64c.exe", "gswin32c.exe", "gs.exe" }
            : new[] { "gs" };

        foreach (var dir in pathEnv.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
        {
            foreach (var name in names)
            {
                var full = Path.Combine(dir.Trim('"'), name);
                if (File.Exists(full))
                {
                    path = full;
                    return true;
                }
            }
        }

        return false;
    }

    private static IEnumerable<string> EnumerateCandidates()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            foreach (var root in new[] { programFiles, programFilesX86 })
            {
                if (string.IsNullOrWhiteSpace(root))
                {
                    continue;
                }

                var gsRoot = Path.Combine(root, "gs");
                if (!Directory.Exists(gsRoot))
                {
                    continue;
                }

                // Prefer highest version folder: gs10.07.1, gs10.03.1, …
                string[] versions;
                try
                {
                    versions = Directory.GetDirectories(gsRoot, "gs*");
                }
                catch (IOException)
                {
                    continue;
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }

                Array.Sort(versions, StringComparer.OrdinalIgnoreCase);
                Array.Reverse(versions);

                foreach (var versionDir in versions)
                {
                    yield return Path.Combine(versionDir, "bin", "gswin64c.exe");
                    yield return Path.Combine(versionDir, "bin", "gswin32c.exe");
                }
            }

            yield break;
        }

        // Linux / macOS common locations
        yield return "/usr/bin/gs";
        yield return "/usr/local/bin/gs";
        yield return "/opt/homebrew/bin/gs";
    }
}
