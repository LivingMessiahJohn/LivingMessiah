

```html
@using CalendarEnum = LivingMessiahAdmin.Features.Calendar.Enums
@using Microsoft.Extensions.Options
@using DateType = LivingMessiah.Features.Calendar.Enums.DateType


<p class="mt-3 mb-3 bg-info-subtle p-2">
	This page is a <i>Health Check</i> of the calendar dates.
	Specifically it makes sure the hard coded <code>DateTime Date</code> members found in
	<code>LivingMessiah.Features.Calendar.Enums</code> folder are consistent.
</p>

<h4 class="mb-1 mt-4  @DateType.FromValue(DateType.Month.Value).TextColor">
	@DateType.FromValue(DateType.Month.Value).Name
</h4>
<TableTemplate Items="@DataMonth">
	<TableHeader>
		<th class="text-info">Date</th>
		<th class="text-info">DateLocal</th>
		<th>DIFF</th>
		<th class="text-info">Enum</th>
	</TableHeader>
	<RowTemplate>
		<td>@context.Date.ToString(DateFormat.YYYY_MM_DD)</td>
		<td>
			@CalendarEnum.LunarMonth.FromValue(context.EnumId).Date.ToString(DateFormat.YYYY_MM_DD)
		</td>
		<td>
			@GetDateDiff(context.Date, CalendarEnum.LunarMonth.FromValue(context.EnumId).Date)
		</td>
		<td>@GetEnum(context.EnumId, DateType.Month)</td>
	</RowTemplate>
</TableTemplate>


<h4 class="mb-1 mt-4 @DateType.FromValue(DateType.Feast.Value).TextColor">
	@DateType.FromValue(DateType.Feast.Value).Name
</h4>
<TableTemplate Items="@DataFeast">
	<TableHeader>
		<th class="text-info">Date</th>
		<th class="text-info">DateLocal</th>
		<th>DIFF</th>
		<th class="text-info">Enum</th>
	</TableHeader>
	<RowTemplate>
		<td>@context.Date.ToString(DateFormat.YYYY_MM_DD)</td>
		<td>
			@CalendarEnum.FeastDay.FromValue(context.EnumId).Date.ToString(DateFormat.YYYY_MM_DD)
		</td>
		<td>
			@GetDateDiff(context.Date, CalendarEnum.FeastDay.FromValue(context.EnumId).Date)
		</td>
		<td>@GetEnum(context.EnumId, DateType.Feast)</td>
	</RowTemplate>
</TableTemplate>


<h4 class="mb-1 mt-4  @DateType.FromValue(DateType.Season.Value).TextColor">
	@DateType.FromValue(DateType.Season.Value).Name
</h4>
<TableTemplate Items="@DataSeason">
	<TableHeader>
		<th class="text-info">Date</th>
		<th class="text-info">DateLocal</th>
		<th>DIFF</th>
		<th class="text-info">Enum</th>
	</TableHeader>
	<RowTemplate>
		<td>@context.Date.ToString(DateFormat.YYYY_MM_DD)</td>
		<td>
			@CalendarEnum.Season.FromValue(context.EnumId).Date.ToString(DateFormat.YYYY_MM_DD)
		</td>
		<td>
			@GetDateDiff(context.Date, CalendarEnum.Season.FromValue(context.EnumId).Date)
		</td>
		<td>@GetEnum(context.EnumId, DateType.Season)</td>

	</RowTemplate>
</TableTemplate>
```

```csharp
@code {

	protected int YearId { get; set; }

	protected List<CalendarQuery>? DataMonth => CalendarDates.Get().Where(w => w.DateTypeId == DateType.Month.Value).ToList();
	protected List<CalendarQuery>? DataFeast => CalendarDates.Get().Where(w => w.DateTypeId == DateType.Feast.Value).ToList();
	protected List<CalendarQuery>? DataSeason => CalendarDates.Get().Where(w => w.DateTypeId == DateType.Season.Value).ToList();

	protected override void OnInitialized()
	{
		YearId = AppSettings!.Value.YearId;
		Logger!.LogDebug("{Method}, YearId: {YearId}", nameof(OnInitialized), YearId);
	}

	int GetDateDiff(DateTime db, DateTime loc)
	{
		TimeSpan difference = db - loc;
		return (int)difference.TotalDays;
	}

	string GetEnum(int enumId, DateType dateType)
	{
		if (dateType == DateType.Month)
		{
			return $"{enumId} - {Enums.LunarMonth.FromValue(enumId).FullName}";
		}
		else if (dateType == DateType.Feast)
		{
			return $"{enumId} - {Enums.FeastDay.FromValue(enumId).Transliteration}";
		}
		else if (dateType == DateType.Season)
		{
			return $"{enumId} - {Enums.Season.FromValue(enumId).Name}";
		}
		else
		{
			return "???";
		}

	}


}
```