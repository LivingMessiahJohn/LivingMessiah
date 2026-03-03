namespace RCL.Features.Calendar.Enums;

public record EventRecord(
  DateOnly Date, 
  Enums.Filter Filter, 
  string JustifyContentSuffix, 
  string CSSBorder, 
  string Span, 
  int Sort2,  
  string ModalLineItem);

// int Sort1, // Filter.[Month][Feast][Season][Parasha][Omer].Value 1-4