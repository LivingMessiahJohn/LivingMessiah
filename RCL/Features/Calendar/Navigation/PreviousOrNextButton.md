
```html
@* DUMP ToDo Remove *@
@* *@

@if (DateOnlyRange is not null)
{
	if (Direction == Enums.Direction.Previous)
	{
		<p>@CurrentDateOnly.Year @DateOnlyRange.Start.Year @CurrentDateOnly.Month @DateOnlyRange.Start.Month </p>
	}
	else
	{
		<p class="text-end">@CurrentDateOnly.Year @DateOnlyRange.End.Year @CurrentDateOnly.Month @DateOnlyRange.End.Month </p>
	}
	<p class="text-center">currentKey: @currentKey; startKey: @startKey; endKey: @endKey</p>
}
```