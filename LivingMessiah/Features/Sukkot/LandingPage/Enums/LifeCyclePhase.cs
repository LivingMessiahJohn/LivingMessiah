using Ardalis.SmartEnum;

using static LivingMessiah.Helpers.Constants.Emoji;
using LivingMessiah.Helpers;

namespace LivingMessiah.Features.Sukkot.LandingPage.Enums;

public abstract class LifeCyclePhase : SmartEnum<LifeCyclePhase>
{
	#region Id's
	private static class Id
	{
		internal const int GetReady = 1;
		internal const int RegistrationOpen = 2;
		internal const int Over = 3;
	}
	#endregion

	#region Declared Public Instances
	public static readonly LifeCyclePhase GetReady = new GetReadySE();
	public static readonly LifeCyclePhase RegistrationOpen = new RegistrationOpenSE();
	public static readonly LifeCyclePhase Over = new OverSE();
	#endregion

	private LifeCyclePhase(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string SubTitle { get; }
	public abstract string Emoji { get; }
	#endregion

	#region Private Instantiation
	private sealed class GetReadySE : LifeCyclePhase
	{
		public GetReadySE() : base($"{nameof(Id.GetReady)}", Id.GetReady) { }
		public override string Title => "Sukkot Registration is getting close!";
		public override string SubTitle => "Start getting prepared and making plans";
		public override string Emoji => Surprise;
	}

	private sealed class RegistrationOpenSE : LifeCyclePhase
	{
		public RegistrationOpenSE() : base($"{nameof(Id.RegistrationOpen)}", Id.RegistrationOpen) { }
		public override string Title => "Sukkot 9999 Registration Open!";
		public override string SubTitle => "Please register as soon as possible thereby assisting leadership with planning";
		public override string Emoji => Happy;
	}

	private sealed class OverSE : LifeCyclePhase
	{
		public OverSE() : base($"{nameof(Id.Over)}", Id.Over) { }
		public override string Title => "Sukkot Is Over";
		public override string SubTitle => "We had a great time as usual, hope to see you next year.";
		public override string Emoji => Sad;
	}
	#endregion
}
