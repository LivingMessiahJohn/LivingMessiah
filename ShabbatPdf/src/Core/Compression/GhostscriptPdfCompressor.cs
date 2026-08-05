using System.Diagnostics;
using System.Text;
using ShabbatPdf.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Compresses PDFs by shelling out to Ghostscript (<c>pdfwrite</c> + <c>-dPDFSETTINGS</c>).
/// Ghostscript is dual-licensed AGPL / commercial (Artifex); operators must ensure license fit.
/// </summary>
public sealed class GhostscriptPdfCompressor : IPdfCompressor
{
    private readonly PdfCompressOptions _options;
    private readonly ILogger<GhostscriptPdfCompressor> _logger;

    public GhostscriptPdfCompressor(
        IOptions<PdfCompressOptions> options,
        ILogger<GhostscriptPdfCompressor>? logger = null)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<GhostscriptPdfCompressor>.Instance;
    }

    public async Task<PdfCompressResult> CompressAsync(
        string inputPath,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(inputPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);

        if (!File.Exists(inputPath))
        {
            return PdfCompressResult.Fail($"Input PDF not found: {inputPath}");
        }

        if (string.Equals(
                Path.GetFullPath(inputPath),
                Path.GetFullPath(outputPath),
                StringComparison.OrdinalIgnoreCase))
        {
            return PdfCompressResult.Fail("Input and output PDF paths must differ.");
        }

        var inputBytes = new FileInfo(inputPath).Length;

        if (!GhostscriptPathResolver.TryResolveExistingFile(_options, out var gsPath))
        {
            var hint = string.IsNullOrWhiteSpace(_options.GhostscriptPath)
                ? "Install Ghostscript or set PdfCompress:GhostscriptPath."
                : $"PdfCompress:GhostscriptPath not found: '{_options.GhostscriptPath}'.";
            return PdfCompressResult.Fail(
                $"Ghostscript executable not found. {hint}",
                inputBytes);
        }

        var settings = NormalizePdfSettings(_options.PdfSettings);
        var compatibility = string.IsNullOrWhiteSpace(_options.CompatibilityLevel)
            ? "1.4"
            : _options.CompatibilityLevel.Trim();

        var outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Ghostscript requires -sOutputFile=path as a single argv token.
        var args = new List<string>
        {
            "-sDEVICE=pdfwrite",
            $"-dCompatibilityLevel={compatibility}",
            $"-dPDFSETTINGS={settings}",
            "-dNOPAUSE",
            "-dQUIET",
            "-dBATCH",
            $"-sOutputFile={outputPath}",
            inputPath
        };

        _logger.LogInformation(
            "Ghostscript compress start exe={Exe} settings={Settings} inputBytes={Bytes} in={In} out={Out}",
            gsPath,
            settings,
            inputBytes,
            inputPath,
            outputPath);

        var timeout = TimeSpan.FromSeconds(Math.Clamp(_options.TimeoutSeconds, 30, 3600));
        ProcessResult processResult;
        try
        {
            processResult = await RunProcessAsync(gsPath, args, timeout, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return PdfCompressResult.Fail(
                $"Ghostscript timed out after {timeout.TotalSeconds:0}s.",
                inputBytes);
        }
        catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception)
        {
            _logger.LogError(ex, "Failed to start Ghostscript at {Path}", gsPath);
            return PdfCompressResult.Fail(
                $"Failed to start Ghostscript: {ex.Message}",
                inputBytes);
        }

        if (processResult.ExitCode != 0)
        {
            var detail = string.IsNullOrWhiteSpace(processResult.StdErr)
                ? processResult.StdOut
                : processResult.StdErr;
            if (string.IsNullOrWhiteSpace(detail))
            {
                detail = $"(no output, exit {processResult.ExitCode})";
            }

            _logger.LogError(
                "Ghostscript failed exit={Code}: {Detail}",
                processResult.ExitCode,
                detail);
            return PdfCompressResult.Fail(
                $"Ghostscript failed (exit {processResult.ExitCode}): {TrimOneLine(detail)}",
                inputBytes);
        }

        if (!File.Exists(outputPath))
        {
            return PdfCompressResult.Fail(
                "Ghostscript exited 0 but output PDF was not created.",
                inputBytes);
        }

        var outputBytes = new FileInfo(outputPath).Length;
        if (outputBytes <= 0)
        {
            return PdfCompressResult.Fail(
                "Ghostscript produced an empty output PDF.",
                inputBytes);
        }

        _logger.LogInformation(
            "Ghostscript compress done inputMB={In:F1} outputMB={Out:F1} ratio={Ratio:P0}",
            inputBytes / (1024d * 1024d),
            outputBytes / (1024d * 1024d),
            inputBytes > 0 ? (double)outputBytes / inputBytes : 0);

        return PdfCompressResult.Ok(outputPath, inputBytes, outputBytes);
    }

    public static string NormalizePdfSettings(string? settings)
    {
        if (string.IsNullOrWhiteSpace(settings))
        {
            return "/ebook";
        }

        var s = settings.Trim();
        return s.StartsWith('/') ? s : "/" + s;
    }

    private static string TrimOneLine(string text)
    {
        var one = text.Replace('\r', ' ').Replace('\n', ' ').Trim();
        return one.Length <= 400 ? one : one[..400] + "…";
    }

    private static async Task<ProcessResult> RunProcessAsync(
        string fileName,
        IReadOnlyList<string> args,
        TimeSpan timeout,
        CancellationToken cancellationToken)
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        foreach (var arg in args)
        {
            psi.ArgumentList.Add(arg);
        }

        using var process = new Process { StartInfo = psi };
        var stdOut = new StringBuilder();
        var stdErr = new StringBuilder();

        process.OutputDataReceived += (_, e) =>
        {
            if (e.Data is not null)
            {
                stdOut.AppendLine(e.Data);
            }
        };
        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data is not null)
            {
                stdErr.AppendLine(e.Data);
            }
        };

        if (!process.Start())
        {
            throw new InvalidOperationException($"Could not start process: {fileName}");
        }

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCts.CancelAfter(timeout);

        try
        {
            await process.WaitForExitAsync(timeoutCts.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            try
            {
                if (!process.HasExited)
                {
                    process.Kill(entireProcessTree: true);
                }
            }
            catch (InvalidOperationException)
            {
                // already exited
            }

            throw;
        }

        // Ensure async readers finish
        await process.WaitForExitAsync(CancellationToken.None).ConfigureAwait(false);

        return new ProcessResult(process.ExitCode, stdOut.ToString(), stdErr.ToString());
    }

    private sealed record ProcessResult(int ExitCode, string StdOut, string StdErr);
}
