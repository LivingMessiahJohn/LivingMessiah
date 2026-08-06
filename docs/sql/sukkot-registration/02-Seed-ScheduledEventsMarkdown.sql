/*
  Seed dbo.ScheduledEventsMarkdown when empty (schedule markdown for landing page / #198).

  Run against SukkotRegistration (local or Azure Free):

    sqlcmd -S "JohnsDellDT\SQLEXPRESS" -E -C -d SukkotRegistration -i docs/sql/sukkot-registration/02-Seed-ScheduledEventsMarkdown.sql

  Content adapted from issue #200 attachment 200-Create-ScheduledEventsMarkdown.md.
*/

SET NOCOUNT ON;

IF OBJECT_ID(N'dbo.ScheduledEventsMarkdown', N'U') IS NULL
BEGIN
    RAISERROR(N'dbo.ScheduledEventsMarkdown does not exist. Publish SukkotRegistration dacpac first.', 16, 1);
    RETURN;
END;

IF EXISTS (SELECT 1 FROM dbo.ScheduledEventsMarkdown WHERE [Lock] = 'X')
BEGIN
    PRINT 'ScheduledEventsMarkdown already has a row — no seed applied.';
    RETURN;
END;

INSERT INTO dbo.ScheduledEventsMarkdown ([Lock], Markdown, LastRevised)
VALUES (
    'X',
    N'**Table of Contents**
- [Camp set up](#camp-set-up)
- [Day 1](#day-1)
- [Day 2](#day-2)
- [Day 3](#day-3)
- [Day 4](#day-4)
- [Day 5](#day-5)
- [Day 6](#day-6)
- [Day 7](#day-7)
- [Day 8](#day-8)
- [Camp Clean up](#camp-clean-up)

## Camp set up
Friday September 25th - Arrivals - Camp set up

- **4:30 pm** - Prayer Walk around perimeter of camp
- **5:00 pm** - Community Erev Shabbat Dinner - Welcome Meal  
  Provided by Living Messiah  
  Grilled Hot Dogs, Baked Beans, Cole Slaw, Chips, Cookies
- **6:30 - 9:30 pm** - Opening Night Welcome and Campfire fellowship

## Day 1
Shabbat September 26th

- **Sunrise** - Morning Coffee Fellowship
- **10:30 am** - Camp Orientation - Mandatory for all campers
- **12:00 Noon - 1:00 pm** - Lunch
- **2:30 pm** - Afternoon Prayers
- **3:00 pm** - Shabbat Service
- **5:30 pm** - Oneg - Bring your winning crockpot of chili to share or a chili topping
- **6:30 - 9:30 pm** - Campfire Fellowship

## Day 2
Sunday September 27th
- **Sunrise** - Morning Coffee Fellowship
- **9:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **10:00 am** - Morning Prayer
- **10:30 am** - Round Table Topic Discussion
- **12:00 Noon - 1:00 pm** - Lunch
- **Afternoon Activities** - Hike Field Trip - Open Studio with April - Marketplace
- **5:00 - 6:30 pm** - Evening Meal
- **6:35 pm** - Evening Presentation - The Heavens Declare
- Campfire Fellowship

## Day 3
Monday September 28th
- **Sunrise** - Morning Coffee Fellowship
- **9:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **10:00 am** - Morning Prayer
- **10:30 am** - Round Table Topic Discussion
- **12:00 Noon - 1:00 pm** - Lunch
- **1:30 - 2:30 pm** - Afternoon Activity - Greenhouse Project
- **2:30 - 4:30 pm** - Open Studio with April
- **5:00 - 6:30 pm** - Evening Meal
- **6:35 pm** - Evening Presentation - The Path of Redemption - Part 1
- Campfire Fellowship

## Day 4
Tuesday September 29th
- **Sunrise** - Morning Coffee Fellowship
- **9:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **10:00 am** - Morning Prayer
- **10:30 am** - Tuesday Torah Study
- **1:30 - 2:30 pm** - Afternoon Activity - Greenhouse Project
- **2:30 - 4:30 pm** - Open Studio with April
- **5:00 - 6:30 pm** - Evening Meal
- **6:35 pm** - Evening Presentation - The Path of Redemption - Part 2
- Campfire Fellowship

**1 Hour Therapeutic Massage with Ludo**
$75, payment made to Ludo
- **10:30 am** - appointment 1
- **11:45 am** - appointment 2
- **1:00 pm** - appointment 3
- **2:15 pm** - appointment 4
- **3:30 pm** - appointment 5

## Day 5
Wednesday September 30th
- **Sunrise** - Morning Coffee Fellowship
- **9:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **10:00 am** - Morning Prayer
- **10:30 am** - Sprouted Whole Wheat Sourdough Class with Deborah Hargrove
- **12:00 Noon - 1:00 pm** - Lunch
- **1:00 - 2:00 pm** - Afternoon Activity - Greenhouse Project
- **2:00 - 3:00 pm** - Open Studio with April
- **3:00 pm** - Round Table Topic Discussion
- **5:00 - 6:30 pm** - Evening Meal
- **6:35 pm** - Mens Fellowship / Womans Fellowship
- Campfire Fellowship

## Day 6
Thursday October 1st

- **Sunrise** - Morning Coffee Fellowship
- **9:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **10:00 am** - Morning Prayer
- **10:30 am** - Round Table Topic Discussion
- **12:00 Noon - 1:00 pm** - Lunch
- **Afternoon Activity** - Worship in Dance Practice
- **4:30 - 6:30 pm** - R & R Pizza - Sierra Vista
- **6:35 pm** - Testimonies of Gratitude, Campfire Trivia

## Day 7
Friday October 2nd
- **Sunrise** - Morning Coffee Fellowship
- **9:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **10:00 am** - Morning Prayer
- **10:30 am** - Round Table Topic Discussion
- **12:00 Noon - 1:00 pm** - Lunch
- **Afternoon Activity** - Talent Practice
- **5:00 - 6:30 pm** - Erev Shabbat Dinner, Provided - Burrito, Tostada, Taco Bar -  
  Bring a Topping To Share
- **6:35 pm** - Talent Night
- **8:00 pm** - Campfire Fellowship

## Day 8
Shabbat October 3rd
- **Sunrise** - Morning Coffee Fellowship
- **10:00 am** - Cracking the Aleph Beit Code - Youth - All ages welcome
- **2:30 pm** - Afternoon Prayers
- **3:00 pm** - Shabbat Service
- **5:30 pm** - Oneg - Nacho Bar - Provided By Living Messiah
- **6:30 - 9:30 pm** - Campfire Fellowship Farewells

## Camp Clean up
Sunday October 4th
- Camp Clean up
- Departures
',
    GETDATE()
);

PRINT 'ScheduledEventsMarkdown seed inserted.';
