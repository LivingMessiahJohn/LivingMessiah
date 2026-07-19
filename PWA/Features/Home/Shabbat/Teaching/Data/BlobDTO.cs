using ParashaEnums = RCL.Features.Parasha.Enums;

namespace PWA.Features.Home.Shabbat.Teaching.Data;

public record BlobDTO(string Url, string Parasha, ParashaEnums.PdfType PdfType, bool Exists, bool ExceptionOccurred);
/*
- Add LastTriennialEnum to BlobDTO
- Review Parasha
*/