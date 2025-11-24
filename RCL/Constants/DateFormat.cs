
namespace RCL.Constants;

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

/*

### XRef

#### Features\

- FeastDayPlanner\Data\Service.cs(27):model.GregorianDate = today.ToString(DateFormat.FeastDayPlanner);
- FeastDayPlanner\EveningAndDay.razor(22):<div class="progress-bar bg-secondary-subtle"><h6 class="text-dark">@item.Date.AddDays(-1).ToString(DateFormat.FeastDayPlanner)</h6></div>
- FeastDayPlanner\EveningAndDay.razor(28):<div class="progress-bar bg-secondary-subtle"><h6 class="text-dark">@item.Date.ToString(DateFormat.FeastDayPlanner)</h6></div>
 `FeastDayPlanner` 

- Home\CurrentParashaTable.razor(22):<td>@context.Date.ToString(@DateFormat.YYYY_MMMM_DD)</td>
- Home\CurrentParashaTable.razor(23):<td>@context.Date.ToString(@DateFormat.YYYY_MM_DD)</td>

- Parasha\CurrentParasha.razor(21):<td>@CurrentReading!.Date.ToString(DateFormat.MMM_d)</td>
- Parasha\Enums\Triennial.cs(415):return $" {this.TriNum}, {BibleBook.FromValue(this.TorahVerse.BibleBook).Abrv} {this.TorahVerse.ChapterVerse}, {this.Date.ToString(DateFormat.YYYY_MM_DD)}";
- Parasha\Grid.razor(29):@context.TorahAbrv @context.Value @context.Date.ToString(DateFormat.YYYY_MM_DD)
- Parasha\Grid.razor(55):private string dateFormat => DateFormat.MMM_d;
- Parasha\PrintFriendlyTable.razor(24):<td>@item.Date.ToString(DateFormat.YYYY_MM_DD)</td>
- Parasha\Table.razor(47):@item.Date.ToString(DateFormat.MMM_d)

#### Helpers\
- DateUtil.cs(40):string gregorianDate = today.ToString(DateFormat.FeastDayPlanner);

*/