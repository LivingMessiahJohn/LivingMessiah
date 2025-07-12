# Port Notes from .Net7 to DotNet9

## New features Added
- [x] Home Page
    - [x] A Welcome Component with state
      - This is partially done, need to port this page to have interactivity on the client side.
    - [x] Upcoming Feasts Events  `Home/UpcomingFeasts/FeastCards.razor`
    - [x] `LunarMoonCard`
    - [ ] `SpecialEventsCard`
      - this is dependent on the CRUD functionality of the `SpecialEvents` to be ported, presumably to a LivingMessiahAdmin project.

- [x] `Nav<SmartEnum` Replaced `Links` with the `Nav` gotten from `MHB`.
- [x] NavBar with `More` and `Search`
    - [x] Need to try and fix 
- [ ] Parasha file upload automation
- [x] Podcast: Added as a share Icon?
- [x] PageHeader
- [ ] Dynamic Shabbat-o-Meter and Parasha File Download
    - Create a component `ShabbatMeterTeachingDownload.razor` 
    - This component is dynamic in that it will be a simple count down of the days of the week and will change on (or near) the Shabbat. When that occurs a `Lyrics, Liturgy and Teaching` download link will appear.


## Ported Components
### Layout
- [x] This whole folder has been rewritten and moved to root level.
- [x] Address
- [x] BibleQuote
- [ ] LearnMoreModalTemplate: 
    - [ ] My approach to using needs to be rethought like into types, [see]("C:\Source\LivingeMessiahBackup\008-port-calendar-carousel\Wiki\Learn-More-Info-only-Bootstrap-Modal-no-code-behind-using-data-bs-and-fade.md") 
- [x] Parasha
    - [ ] Re-visit this after I create a Razor Class Library is created from MHB
- [x] TableTemplate

## Ported Features
- [x] About (AboutUs redirects to About)
- [x] Calendar
- [x] Donations
- [x] FeastDayPlanner
- [x] Feasts
- [x] IntroductionAndWelcomePage 
    - [x] converted into a drop down Learn More
    - [ ] Needs to have State added to it.
- [x] Leadership
- [x] HeavensDeclare
- [x] <strike>NavigationSearch</strike> this is now NavSearchModal
- [x] ThresholdCovenant
- [x] LunarMonths
- [x] ShabbatService: This need a rewrite.
- [ ] Shavuot: Only `OmerGematriaDictionary.cs` ported, need to move elsewhere.
- [x] SiteMap
- [ ] Sukkot: lots to think about here, 

## Not yet ported
- [ ] AboutUs **The content of this page needs review**
- [ ] Articles
- [ ] ImportantLinks
- [ ] Psalms
- [ ] TorahTuesday: Don't know what to do with this yet.
- [ ] UpcomingEvents
- [ ] Pages\Account: Auth0 
- [ ] Sukkot AppState

## Not yet ported <mark>Other Folder<mark>
- [ ] Announcements
- [ ] BloodMoons
- [ ] Community
- [ ] FurtherStudies
- [ ] FurtherStudies.cs
- [ ] Gallery
- [ ] Location
- [ ] Mishpocha
- [ ] Psa075.html
- [ ] Store

## Not planning to port
- InDepthStudy: Were not doing it anymore, so not porting it.
- Search at the top of all pages


## Porting elsewhere
- [ ] ArchivedVideos
- [ ] Calendar\ManageKeyDates
- [ ] Calendar\ManageParashaCalendar
- [ ] Contacts
- [ ] PsalmsAndProverbs
- [ ] SampleCode: What to do with this?  Maybe move to LivingMessiahSample project
- [ ] SpecialEvents
- [ ] Shavuot: Most of this can be found in the new calendar design I want [see](C:\Source\LivingeMessiahBackup\Feature-Ideas\999-Calendar-based-on-Bootstrap-7-col-grid\999-Calendar-based-on-Bootstrap-7-col-grid.md)
- [ ] SukkotAdmin: This will be a big lift 
- [ ] WindmillRanch:

## New features to add
- [-] Sitemap: Navigation Search:
    - See [Prompt](C:\Source\LivingMessiahBackup\Feature-Ideas\999-Calendar-based-on-Bootstrap-7-col-grid\999-Calendar-based-on-Bootstrap-7-col-grid.md)
- [ ] Note, **Blazored.Typeahead** repo is "read only", need to find a replacement or write one.
- [ ] Azure Function? PWA? 


## New features to explore
See local folder C:\Source\LivingeMessiahBackup\Feature-Ideas\
- [ ] Simpler ReadOnly Calendar (maybe based on a version of Bootstrap 5.3 with 7 col grid)
