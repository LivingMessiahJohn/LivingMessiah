

CREATE   PROCEDURE stpEventInsert
(
	@EventDate smalldatetime, 
	@ShowBeginDate smalldatetime, 
	@ShowEndDate smalldatetime,
	@EventTypeId int,
	@Title nvarchar(500),
	@SubTitle nvarchar(500),
	@Description nvarchar(max),
	@ImageUrl nvarchar(500),
	@WebsiteUrl nvarchar(500),
	@WebsiteDescr nvarchar(500),
	@YouTubeId nvarchar(25),
	@NewId as int OUTPUT
)
AS
/*
	DECLARE @RC int, @NewId int
	EXEC @RC = stpEventInsert 2021, NULL, '2021-12-09', '2021-11-01', '2021-12-15', 7, 'Title', NULL, 'Desc'
	, 'ImageUrl', 'WebsiteUrl',  'WebsiteDescr',  NULL
	, @NewId OUTPUT
	SELECT @NewId AS NewId

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure = 'stpEventInsert'

	SELECT * FROM Event ORDER BY DateTime Desc
	SELECT * FROM vwSpecialEvent 
	
	GRANT EXECUTE ON stpEventInsert TO InserUserName
*/

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName

BEGIN TRY

	INSERT INTO Event 
	([DateTime], ShowBeginDate,  ShowEndDate,  EventTypeId,  Title,  SubTitle,  Description,  ImageUrl,  WebsiteUrl,  WebsiteDescr,  YouTubeId)
	VALUES  
	(@EventDate, @ShowBeginDate, @ShowEndDate, @EventTypeId, @Title, @SubTitle,	@Description,	@ImageUrl, @WebsiteUrl, @WebsiteDescr,	@YouTubeId)
	
	SET @NewId = SCOPE_IDENTITY() 
	PRINT 'End of ' + @ProcName + ', @NewId=' + CAST(@NewId AS NVARCHAR(12));

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
 	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;



/*
SELECT * FROM EventType

Id Descr
-- ------------------
2	Mens Coffee Club
3	Ladies Evening Fellowship
4	Community Dinner
5	Erev Shabbat
6	Movie
7	Guest Speaker
8	Other
9 New Moon

*/
GO

