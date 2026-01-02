using Ardalis.SmartEnum;
using RCL.Features.Calendar.Constants;
using Microsoft.AspNetCore.Components;

namespace RCL.Features.Calendar.Enums;

public abstract class Season : SmartEnum<Season>
{

	#region Id's
	private static class Id
	{
		internal const int Winter = 1;
		internal const int Spring = 2;
		internal const int Summer = 3;
		internal const int Fall = 4;
	}
	#endregion


	#region Declared Public Instances
	public static readonly Season Winter = new WinterSE();
	public static readonly Season Spring = new SpringSE();
	public static readonly Season Summer = new SummerSE();
	public static readonly Season Fall = new FallSE();
	#endregion

	private Season(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Type { get; }
	public abstract string BadgeColor { get; }
	public abstract string Icon { get; }
	public abstract string Emoji { get; }  // https://www.dotnetcatch.com/2019/06/04/visual-studio-quicktip-add-emoji-to-your-source-code/
	public abstract string CalendarColor { get; }
	public abstract DateRange Range { get; }
  //public abstract MarkupString TitleHtml { get; }
  

  #endregion

  #region Private Instantiation
  private sealed class WinterSE : Season
	{
		public WinterSE() : base($"{nameof(Id.Winter)}", Id.Winter) { }
		public override string Type => "Solstice";
		public override string BadgeColor => "bg-primary";
		public override string Icon => "fas fa-snowflake";
		public override string Emoji => "❄";
		public override string CalendarColor => CalendarColors.Primary;
		public override DateRange Range => new(SeasonDates.Winter, SeasonDates.Spring.AddDays(-1));
    //public override MarkupString TitleHtml => new($"<i class='fas fa-hanukiah'></i>{Type}");
  }
	private sealed class SpringSE : Season
	{
		public SpringSE() : base($"{nameof(Id.Spring)}", Id.Spring) { }
		public override string Type => "Equinox"; // Equilux
		public override string BadgeColor => "bg-success";
		public override string Icon => "fas fa-cloud-sun-rain";
		public override string Emoji => "🌨";
		public override string CalendarColor => CalendarColors.Success;
		public override DateRange Range => new(SeasonDates.Spring, SeasonDates.Summer.AddDays(-1));
	}
	private sealed class SummerSE : Season
	{
		public SummerSE() : base($"{nameof(Id.Summer)}", Id.Summer) { }
		public override string Type => "Solstice";
		public override string BadgeColor => "bg-danger";
		public override string Icon => "far fa-sun";
		public override string Emoji => "☀";
		public override string CalendarColor => CalendarColors.Danger;
		public override DateRange Range => new(SeasonDates.Summer, SeasonDates.Fall.AddDays(-1));

	}
	private sealed class FallSE : Season
	{
		public FallSE() : base("Fall", Id.Fall) { }
		public override string Type => "Equinox";  // Equilux
		public override string BadgeColor => "bg-warning";
		public override string Icon => "fab fa-canadian-maple-leaf";
		public override string Emoji => "🍁";
		public override string CalendarColor => CalendarColors.Warning;
		public override DateRange Range => new(SeasonDates.Fall, SeasonDates.WinterNextYear.AddDays(-1));
	}
	#endregion

}
// Ignore Spelling: Equilux