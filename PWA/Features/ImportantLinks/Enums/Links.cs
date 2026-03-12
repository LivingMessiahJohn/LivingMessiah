using Ardalis.SmartEnum;

namespace PWA.Features.ImportantLinks.Enums;

public abstract class Links : SmartEnum<Links>
{

	#region Id's
	private static class Id
	{
		internal const int PhysicianHealThyself = 1;
		internal const int MyHebrewBible = 2;
		internal const int TorahTogether = 3;
		internal const int BYNA = 4;
		internal const int EtzBneyYosef = 5;
		internal const int Hebrew4Christians = 6;
		internal const int IsraeliteReturn = 7;
		internal const int AboveAllImages = 8;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Links PhysicianHealThyself = new PhysicianHealThyselfSE();
	public static readonly Links MyHebrewBible = new MyHebrewBibleSE();
	public static readonly Links TorahTogether = new TorahTogetherSE();
	public static readonly Links BYNA = new BYNASE();
	public static readonly Links EtzBneyYosef = new EtzBneyYosefSE();
	public static readonly Links Hebrew4Christians = new Hebrew4ChristiansSE();
	public static readonly Links IsraeliteReturn = new IsraeliteReturnSE();
	public static readonly Links AboveAllImages = new AboveAllImagesSE();
	// SE=SmartEnum
	#endregion

	private Links(string name, int value) : base(name, value) { } // ConstructorSmartEnumTemplate

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string Image { get; }
	public abstract string Url { get; }
	public abstract string UrlSimple { get; }
	public abstract string UrlPacketDownload { get; }
	#endregion

	#region Private Instantiation

	private sealed class PhysicianHealThyselfSE : Links
	{
		public PhysicianHealThyselfSE() : base($"{nameof(Id.PhysicianHealThyself)}", Id.PhysicianHealThyself) { }
		public override string Title => "Early Treatment Report";
		public override string Image => "earlytreatmentreport.jpg";
		public override string Url => "https://earlytreatmentreport.com/";
		public override string UrlSimple => "EarlyTreatmentReport.com";
		public override string UrlPacketDownload => "https://livingmessiahstorage.blob.core.windows.net/pdfs/EarlyTreatmentReport-2021-10-01.pdf";
	}

	private sealed class MyHebrewBibleSE : Links
	{
		public MyHebrewBibleSE() : base($"{nameof(Id.MyHebrewBible)}", Id.MyHebrewBible) { }
		public override string Title => "My Hebrew Bible";
		public override string Image => "myhebrewbible.png";
		public override string Url => "https://myhebrewbible.com";
		public override string UrlSimple => "MyHebrewBible.com";
		public override string UrlPacketDownload => "";
	}

	private sealed class TorahTogetherSE : Links
	{
		public TorahTogetherSE() : base($"{nameof(Id.TorahTogether)}", Id.TorahTogether) { }
		public override string Title => "Torah Together";
		public override string Image => "torahtogether.jpg";
		public override string Url => "https://www.torahtogether.com";
		public override string UrlSimple => "www.TorahTogether.com";
		public override string UrlPacketDownload => "";
	}

	private sealed class BYNASE : Links
	{
		public BYNASE() : base($"{nameof(Id.BYNA)}", Id.BYNA) { }
		public override string Title => "B'ney Yosef North America";
		public override string Image => "BYNAHomePage.jpg";
		public override string Url => "https://www.bneyyosefna.com";
		public override string UrlSimple => "www.BneyYosefNA.com";
		public override string UrlPacketDownload => "";
	}

	private sealed class EtzBneyYosefSE : Links
	{
		public EtzBneyYosefSE() : base($"{nameof(Id.EtzBneyYosef)}", Id.EtzBneyYosef) { }
		public override string Title => "Etz Bney Yosef";
		public override string Image => "EBYHomePage.jpg";
		public override string Url => "http://www.etzbneyyosef.com/";
		public override string UrlSimple => "www.EtzBneyYosef.com";
		public override string UrlPacketDownload => "";
	}

	private sealed class Hebrew4ChristiansSE : Links
	{
		public Hebrew4ChristiansSE() : base($"{nameof(Id.Hebrew4Christians)}", Id.Hebrew4Christians) { }
		public override string Title => "Hebrew 4 Christians";
		public override string Image => "Hebrew4Christians.jpg";
		public override string Url => "https://www.hebrew4christians.com";
		public override string UrlSimple => "www.Hebrew4Christians.com";
		public override string UrlPacketDownload => "";
	}

	private sealed class IsraeliteReturnSE : Links
	{
		public IsraeliteReturnSE() : base($"{nameof(Id.IsraeliteReturn)}", Id.IsraeliteReturn) { }
		public override string Title => "Israelite Return";
		public override string Image => "IsrRtnHomePage.jpg";
		public override string Url => "http://israelitereturn.com/";
		public override string UrlSimple => "www.IsraeliteReturn.com";
		public override string UrlPacketDownload => "";
	}

	private sealed class AboveAllImagesSE : Links
	{
		public AboveAllImagesSE() : base($"{nameof(Id.AboveAllImages)}", Id.AboveAllImages) { }
		public override string Title => "Israelite Return";
		public override string Image => "IsrRtnHomePage.jpg";
		public override string Url => "http://aboveallimages.net/";
		public override string UrlSimple => "www.AboveAllImages.net";
		public override string UrlPacketDownload => "";
	}
	
	#endregion

}

