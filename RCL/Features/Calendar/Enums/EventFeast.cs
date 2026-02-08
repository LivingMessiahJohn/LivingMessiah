using Ardalis.SmartEnum;
using RCL.Features.Calendar.Constants;

namespace RCL.Features.Calendar.Enums;

public abstract class EventFeast : SmartEnum<EventFeast>
{
  #region Id's
  private static class Id
  {
    internal const int Hanukkah = 3;
    internal const int Purim = 4;
    internal const int Seder = 5;
    internal const int Matzah1 = 6;
    internal const int Matzah2 = 7;
    internal const int Omer = 8;
    internal const int Shavuot = 9;
    internal const int YomTeruah = 10;
    internal const int YomKippur = 11;
    internal const int SukkotPrep = 12;
    internal const int SukkotDay1 = 13;
    internal const int SukkotDay8 = 14;
    internal const int SukkotTearDown = 15;
  }
  #endregion

  #region Declared Public Instances
  public static readonly EventFeast Hanukkah = new HanukkahSE();
  public static readonly EventFeast Purim = new PurimSE();
  public static readonly EventFeast Seder = new SederSE();
  public static readonly EventFeast Matzah1 = new Matzah1SE();
  public static readonly EventFeast Matzah7 = new Matzah7SE();
  public static readonly EventFeast Omer = new OmerSE();
  public static readonly EventFeast Shavuot = new ShavuotSE();
  public static readonly EventFeast YomTeruah = new YomTeruahSE();
  public static readonly EventFeast YomKippur = new YomKippurSE();
  public static readonly EventFeast SukkotPrep = new SukkotPrepSE();
  public static readonly EventFeast SukkotDay1 = new SukkotDay1SE();
  public static readonly EventFeast SukkotDay8 = new SukkotDay8SE();
  public static readonly EventFeast SukkotTearDown = new SukkotTearDownSE();
  #endregion

  private EventFeast(string name, int value) : base(name, value) { }

  #region Extra Fields
  public abstract string Icon { get; }
  public abstract string Title { get; }
  public abstract DateOnly Date { get; }
  public abstract FeastDateRange FeastDateRange { get; }
  public abstract string BadgeColor { get; }
  public abstract string TextColor { get; }
  public abstract DateType Category { get; }
  public abstract int EventSort { get; }
  public abstract bool IsSplit { get; }
  public abstract int Repeats { get; }  // ToDo: do something with this or remove it
  #endregion

  #region Extra Properties
  public DateOnly ErevDate => Date.AddDays(-1);
  public DateOnly DayTimeDate => Date;
  public string SpanIcon => $"<span class='badge {BadgeColor}'><i class='{Icon} '></i></span>";
  public string SpanText
  {
    get
    {
      if (this == EventFeast.Seder | this == EventFeast.SukkotPrep | this == EventFeast.SukkotTearDown)
      {
        return $"<span class='badge badge bg-opacity-25 {TextColor}'>blank</span>";
      }
      else
      {
        return $"<span class='badge {BadgeColor} {TextColor}'>{Title}</span>";
      }
    }
  }

  public string SpanIconAndText => $"<span class='badge {BadgeColor} {TextColor}'><i class='{Icon}'></i> {Title}</span>";
  public string SpanBlank => $"<span class='badge badge bg-opacity-25 text-white'> blank</span>";

  #endregion

  #region Private Instantiation

  private sealed class HanukkahSE : EventFeast
  {
    public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
    public override string Icon => "fas fa-hanukiah";
    public override string Title => "Hanukkah";
    public override DateOnly Date => FeastDayDates.Hanukkah;
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Hanukkah.AddDays(-1), FeastDayDates.Hanukkah.AddDays(7));
    public override string BadgeColor => "bg-primary";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 8;
  }

  private sealed class PurimSE : EventFeast
  {
    public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
    public override string Icon => "fas fa-dice";
    public override string Title => "Purim";
    public override DateOnly Date => FeastDayDates.Purim;
    //public override FeastDateRange DateRange => new(FeastDayDates.Purim, FeastDayDates.Purim);
    public override FeastDateRange FeastDateRange => new(Date.AddDays(-1), Date);
    public override string BadgeColor => "bg-warning";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class SederSE : EventFeast
  {
    public SederSE() : base($"{nameof(Id.Seder)}", Id.Seder) { }
    public override string Icon => "fas fa-door-open";
    public override string Title => "Seder";
    //public override DateOnly Date => FeastDayDates.Passover; 
    public override DateOnly Date => FeastDayDates.Passover.AddDays(-1);
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Passover.AddDays(-1), FeastDayDates.Passover.AddDays(6));
    public override string BadgeColor => "bg-primary-subtle";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => false;
    public override int Repeats => 0;
  }

  private sealed class Matzah1SE : EventFeast
  {
    public Matzah1SE() : base($"{nameof(Id.Matzah1)}", Id.Matzah1) { }
    public override string Icon => "fas fa-times-circle";
    public override string Title => "Matzah 1";
    public override DateOnly Date => FeastDayDates.Passover;
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Passover.AddDays(-1), FeastDayDates.Passover.AddDays(6));
    public override string BadgeColor => "bg-primary";
    public override string TextColor => "text-white";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 1;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class Matzah7SE : EventFeast
  {
    public Matzah7SE() : base($"{nameof(Id.Matzah2)}", Id.Matzah2) { }
    public override string Icon => "fas fa-times-circle";
    public override string Title => "Matzah 7";
    public override DateOnly Date => FeastDayDates.Passover.AddDays(6);
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Passover.AddDays(-1), FeastDayDates.Passover.AddDays(6));
    public override string BadgeColor => "bg-primary";
    public override string TextColor => "text-white";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class OmerSE : EventFeast
  {
    public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
    public override string Icon => "fas fa-hashtag";
    public override string Title => "Omer";
    public override DateOnly Date => FeastDayDates.Passover.AddDays(+2); // Fix was -1
    public override FeastDateRange FeastDateRange => new(Date, Date);
    public override string BadgeColor => "bg-info";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => false;
    public override int Repeats => 49;
  }

  private sealed class ShavuotSE : EventFeast
  {
    public ShavuotSE() : base($"{nameof(Id.Shavuot)}", Id.Shavuot) { }
    public override string Icon => "fas fa-book-open";
    public override string Title => "Shavuot";
    public override DateOnly Date => FeastDayDates.Weeks; // ToDo: rename to FeastDayDates.Shavuot
    public override FeastDateRange FeastDateRange => new(Date.AddDays(-1), Date);
    public override string BadgeColor => "bg-success";
    public override string TextColor => "text-white";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class YomTeruahSE : EventFeast
  {
    public YomTeruahSE() : base($"{nameof(Id.YomTeruah)}", Id.YomTeruah) { }
    public override string Icon => "fas fa-bullhorn";
    public override string Title => "Yom Teruah";
    public override DateOnly Date => FeastDayDates.Trumpets; // ToDo: rename to FeastDayDates.YomTeruah
    public override FeastDateRange FeastDateRange => new(Date.AddDays(-1), Date);
    public override string BadgeColor => "bg-warning";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class YomKippurSE : EventFeast
  {
    public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
    public override string Icon => "fas fa-hands-helping";
    public override string Title => "Yom Kippur";
    public override DateOnly Date => FeastDayDates.YomKippur;
    public override FeastDateRange FeastDateRange => new(Date.AddDays(-1), Date);
    public override string BadgeColor => "bg-danger";
    public override string TextColor => "text-white";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class SukkotPrepSE : EventFeast
  {
    public SukkotPrepSE() : base($"{nameof(Id.SukkotPrep)}", Id.SukkotPrep) { }
    public override string Icon => "fas fa-campground";
    public override string Title => "Preparation";
    public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(-1); // ToDo: rename to FeastDayDates.Sukkot
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Tabernacles.AddDays(-1), FeastDayDates.Tabernacles.AddDays(8));
    public override string BadgeColor => "bg-primary-subtle";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => false;
    public override int Repeats => 0;
  }

  private sealed class SukkotDay1SE : EventFeast
  {
    public SukkotDay1SE() : base($"{nameof(Id.SukkotDay1)}", Id.SukkotDay1) { }
    public override string Icon => "fas fa-campground";
    public override string Title => "Sukkot Day 1";
    public override DateOnly Date => FeastDayDates.Tabernacles; // ToDo: rename to FeastDayDates.Sukkot
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Tabernacles.AddDays(-1), FeastDayDates.Tabernacles.AddDays(8));
    public override string BadgeColor => "bg-primary";
    public override string TextColor => "text-white";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 1;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class SukkotDay8SE : EventFeast
  {
    public SukkotDay8SE() : base($"{nameof(Id.SukkotDay8)}", Id.SukkotDay8) { }
    public override string Icon => "fas fa-campground";
    public override string Title => "Sukkot Day 8";
    public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(7); // ToDo: rename to FeastDayDates.Sukkot
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Tabernacles.AddDays(-1), FeastDayDates.Tabernacles.AddDays(8));
    public override string BadgeColor => "bg-primary";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => true;
    public override int Repeats => 0;
  }

  private sealed class SukkotTearDownSE : EventFeast
  {
    public SukkotTearDownSE() : base($"{nameof(Id.SukkotTearDown)}", Id.SukkotTearDown) { }
    public override string Icon => "fas fa-campground";
    public override string Title => "Tear Down";
    public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(8); // ToDo: rename to FeastDayDates.Sukkot
    public override FeastDateRange FeastDateRange => new(FeastDayDates.Tabernacles.AddDays(-1), FeastDayDates.Tabernacles.AddDays(8));
    public override string BadgeColor => "bg-primary-subtle";
    public override string TextColor => "text-dark";
    public override DateType Category => DateType.Feast;
    public override int EventSort => 0;
    public override bool IsSplit => false;
    public override int Repeats => 0;
  }

  #endregion
}

public record FeastDateRange(DateOnly Min, DateOnly Max);
