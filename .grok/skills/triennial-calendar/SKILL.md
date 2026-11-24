---
name: triennial-calendar
description: "Attach each Living Messiah triennial Shabbat reading to its calendar Saturday: filter `Triennial.List` by month, show the Torah reference on the day, and show Torah, Haftorah, and Brit for that date. Use when the user runs /triennial-calendar, or asks to put parasha, triennial readings, a Torah portion, Haftorah, or Brit Chadashah on a calendar day or month grid. Reading dates are the seed date 2024-10-26 plus 7 times (Value - 1). Leave PDF downloads and the full parasha table alone."
---

# Triennial readings on a calendar

`Triennial` is an Ardalis `SmartEnum` of 155 Shabbat readings. It is already in the solution. Search for `class Triennial` and use that type. Do not regenerate the entries, and do not change `Value`, verse ranges, or the seed date unless the user asks.

Wire readings onto the calendar this app already has. Do not port `EventRecord`, `Filter`, `BaseEventsRepository`, or `RowsAndColumnsArray` into an app that does not already use them.

If those types exist, the reference wiring is `GetParasha` and `GetWholeParasha` in `RCL/Features/Calendar/Data/BaseEventsRepository.cs`, consumed by `RCL/Features/Calendar/RowsAndColumnsArray.razor`. Follow that wiring. Otherwise apply the rules below to this app's month grid and day detail.

## Dates

`Triennial.Date` is `Helpers.TriennialSeedDate` (`2024-10-26`) plus `7 * (Value - 1)`. `DateOnly` is that same civil date. `Value` 1 is the seed Saturday. Later values are the following Saturdays, one per week, through 155. At most one reading exists for a date. `SingleOrDefault` on `DateOnly` is safe.

Do not call `GetCurrentReading()` to fill a month. That helper returns only the next Saturday in `America/Phoenix`, or null. Use it only when the user asks for this week's reading.

## Month badges

For the selected year and month, take `Triennial.List` where `Date.Year` and `Date.Month` match that month. Ignore grid padding days that belong to the previous or next month when building this list.

Each match is one day badge. The badge markup is `SpanIconAndTextTorah`:

```html
<span class="badge bg-danger-subtle text-dark">{TorahAbrv}</span>
```

`TorahAbrv` is a leading space, the book abbreviation, a space, then `TorahVerse.ChapterVerse` (for example ` Gen 1:1-2:3`). Render the string as HTML. Center it (`justify-content-center`) when the day stacks several events.

The parasha event has no separate modal line. The reading text belongs only in the day detail below. Adding `AllAbrv` as an event line shows a blank event plus the Parasha section.

## Day detail

For any cell date, find `Triennial.List` where `DateOnly` equals that date. Return `AllAbrv`, or `""` when there is no match. Look up the cell's own date, including a Saturday that is only visible as padding from an adjacent month. That padding Saturday has detail text and does not get a badge from the selected month's list.

`AllAbrv` joins these parts with `"; "`:

- `{book abbreviation} {TorahVerse.ChapterVerse}`
- `Haftorah: {HaftorahAbrv}` when `HaftorahVerses` is not null
- `Brit: {BritAbrv}` when `BritVerses` is not null

`HaftorahAbrv` and `BritAbrv` join their ranges with `", "` as `{abbreviation} {ChapterVerse}`.

Show the detail only when the string is not whitespace. Label it `Parasha:`. A missing reading renders nothing.

Below the `sm` breakpoint, omit the badge and still supply the detail string. Keep that detail reachable on a Saturday whose only content is the reading.

## Dependencies

`TorahAbrv` and `AllAbrv` call `BibleBook.FromValue` on `VerseRange.BibleBook`. If those properties throw, the copied `BibleBook` or `VerseRange` type is missing or in the wrong namespace. Fix that reference. Do not add calendar types to work around it.

Leave PDF filenames, blob existence checks, Shabbat-card buttons, and the full parasha table out of this feature.
