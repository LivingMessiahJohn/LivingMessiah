namespace RCL.Features.Calendar.Enums;

public record EventRecord(DateOnly Date, Enums.Filter Filter, string JustifyContentSuffix, string Span, int Sort1, int Sort2);

