
## Refactor

all this crap should be put in a Service class

and DayilyWrapper and LuunarMonthCard should be much dumber

```html

@if (String.IsNullOrEmpty(ImageFile))

<div class="alert alert-danger">
	<div class="d-flex justify-content-center">
		<small>LunarMonth: @LunarMonth!.Name</small>
		<small>&nbsp;|&nbsp;</small>
		<small>ImageFile: @ImageFile</small>
	</div>
</div>

<div class="alert alert-danger mt-2">
	<div class="d-flex justify-content-evenly">
		<small>DaysToGo: @DaysToGo</small>
		<small>DaysOld: @DaysOld => @DaysOldMessage</small>
	</div>
</div>

}

```


``csharp

	DaysOld = 30 - (LunarMonth.Date - dateAzUTC).Days;
	DaysOld = 30 - LunarMonth.Date.DayNumber - dateAzUTC.DayNumber;

```