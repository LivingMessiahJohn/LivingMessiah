using ShabbatPdf.Core.Models;

namespace ShabbatPdf.Core.Pipeline;

/// <summary>
/// Orchestrates extract → anchor → markdown → optional upload.
/// Implementation lands in a later PR.
/// </summary>
public interface IParsePipeline
{
    Task<ParseResult> RunAsync(ParseRequest request, CancellationToken ct = default);
}
