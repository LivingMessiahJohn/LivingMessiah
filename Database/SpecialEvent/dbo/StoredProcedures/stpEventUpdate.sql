CREATE   PROCEDURE stpEventUpdate
(
	@Id as int,
	@EventDate smalldatetime, 
	@ShowBeginDate smalldatetime, 
	@ShowEndDate smalldatetime,
	@EventTypeId int,
	@Title nvarchar(100),
	@SubTitle nvarchar(100),
	@Description nvarchar(max),
	@ImageUrl nvarchar(150),
	@WebsiteUrl nvarchar(150),
	@WebsiteDescr nvarchar(150),
	@YouTubeId nvarchar(25)
)
AS
/*
	DECLARE @RC int
	EXEC @RC = stpEventUpdate 1, '2021-12-09', '2021-11-01', '2021-12-15', 6, 'Movie - Jaws', NULL, 'Desc'
	, 'ImageUrl', 'WebsiteUrl',  'WebsiteDescr', 'YouTubeId'

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure = 'stpEventUpdate'

	SELECT * FROM Event ORDER BY DateTime Desc
	SELECT * FROM vwSpecialEvent 
	
	GRANT EXECUTE ON stpEventUpdate TO InserUserName
*/

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName

BEGIN TRY

	UPDATE Event SET
		[DateTime]=@EventDate
	, ShowBeginDate=@ShowBeginDate
	,  ShowEndDate=@ShowEndDate
	,  EventTypeId=@EventTypeId
	,  Title=@Title
	,  SubTitle=@SubTitle
	,  [Description]=@Description
	,  ImageUrl=@ImageUrl
	,  WebsiteUrl=@WebsiteUrl
	,  WebsiteDescr=@WebsiteDescr
	,  YouTubeId=@YouTubeId
	WHERE Id = @Id

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

