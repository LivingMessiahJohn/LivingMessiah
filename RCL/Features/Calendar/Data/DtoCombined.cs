
namespace RCL.Features.Calendar.Data;

public record DtoCombined(DateOnly Date, int RowNumber, bool HasDupes, int DupeCount, Enums.DateType DateType, string? Description);
