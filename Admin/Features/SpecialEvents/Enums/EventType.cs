using Ardalis.SmartEnum;

namespace Admin.Features.SpecialEvents.Enums;

public abstract class EventType : SmartEnum<EventType>
{
	#region Id's
	private static class Id
	{
		internal const int MensCoffeeClub = 2;
		internal const int LadiesEveningFellowship = 3;
		internal const int CommunityDinner = 4;
		internal const int ErevShabbat = 5;
		internal const int Movie = 6;
		internal const int GuestSpeaker = 7;
		internal const int Other = 8;
		internal const int NewMoon = 9;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly EventType MensCoffeeClub = new MensCoffeeClubSE();
	public static readonly EventType LadiesEveningFellowship = new LadiesEveningFellowshipSE();
	public static readonly EventType CommunityDinner = new CommunityDinnerSE();
	public static readonly EventType ErevShabbat = new ErevShabbatSE();
	public static readonly EventType Movie = new MovieSE();
	public static readonly EventType GuestSpeaker = new GuestSpeakerSE();
	public static readonly EventType Other = new OtherSE();
	public static readonly EventType NewMoon = new NewMoonSE();
	// SE=SmartEnum
	#endregion

	private EventType(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Descr { get; }
	#endregion


	#region Private Instantiation

	private sealed class MensCoffeeClubSE : EventType
	{
		public MensCoffeeClubSE() : base($"{nameof(Id.MensCoffeeClub)}", Id.MensCoffeeClub) { }
		public override string Descr => "Mens Coffee Club";
	}

	private sealed class LadiesEveningFellowshipSE : EventType
	{
		public LadiesEveningFellowshipSE() : base($"{nameof(Id.LadiesEveningFellowship)}", Id.LadiesEveningFellowship) { }
		public override string Descr => "Ladies Evening Fellowship";
	}

	private sealed class CommunityDinnerSE : EventType
	{
		public CommunityDinnerSE() : base($"{nameof(Id.CommunityDinner)}", Id.CommunityDinner) { }
		public override string Descr => "Community Dinner";
	}

	private sealed class ErevShabbatSE : EventType
	{
		public ErevShabbatSE() : base($"{nameof(Id.ErevShabbat)}", Id.ErevShabbat) { }
		public override string Descr => "Erev Shabbat";
	}

	private sealed class MovieSE : EventType
	{
		public MovieSE() : base($"{nameof(Id.Movie)}", Id.Movie) { }
		public override string Descr => "Movie";
	}

	private sealed class GuestSpeakerSE : EventType
	{
		public GuestSpeakerSE() : base($"{nameof(Id.GuestSpeaker)}", Id.GuestSpeaker) { }
		public override string Descr => "Guest Speaker";
	}

	private sealed class OtherSE : EventType
	{
		public OtherSE() : base($"{nameof(Id.Other)}", Id.Other) { }
		public override string Descr => "Other";
	}

	private sealed class NewMoonSE : EventType
	{
		public NewMoonSE() : base($"{nameof(Id.NewMoon)}", Id.NewMoon) { }
		public override string Descr => "New Moon Gathering";
	}

	#endregion
}

