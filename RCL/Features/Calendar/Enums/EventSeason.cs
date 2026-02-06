using Ardalis.SmartEnum;
using RCL.Features.Calendar.Constants;
using Microsoft.AspNetCore.Components;
//using CSS = RCL.Features.Calendar.Constants.CSS.JustifyContentSuffix;

namespace RCL.Features.Calendar.Enums; 

public abstract class EventSeason : SmartEnum<EventSeason>
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
	public static readonly EventSeason Winter = new WinterSE();
	public static readonly EventSeason Spring = new SpringSE();
	public static readonly EventSeason Summer = new SummerSE();
	public static readonly EventSeason Fall = new FallSE();
	#endregion

	private EventSeason(string name, int value) : base(name, value) { } // Constructor

  #region Extra Fields
  public abstract string Type { get; }
	public abstract string BadgeColor { get; }
	public abstract string Icon { get; }
  public abstract string TextColor { get; }
  public abstract int EventSort { get; }
  //public abstract string Emoji { get; }  // https://www.dotnetcatch.com/2019/06/04/visual-studio-quicktip-add-emoji-to-your-source-code/
  //public abstract string CalendarColor { get; }
  public abstract DateOnly Date { get; }

  #endregion

  #region Extra Properties
  //public string JustifyContent => $"justify-content-{CSS.Center}";  // 
  public string SpanIconAndText => $"<span class='badge {BadgeColor} {TextColor}'><i class='{Icon}'></i> {Name}</span>";

  //public DateOnly? ErevDate => Date.AddDays(-1);
  //public DateOnly? DayTimeDate => Date;
  //public MarkupString DayCellHtml => (MarkupString)$"<span class='badge {BadgeColor} fs-6 text-black text-center'><i class='{Icon}'></i> {this.Name}</span>";
  #endregion

  #region Private Instantiation
  private sealed class WinterSE : EventSeason
	{
		public WinterSE() : base($"{nameof(Id.Winter)}", Id.Winter) { }
		public override string Type => "Solstice";
		public override string BadgeColor => "bg-info";  // bg-primary
    public override string Icon => "fas fa-snowflake";
    public override string TextColor => "text-dark";
    public override int EventSort => 0;
    //public override string Emoji => "❄";
    //public override string CalendarColor => CalendarColors.Primary;
    public override DateOnly Date => SeasonDates.Winter; 
  }
	private sealed class SpringSE : EventSeason
	{
		public SpringSE() : base($"{nameof(Id.Spring)}", Id.Spring) { }
		public override string Type => "Equinox"; // Equilux
		public override string BadgeColor => "bg-success";
		public override string Icon => "fas fa-cloud-sun-rain";
    public override string TextColor => "text-dark";
    public override int EventSort => 0;
    //public override string Emoji => "🌨";
    //public override string CalendarColor => CalendarColors.Success;
    public override DateOnly Date => SeasonDates.Spring;
	}
	private sealed class SummerSE : EventSeason
	{
		public SummerSE() : base($"{nameof(Id.Summer)}", Id.Summer) { }
		public override string Type => "Solstice";
		public override string BadgeColor => "bg-danger";
		public override string Icon => "far fa-sun";
    public override string TextColor => "text-dark";
    public override int EventSort => 0;
    //public override string Emoji => "☀";
    //public override string CalendarColor => CalendarColors.Danger;
    public override DateOnly Date => SeasonDates.Summer; 

	}
	private sealed class FallSE : EventSeason
	{
		public FallSE() : base("Fall", Id.Fall) { }
		public override string Type => "Equinox";  // Equilux
		public override string BadgeColor => "bg-warning";
		public override string Icon => "fab fa-canadian-maple-leaf";
    public override string TextColor => "text-dark";
    public override int EventSort => 0;
    //public override string Emoji => "🍁";
    //public override string CalendarColor => CalendarColors.Warning;
    public override DateOnly Date => SeasonDates.Fall; 
	}
	#endregion

}
// Ignore Spelling: Equilux