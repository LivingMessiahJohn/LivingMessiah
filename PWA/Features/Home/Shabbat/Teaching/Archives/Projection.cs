namespace PWA.Features.Home.Shabbat.Teaching.Archives;

public record Projection(
	string TorahAbrv,
	string PdfFile,
	string TeachingHref,
	string CompleteServiceHref); 

// href = $"{Blob.BaseUrl}{pdfFile}"
