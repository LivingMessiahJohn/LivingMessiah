namespace LivingMessiah.Features.FeastDayPlanner;

public static class Progress
{
	public static string Height = "80px";
	public static string HeightMedium = "50px";
	public static string HeightSmall = "30px";
}

/*
 Move to RCL
 Used by LivingMessiah\LivingMessiah\Features\FeastDayPlanner\
 - Enums\PlannerDetail.cs
 - FeastDayPlanner\EveningAndDay.razor

- [ ] Delete code found in RCL.Features.FeastDayPlanner.Constants
 

public static class BarColor
{
	public static string HebrewExtraBGColor = "bg-secondary-subtle";
	public static string HebrewSabbathBGColor = "bg-danger";  // bg-primary-subtle; bg-danger-subtle
	public static string HebrewSabbathTextColor = "text-white"; // text-dark
	public static string HebrewBGColor = "bg-info-subtle";
	public static string HebrewTextColor = "text-dark"; 
}


public static class HebrewYear
{
	public static bool IsLeapYear = true;
}

 */

// Move to RCL
public static class DynamicComponentPaths
{
	public static string VerseReferenceCards =  "LivingMessiah.Features.FeastDayPlanner.VerseReferenceCards.";
}


public static class Test
{
	public static int AddDays = 0; // Default is 0
}

