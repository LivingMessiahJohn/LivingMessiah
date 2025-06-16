# Port Notes from .Net7 to DotNet9

## New features Added
- [x] `Nav<SmartEnum` Replaced `Links` with the `Nav` gotten from `MHB`.
- [x] NavBar with `More` and `Search`
    - [x] Need to try and fix 
- [-] Parasha file upload automation
- [x] Podcast: Added as a share Icon?
- [x] PageHeader

## Ported Components
### Layout
- [x] This whole folder has been rewritten and moved to root level.
- [x] Address
- [x] BibleQuote
- [ ] LearnMoreModalTemplate: 
    - [ ] My approach to using needs to be rethought like into types, [see]("C:\Source\LivingeMessiahBackup\008-port-calendar-carousel\Wiki\Learn-More-Info-only-Bootstrap-Modal-no-code-behind-using-data-bs-and-fade.md") 
- [x] TableTemplate

## Ported Features
- [x] Calendar
- [x] Donations
- [x] FeastDayPlanner
- [x] Feasts
- [x] Leadership
- [x] HeavensDeclare
- [x] <strike>NavigationSearch</strike> this is now NavSearchModal
- [x] ThresholdCovenant
- [x] LunarMonths
- [ ] Shavuot: Only `OmerGematriaDictionary.cs` ported, need to move elsewhere.
- [x] SiteMap
- [ ] Sukkot: lots to think about here, 

## Not yet ported
- [ ] Articles
- [ ] ImportantLinks
- [ ] Parasha: Hold off until a Razor Class Library is created from MHB
- [ ] Psalms
- [ ] ShabbatService: This need a rewrite.
- [ ] TorahTuesday: Don't know what to do with this yet.
- [ ] UpcomingEvents
- [ ] Pages\Account: Auth0 

## Not yet ported <mark>Other Folder<mark>
- [ ] About
- [ ] AboutUs
- [ ] Announcements
- [ ] BloodMoons
- [ ] Community
- [ ] FurtherStudies
- [ ] FurtherStudies.cs
- [ ] Gallery
- [ ] IntroductionAndWelcomePage
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
- [-] Home Page
    - [ ] A Feature(s) Components, Next big event(s), see UpcomingEvents 
- [-] Sitemap: Navigation Search:
    - See [Prompt](C:\Source\LivingMessiahBackup\Feature-Ideas\999-Calendar-based-on-Bootstrap-7-col-grid\999-Calendar-based-on-Bootstrap-7-col-grid.md)
- [ ] Note, **Blazored.Typeahead** repo is "read only", need to find a replacement or write one.
- [ ] Azure Function? PWA? 


## New features to explore
See local folder C:\Source\LivingeMessiahBackup\Feature-Ideas\

- Moon Phases
- Simplier ReadOnly Calendar (maybe based on Bootstrap 7 col grid)
