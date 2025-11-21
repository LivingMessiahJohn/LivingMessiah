# Enums.md

Dates for the new year are established after Sukkot. 
Once the dates are finalized you can run this code gen sql and add it to the Enum files.
Therefore a database connection is not necessary when viewing this information.

## `KeyDate.tvfDateCodeGen`
- Creates output like this...
```csharp
public override DateTime Date => Convert.ToDateTime("2023-10-14"); // EnumId: 1; Heshvan | Bul
```

```sql
CREATE  OR ALTER FUNCTION KeyDate.tvfDateCodeGen  (@YearId int, @DateTypeId int)
RETURNS TABLE AS RETURN 

/*

SELECT * FROM KeyDate.tvfDateCodeGen (2024, 1) -- Month
SELECT * FROM KeyDate.tvfDateCodeGen (2024, 2) -- Feast
THIS CODE GEN MOVED TO tvfSeasonDatesCodeGen   -- Season

GRANT SELECT ON KeyDate.tvfDateCodeGen TO [INSERT-USER-HERE]

*/

SELECT EnumId,
--CONVERT(varchar, Date, 23), 
'public override DateTime Date => Convert.ToDateTime("' + CONVERT(varchar, Date, 23) + '");' + ' // EnumId: ' + CAST(EnumId AS varchar(10)) + '; ' +  + Description AS CodeGen
FROM KeyDate.Calendar 
WHERE YearId = @YearId and DateTypeId=@DateTypeId
```


## `KeyDate.tvfSeasonDatesCodeGen`
- Code generation for `SeasonDates.cs` (RCL\Features\Calendar\Constants\SeasonDates.cs)
- Creates output like this...

```csharp
public static readonly DateTime Winter = Convert.ToDateTime("2024-12-20");
```


```sql
CREATE OR ALTER  FUNCTION KeyDate.tvfSeasonDatesCodeGen  (@YearId int)
RETURNS TABLE AS RETURN 

/*
SELECT * FROM KeyDate.tvfSeasonDatesCodeGen (2025) ORDER BY EnumId

GRANT SELECT ON KeyDate.tvfSeasonDatesCodeGen TO [INSERT-USER-HERE]

*/

SELECT EnumId,
'public static readonly DateTime ' + Description + ' = Convert.ToDateTime("' + CONVERT(varchar, Date, 23) + '");' AS CodeGen
FROM KeyDate.Calendar 
WHERE YearId = @YearId and DateTypeId=3 -- Season
UNION 
SELECT 5, 'public static readonly DateTime WinterNextYear = Convert.ToDateTime("' + CAST(@YearId AS NVARCHAR(12)) + '-12-20");'   AS CodeGen
```