namespace RCL.Features.Calendar.Enums;

public record EventRecord(DateOnly Date, Enums.DateType Category, string JustifyContentSuffix, string Span, int Sort1, int Sort2);
