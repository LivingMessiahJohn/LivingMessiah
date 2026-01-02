using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Features.WeeklyDownloads.Enums;

public abstract class Source : SmartEnum<Source>
{
	#region Id's
	private static class Id
	{
		internal const int Url = 1;
		internal const int LocalFile = 2;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Source Url = new UrlSE();
	public static readonly Source LocalFile = new LocalFileSE();
	// SE=SmartEnum
	#endregion

	private Source(string name, int value) : base(name, value) { }  // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string Description { get; }
	public abstract string Icon { get; }
	#endregion

	#region Private Instantiation

	private sealed class UrlSE : Source
	{
		public UrlSE() : base("Url", Id.Url) { }
		public override string Title => "Url";
		public override string Description => "iCloud attachment URL";
		public override string Icon => "fas fa-link";
	}

	private sealed class LocalFileSE : Source
	{
		public LocalFileSE() : base("LocalFile", Id.LocalFile) { }
		public override string Title => "File"; //Local File
		public override string Description => "File from your computer";
		public override string Icon => "fas fa-file";
	}

	#endregion

}

