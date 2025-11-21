namespace RCL.Features.Calendar.Constants;

public static class KeyDateYear
{
  public const int YearId = 2025;

  // mutable static property removes `Unreachable code detected` warning when defined like `const bool IsPregnant = false;`
  public static bool IsPregnant { get; set; } = false;
}