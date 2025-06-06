using Ardalis.SmartEnum;

namespace LivingMessiah.Enums;

[Flags]
public enum PageListType
{
	None = 0,
	SitemapPage = 1,
	Footer = 2,
	Layout = 4,
	HealthCheck = 8,
	Reply = 16,
	LayoutXs = 32,
	LayoutSm = 64,
	LayoutMd = 128,
	LayoutLg = 256,
	LayoutXl = 512,
}


public abstract class Nav : SmartEnum<Nav>
{

	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int Donate = 2;
		internal const int DonateReplyConfirm = 3;
	}
	#endregion


	#region  Declared Public Instances
	public static readonly Nav Home = new HomeSE();
	public static readonly Nav Donate = new DonateSE();
	public static readonly Nav DonateReplyConfirm = new DonateReplyConfirmSE();
	#endregion

	private Nav(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Sort { get; }
	public abstract string HomeTitleSuffix { get; }
	public abstract string HomeFloatRightHebrew { get; }
	public abstract bool IsPartOfList(PageListType pageListType);
	public abstract PageListType PageListType { get; }
	public abstract bool Disabled { get; }

	public string DisabledHtml
	{
		get
		{
			return $"{(Disabled ? " disabled" : "")}";
		}
	}

	public string TextColor
	{
		get
		{
			return $"{(Disabled ? " text-black-50" : "text-primary")}";
		}
	}

	#endregion


	#region Private Instantiation

	private sealed class HomeSE : Nav
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Home";
		public override string Icon => "fas fa-home";
		public override int Sort => Id.Home;
		public override string HomeTitleSuffix => " bayit H1004";
		public override string HomeFloatRightHebrew => "בַּיִת";
		public override PageListType PageListType => PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class DonateSE : Nav
	{
		public DonateSE() : base($"{nameof(Id.Donate)}", Id.Donate) { }
		public override string Index => "/Donate";
		public override string Title => "Donate";
		public override string Icon => "fas fa-donate";
		public override int Sort => Id.Donate;
		public override string HomeTitleSuffix => " Tsadik H6662";
		public override string HomeFloatRightHebrew => "צַדִּיק";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class DonateReplyConfirmSE : Nav
	{
		public DonateReplyConfirmSE() : base($"{nameof(Id.DonateReplyConfirm)}", Id.DonateReplyConfirm) { }
		public override string Index => "/confirm_donation.html";
		public override string Title => "Donation Confirmed";
		public override string Icon => "fab fa-cc-stripe";
		public override int Sort => Id.DonateReplyConfirm;
		public override string HomeTitleSuffix => "";
		public override string HomeFloatRightHebrew => "";

		public override PageListType PageListType => PageListType.Reply;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false; // N/A
	}


	// 

	#endregion
}