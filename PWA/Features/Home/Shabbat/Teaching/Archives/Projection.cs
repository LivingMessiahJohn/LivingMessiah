namespace PWA.Features.Home.Shabbat.Teaching.Archives;

public record Projection(
	string TorahAbrv,
	string TeachingHref,
	string TeachingPdfFile,
	string CompleteServiceHref,
	string CompleteServicePdfFile);

// href = $"{Blob.BaseUrl}{pdfFile}"
