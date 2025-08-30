namespace LivingMessiah.Features.Sukkot.LandingPage.Constants;

public static class TShirts
{
	public static bool IsAvailableForSale { get; set; } = false;
	public static string ForSaleMessage { get; set; } = "T-Shirts are available for sale, click the image below.";
	public static string ComingSoonMessage { get; set; } = "T-Shirts not yet available, check back later. ";

	public static string ImgSrc
	{
		get
		{
			return Blob.UrlRoot("sukkot-2024-tee-shirts.jpg");
		}
	}
	public static string Href { get; set; } = "https://aboveallimages.net/shop/ols/categories/sukkot";
	public static string Title { get; set; } = "Sukkot T-Shirts for sale";
}
