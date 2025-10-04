namespace LivingMessiah.Features.Home.UpcomingFeasts;

public class FeastCardDTO
{
	public string Title { get; set; } = default!;
	public string Index { get; set; } = default!;
	public string Image { get; set; } = default!;
	public string FloatRightHebrew { get; set; } = default!;
	public string RangeFormatted { get; set; } = default!;	
	public int DaysAway { get; set; } = 0;


}