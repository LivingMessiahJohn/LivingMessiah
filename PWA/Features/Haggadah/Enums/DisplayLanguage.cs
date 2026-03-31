namespace PWA.Features.Haggadah.Enums;

using Ardalis.SmartEnum;


public abstract class DisplayLanguage : SmartEnum<DisplayLanguage>
{
	#region Id's
	private static class Id
	{
		internal const int English = 1;
		internal const int Spanish = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly DisplayLanguage English = new EnglishSE();
	public static readonly DisplayLanguage Spanish = new SpanishSE();
	#endregion

	private DisplayLanguage(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract string Description { get; }
	public abstract string Title { get; }
	public abstract string Pdf { get; }
	public abstract string PdfDescription { get; }
	#endregion

	private sealed class EnglishSE : DisplayLanguage
	{
		public EnglishSE() : base(nameof(English), Id.English) { }
		public override string Title => "English";
		public override string Description => "Display content in English only.";
		public override string Pdf => "haggadah-eng.pdf";
		public override string PdfDescription => "Haggadah English PDF";
	}

	private sealed class SpanishSE : DisplayLanguage
	{
		public SpanishSE() : base(nameof(Spanish), Id.Spanish) { }
		public override string Title => "Español";
		public override string Description => "Display content in Español only.";
		public override string Pdf => "haggadah-esp.pdf";
		public override string PdfDescription => "Haggadah Spanish PDF";
	}
}

