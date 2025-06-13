# Port Notes from .Net7 to DotNet9

## Ported Components
### Layout
- [x] This whole folder has been rewritten to some level.
- [x] Address
- [ ] LearnMoreModalTemplate: My approach to using needs to be rethought like into types, [see]("C:\Source\LivingeMessiahBackup\008-port-calendar-carousel\Wiki\Learn-More-Info-only-Bootstrap-Modal-no-code-behind-using-data-bs-and-fade.md") 
- [ ] PageHeader
- [ ] TableTemplate


## Ported Features
- [x] Calendar
- [x] Donations
- [x] FeastDayPlanner
- [x] Feasts
- [x] Leadership
- [x] LunarMonths
- [ ] Shavuot: Only `OmerGematriaDictionary.cs` ported, need to move elsewhere.
- [x] SiteMap
- [x] 

## Not yet ported
- [ ] Articles
- [ ] HeavensDeclare
- [ ] ImportantLinks
- [ ] Parasha: Hold off until a Razor Class Library is created from MHB
- [ ] Psalms
- [ ] ShabbatService: This need a rewrite.
- [ ] UpcomingEvents
- [ ] Pages\Account: Auth0 

## Not planning to port
- InDepthStudy: Were not doing it anymore, so not porting it.
- NavigationSearch: What is this?  Need to think about other solutions


## Porting elsewhere
- [ ] ArchivedVideos
- [ ] Calendar\ManageKeyDates
- [ ] Calendar\ManageParashaCalendar
- [ ] Contacts
- [ ] PsalmsAndProverbs
- [ ] SampleCode: What to do with this?  Maybe move to LivingMessiahSample project
    - [ ] 
- [ ] Shavuot: Most of this can be found in the new calendar design I want [see](C:\Source\LivingeMessiahBackup\Feature-Ideas\999-Calendar-based-on-Bootstrap-7-col-grid\999-Calendar-based-on-Bootstrap-7-col-grid.md)
- [ ] Sukkot: This will be a big lift 
- [ ] WindmillRanch:


## New features to add
- [x] Nav Replaced `Links` with the `Nav` gotten from `MHB`.
- [ ] Home Page
    - [ ] A Feature(s) Components, Next big event(s), see UpcomingEvents 
- [ ] Sitemap: Navigation Search:
        - [ ] Note, Blazored.Typeahead is "read only", need to find a replacement or write one.
- [ ] Azure Function? PWA? 
