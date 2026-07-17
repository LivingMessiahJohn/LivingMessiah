using PWA.Features.Home.Shabbat.Teaching.Enums;

namespace PWA.Features.Home.Shabbat.Teaching.Data;

public record BlobDTO(string Url, string Parasha, PdfType PdfType, bool Exists, bool ExceptionOccurred);
/*
- Add LastTriennialEnum to BlobDTO
- Review Parasha
*/