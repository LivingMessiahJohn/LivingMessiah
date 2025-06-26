using Ardalis.SmartEnum;

namespace LivingMessiah.Features.Liturgy.Enums;

public abstract class Content : SmartEnum<Content>
{
	#region Id's
	private static class Id
	{
		internal const int CallToService = 1;
		internal const int OpeningAdoration = 2;
		internal const int PraiseAndWorship = 3;
		internal const int CommunityPrayer = 4;
		internal const int ChildrensBlessing = 5;
		internal const int OsehShalom = 6; 
		internal const int Shema = 7;  // NOT SPLIT OUT
		internal const int Vahavta = 8;   // NOT SPLIT OUT
		internal const int PsalmsAndProverbs = 9;
		internal const int Announcements = 10;
		internal const int EtzChaim = 11;  // SiddurPrayer = 11.5;
		internal const int TorahParahsa = 12; 
		internal const int Midrash = 13; 
		internal const int Avinu = 14;
		internal const int AhronicBlessing = 15;
		internal const int WineAndBread = 16;
		internal const int ThankYou = 17;
	}
	#endregion


	#region  Declared Public Instances
	public static readonly Content CallToService = new CallToServiceSE();
	public static readonly Content OpeningAdoration = new OpeningAdorationSE();
	public static readonly Content PraiseAndWorship = new PraiseAndWorshipSE();
	public static readonly Content CommunityPrayer = new CommunityPrayerSE();
	public static readonly Content ChildrensBlessing = new ChildrensBlessingSE();  
	public static readonly Content OsehShalom = new OsehShalomSE();
	public static readonly Content Shema = new ShemaSE();
	public static readonly Content Vahavta = new VahavtaSE();
	public static readonly Content PsalmsAndProverbs = new PsalmsAndProverbsSE();
	public static readonly Content Announcements = new AnnouncementsSE();
	public static readonly Content EtzChaim = new EtzChaimSE();
	public static readonly Content TorahParahsa = new TorahParahsaSE();
	public static readonly Content Midrash = new MidrashSE();
	public static readonly Content Avinu = new AvinuSE();
	public static readonly Content AhronicBlessing = new AhronicBlessingSE();
	public static readonly Content WineAndBread = new WineAndBreadSE();
	public static readonly Content ThankYou = new ThankYouSE();

	#endregion

	private Content(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string GraphicUrl { get; }
	public abstract string Time { get; }
	#endregion

	#region Private Instantiation

	private sealed class CallToServiceSE : Content
	{
		public override string Title => "Call to Service";
		public override string GraphicUrl => "shofar-call-to-service-with-time-1024-385.jpeg";
		public override string Time => "3:00 pm";
		public CallToServiceSE() : base($"{nameof(Id.CallToService)}", Id.CallToService) { }
	}

	private sealed class OpeningAdorationSE : Content
	{
		public OpeningAdorationSE() : base($"{nameof(Id.OpeningAdoration)}", Id.OpeningAdoration) { }
		public override string Title => "Opening Adoration";
		public override string GraphicUrl => "blessed-be-he-who-spoke-1024-385.jpeg";
		public override string Time => "3:01 pm";
	}

	// Add Prayer of Thanks giving |  Toda Raba! 3:02

	private sealed class PraiseAndWorshipSE : Content
	{
		public PraiseAndWorshipSE() : base($"{nameof(Id.PraiseAndWorship)}", Id.PraiseAndWorship) { }
		public override string Title => "Praise and Worship";
		public override string GraphicUrl => "praise-and-worship-1024-385.jpeg"; 
		public override string Time => "3:05 pm";
	}

	private sealed class CommunityPrayerSE : Content
	{
		public CommunityPrayerSE() : base($"{nameof(Id.CommunityPrayer)}", Id.CommunityPrayer) { }
		public override string Title => "Community Prayer";
		public override string GraphicUrl => "community-prayer-1024-385.jpeg"; 
		public override string Time => "3:33 pm";
	}

	private sealed class ChildrensBlessingSE : Content
	{
		public ChildrensBlessingSE() : base($"{nameof(Id.ChildrensBlessing)}", Id.ChildrensBlessing) { }
		public override string Title => "Children's Blessing";
		public override string GraphicUrl => "childrens-blessing-1024-385.jpeg";
		public override string Time => "3:46 pm";
	}

	private sealed class OsehShalomSE : Content
	{
		public OsehShalomSE() : base($"{nameof(Id.OsehShalom)}", Id.OsehShalom) { }
		public override string Title => "Oseh Shalom";
		public override string GraphicUrl => "oseh-shalom-1024-385.jpeg"; 
		public override string Time => "3:48 pm";
	}

	private sealed class ShemaSE : Content
	{
		public ShemaSE() : base($"{nameof(Id.Shema)}", Id.Shema) { }
		public override string Title => "Shema";
		public override string GraphicUrl => "shema-yisrael-brown-1024-385.jpeg"; //shema-yisrael-pink-1024-385.jpeg
		public override string Time => "3:50 pm";
	}

	private sealed class VahavtaSE : Content
	{
		public VahavtaSE() : base($"{nameof(Id.Vahavta)}", Id.Vahavta) { }
		public override string Title => "Vahavta";
		public override string GraphicUrl => "vahavta-1024-385.jpeg";
		public override string Time => "3:51 pm";
	}

	private sealed class PsalmsAndProverbsSE : Content
	{
		public PsalmsAndProverbsSE() : base($"{nameof(Id.PsalmsAndProverbs)}", Id.PsalmsAndProverbs) { }
		public override string Title => "Psalms and Proverbs";
		public override string GraphicUrl => "psalms-and-proverbs-1024-385.jpeg";
		public override string Time => "3:53 pm";
	}

	private sealed class AnnouncementsSE : Content
	{
		public AnnouncementsSE() : base($"{nameof(Id.Announcements)}", Id.Announcements) { }
		public override string Title => "Announcements";
		public override string GraphicUrl => "announcements-1024-385.jpeg"; 
		public override string Time => "3:55 pm";
	}

	// Donation

	private sealed class EtzChaimSE : Content
	{
		public EtzChaimSE() : base($"{nameof(Id.EtzChaim)}", Id.EtzChaim) { }
		public override string Title => "Etz Chaim";
		public override string GraphicUrl => "etz-chaim-1024-385.jpeg"; 
		public override string Time => "4:00 pm";
	}

	private sealed class TorahParahsaSE : Content
	{
		public TorahParahsaSE() : base($"{nameof(Id.TorahParahsa)}", Id.TorahParahsa) { }
		public override string Title => "Torah Parahsa";
		public override string GraphicUrl => "torah-parahsa-green-1024-385.jpeg"; // torah-parahsa-purple-1024-385.jpeg
		public override string Time => "4:02 pm";
	}

	private sealed class MidrashSE : Content
	{
		public MidrashSE() : base($"{nameof(Id.Midrash)}", Id.Midrash) { }
		public override string Title => "Midrash";
		public override string GraphicUrl => "torah-time-720-508.jpg";  // midrash-1024-385.jpeg ... can't find
		public override string Time => "4:03 pm";
	}

	private sealed class AvinuSE : Content
	{
		public AvinuSE() : base($"{nameof(Id.Avinu)}", Id.Avinu) { }
		public override string Title => "Avinu";
		public override string GraphicUrl => "avinu-1024-385.jpeg"; 
		public override string Time => "5:43 pm";
	}

	private sealed class AhronicBlessingSE : Content
	{
		public AhronicBlessingSE() : base($"{nameof(Id.AhronicBlessing)}", Id.AhronicBlessing) { }
		public override string Title => "Aaronic Blessing";
		public override string GraphicUrl => "ahronic-blessing-1024-385.jpeg";
		public override string Time => "5:45 pm";
	}

	private sealed class WineAndBreadSE : Content
	{
		public WineAndBreadSE() : base($"{nameof(Id.WineAndBread)}", Id.WineAndBread) { }
		public override string Title => "Wine and Bread";
		public override string GraphicUrl => "wine-and-bread-1024-385.jpeg"; 
		public override string Time => "5:46 pm";
	}

	private sealed class ThankYouSE : Content
	{
		public ThankYouSE() : base($"{nameof(Id.ThankYou)}", Id.ThankYou) { }
		public override string Title => "Thank You";
		public override string GraphicUrl => "thank-you-1024-385.jpeg"; 
		public override string Time => "5:47 pm";
	}

	#endregion
}
/*
private const string shabbatService = "https://livingmessiahstorage.blob.core.windows.net/images/shabbatservice/";
<img src='@Blobs.UrlShabbatService(Section!.GraphicUrl)' alt="@Section.Title" class="img-fluid rounded mt-4" />

 public abstract MarkupString EngTitle { get; }

		public override MarkupString EngTitle => (MarkupString)(@"
			<h2 class='text-center text-danger fw-bold mb-3'>
				<span class='text-dark'>Messiah, Savior, Redeemer</span>
			</h2>
			");
public abstract string ToggleId { get; }
public override string Descr => "Oseh Shalom + Shema + Vahavta ";

 */


//<sup> <span class='text-dark'><span class='badge rounded-pill bg-danger'>2</span> <i class='text-danger fas fa-wine-glass-alt'></i> </sup>	
// <span class='text-dark'><span class='badge rounded-pill bg-info'>1</span> 
// Ignore Spelling: Matzah, Charoset