namespace RCL.Features.Calendar.Constants;

public static class CalendarColors
{
	//https://www.color-hex.com/color/f57f17
	public const string Forest = "#357cd2";
	public const string Blue = "#0b5394";  //00008b
	public const string LightBlue = "#add8e6";
	public const string MediumBlue = "#3d85c6";
	public const string Purple = "#800080";
	public const string DarkBlue = "#00008b";
	public const string Olive = "#7fa900";  // 808000
	public const string Clay = "#ea7a57";
	public const string Turquoise = "#00bdae";
	public const string Pumpkin = "#f57f17";
	public const string Warning = "#ffc107";  // Fall
	public const string Dark = "#343a40";
	public const string Info = "#17a2b8";
	public const string Primary = "#007bff";  // Winter
	public const string Success = "#28a745";  // Spring
	public const string Danger = "#dc3545";   // Summer 
	public const string Unknown = "#1aaa55";  //
}

/*
Copied from LivingMessiah\Features\Calendar\Constants.cs

### Xref 

Path: LivingMessiah\Features\Calendar\

- Enums\DateType.cs(56):public override string CalendarColor => CalendarColors.Dark;
- Enums\DateType.cs(66):public override string CalendarColor => CalendarColors.Blue;
- Enums\DateType.cs(76):public override string CalendarColor => CalendarColors.Olive;

- Enums\Season.cs(47):public override string CalendarColor => CalendarColors.Primary;
- Enums\Season.cs(57):public override string CalendarColor => CalendarColors.Success;
- Enums\Season.cs(67):public override string CalendarColor => CalendarColors.Danger;
- Enums\Season.cs(78):public override string CalendarColor => CalendarColors.Warning;

- Service.cs(112):CategoryColor = CalendarColors.Info,
 
 */