using Markdig;
using Microsoft.AspNetCore.Components;

namespace PWA.Helpers;

/// <summary>
/// Converts Markdown (e.g. Special Event descriptions from Admin) to HTML for Blazor MarkupString.
/// </summary>
public static class MarkdownHelper
{
	private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
		.UseAdvancedExtensions()
		.Build();

	public static MarkupString ToMarkup(string? markdown)
	{
		if (string.IsNullOrWhiteSpace(markdown))
		{
			return new MarkupString(string.Empty);
		}

		return new MarkupString(Markdown.ToHtml(markdown, Pipeline));
	}
}
