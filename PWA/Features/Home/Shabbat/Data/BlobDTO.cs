namespace PWA.Features.Home.Shabbat.Data;

public record BlobDTO(string Url, string Parasha, Enums.PdfType PdfType, bool Exists, bool ExceptionOccurred);
/*
- Add LastTriennialEnum to BlobDTO
- Review Parasha
*/