# <mark>LMMAdmin</mark> Web App

In my infinite wisdom, I have decided to port "Admin" related processing out from the main app.

- Project Name: **`LMMAdmin`**
- Project Type: .Net 9 Blazor Web App SSR with Authorization
- Custom Domain: I could use the [org](https://livingmessiah.org/) to host the new design.

## Requirements
- Admin, Super User, and User with elevated roles can log in to perform admin tasks.
- Convert existing code that uses `Fluxor` for state management to use techniques have been using in MHB.

# Features
- Resource: C:\Source\LivingeMessiahBackup\Feature-Ideas

## Calendar

### ManageKeyDates 
- /Admin/KeyDatesEdit

### ManageParashaCalendar
This may already be done in MHB, so the dependency for `LivingMessiah.com` is probably going to be the RCL so I don't need to work about it.
- /ParashaCalendar

## PsalmsAndProverbs
I need to make this be ported to static content and remove the DB/Repository

- /PAndP or /PsalmsAndProverbs

### Database Dependencies

#### `vwPsalmsAndProverbs` `GetPsalmsAndProverbsList()`
```sql
--DECLARE @ShabbatDate smalldatetime = '2020-12-13 00:00:00';  
SELECT 
  ShabbatWeekId, ShabbatDate, ShabbatDateYMD
, PsalmsBCV, PsalmsChapter, PsalmsVerseCount, IsWholeChapter
--, PsalmsKJVHtmlConcat
, PsalmsUrl, PsalmsTitle
, ProverbsBCV, ProverbsChapter, ProverbsVerseCount
--, ProverbsKJVHtmlConcat
, ProverbsUrl
, TotalVerseCount
FROM Bible.vwPsalmsAndProverbs v 
WHERE ShabbatDate >= @ShabbatDate
ORDER BY ShabbatWeekId
```


#### `List<Psalms.PsalmsVM>>` `GetPsalms()`
```sql
SELECT 
  ps.Id, ps.BegVerse, ps.EndVerse, ps.EndVerse-ps.BegVerse + 1 AS VerseCount, ps.IsWholeChapter
, 'Psalms ' + CAST (ps.Chapter AS varchar(10)) + ':' + CAST (ps.BegVerse AS varchar(10)) + '-' + CAST (ps.EndVerse AS varchar(10)) AS BCV
, ps.Chapter
, ps.KJVHtmlConcat
, sw.Id AS ShabbatWeekId, CONVERT(VARCHAR(10), sw.ShabbatDate, 111) AS ShabbatDateYMD
FROM Bible.Psalms ps
	LEFT OUTER JOIN ShabbatWeek sw
		ON sw.PsalmsId = ps.Id
	INNER JOIN Bible.BookChapter bc 
		ON ps.BookId = bc.BookId AND ps.Chapter = bc.Chapter
ORDER BY ps.Chapter, ps.BegVerse
```

## SampleCode
What to do with this?  Maybe move to **LivingMessiahSample** project

- Index /SampleCode
-  📁 BibleBooks /BibleBooks
-  📁 BibleCascadingDDL /BibleCascadingDDL
-  📁 BibleSearch /BibleSearch
-  📁 Markdown /MarkdownTest
-  📁 Syncfusion_SfDropDownList /BBCP 
    - Bible Book Chapter DDL
-  📁 Syncfusion_SfDropDownList /BBCP 
- /Grid12Calendar


## SpecialEvents
Can I make `LivingMessiah.com` use Azure Functions for UpcomingEvents `/upcomingevents`
- /SpecialEvents
- `AuthorizeView`


### Repository
```csharp
Task<List<vwSpecialEvent>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd);
Task<FormVM?> GetEventById(int id);
Task<List<FormVM>> GetCurrentEvents();
```

## Shavuot
Shavuot: Most of this can be found in the new calendar design I want to develop

[see](C:\Source\LivingeMessiahBackup\Feature-Ideas\999-Calendar-based-on-Bootstrap-7-col-grid\999-Calendar-based-on-Bootstrap-7-col-grid.md)


### Other
Not sure if this is needed until and if I go back to keeping track of the weekly videos.
- [ ] ArchivedVideos: 


- 
