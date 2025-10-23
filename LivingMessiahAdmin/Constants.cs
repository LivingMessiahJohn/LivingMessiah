namespace LivingMessiahAdmin;

public static class Emoji
{
	public const string NotYetImplemented = $"Not yet implemented {Sad}";
	public const string Happy = @"ʘ‿ʘ";
	public const string Sad = @"(◡︵◡)";
	public const string Shrug = @"¯\_(ツ)_/¯";
	public const string Surprise = @"O_O";
}

public static class Global
{
	public const string ToastShowError = "An invalid operation occurred, contact your administrator";
}

public static class Utc
{
	public const int ArizonaUtcMinus7 = -7;
}

public static class DateFormat
{
	public const string ddd_mm_dd = "ddd, MM/dd";  //ddd, MM/dd/yyyy
	public const string mm_dd = "MM/dd";
	public const string MMM_d = "MMM d";
	public const string MM_dd_HH_mm = "MM/dd HH:mm";
	public const string MM_dd_hh_mm = "MM/dd hh:mm";
	public const string dd = "dd";
	public const string dddd_dd_MMMM = "dddd, dd MMMM";
	public const string dddd_MMMM_dd = "dddd, MMMM dd ";
	public const string ddd_MMMM_dd_YYYY = "ddd, MMMM dd, yyyy";
	public const string ddd_MMM_dd_YYYY = "ddd, MMM dd, yyyy";
	public const string YYYY_MM_DD = "yyyy-MM-dd";
	public const string YYYY_MMMM_DD = "yyyy MMMM dd";
	public const string FeastDayPlanner = "ddd, MMM dd";
}

public static class CurrencyFormat
{
	public const string NoCents = "{0:C0}"; // doesn't work use a property like below
	/*
		public string AmountNoCents { get { return String.Format("{0:C0}", Amount); }	}
	*/
}

/*
Not Used in Admin
public static class Blobs
public static class Emails
public static class SocialMedia
*/

