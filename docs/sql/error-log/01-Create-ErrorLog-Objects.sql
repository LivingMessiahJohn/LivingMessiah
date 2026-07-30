/*
  ErrorLog + dependent objects
  Source: LivingMessiah (local SQLEXPRESS / same schema as Azure LivingMessiah)
  Target: SpecialEvent (or any new Azure SQL DB that needs Admin Database error UI)

  Objects (create order):
    1. dbo.ErrorLog              table
    2. dbo.udfDateDiff_D_H_S_M   scalar function (used by view)
    3. dbo.zvwErrorLog           view
    4. dbo.stpPrintError         proc
    5. dbo.stpLogError           proc  (depends on ErrorLog + stpPrintError)
    6. dbo.stpLogErrorEmpty      proc  (Admin "empty log")
    7. dbo.stpLogErrorTest       proc  (Admin "test log")

  Admin consumers (ConnectionStrings:LivingMessiah-style DB):
    - SELECT TOP 75 * FROM zvwErrorLog ORDER BY ErrorLogID DESC
    - EXEC dbo.stpLogErrorEmpty
    - EXEC dbo.stpLogErrorTest

  Usage:
    sqlcmd -S lmm-azure-sql.database.windows.net -d SpecialEvent -U <user> -P <pwd> -i 01-Create-ErrorLog-Objects.sql
    -- or open in SSMS connected to SpecialEvent and execute
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/*--------------------------------------------------------------------------
  1. Table: dbo.ErrorLog
--------------------------------------------------------------------------*/
IF OBJECT_ID(N'dbo.ErrorLog', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ErrorLog]
    (
        [ErrorLogID]     INT            IDENTITY (1, 1) NOT NULL,
        [ErrorTime]      DATETIME       NOT NULL
            CONSTRAINT [DF_ErrorLog_ErrorTime] DEFAULT (GETDATE()),
        [UserName]       SYSNAME        NOT NULL,
        [ErrorNumber]    INT            NOT NULL,
        [ErrorSeverity]  INT            NULL,
        [ErrorState]     INT            NULL,
        [ErrorProcedure] NVARCHAR (126) NULL,
        [ErrorLine]      INT            NULL,
        [ErrorMessage]   NVARCHAR (4000) NOT NULL,
        [BatchLogJobId]  INT            NULL
            CONSTRAINT [DF_ErrorLog_JobId] DEFAULT ((0)),
        CONSTRAINT [PK_ErrorLog_ErrorLogID] PRIMARY KEY CLUSTERED ([ErrorLogID] ASC)
    );
END
GO

/*--------------------------------------------------------------------------
  2. Function: dbo.udfDateDiff_D_H_S_M  (zvwErrorLog.ErrorTime2)
--------------------------------------------------------------------------*/
CREATE OR ALTER FUNCTION [dbo].[udfDateDiff_D_H_S_M]
(
    @BegDate DATETIME,
    @EndDate DATETIME
)
RETURNS VARCHAR(200)
AS
/*
  SELECT dbo.udfDateDiff_D_H_S_M('2023-7-7 10:00:00', '2023-08-15 15:30:45')
*/
BEGIN
    DECLARE @s NVARCHAR(200) = N'';

    -- Calculate the time difference in seconds
    DECLARE @TimeDiffInSeconds INT = DATEDIFF(SECOND, @BegDate, @EndDate);

    -- Calculate days, hours, minutes, and seconds
    DECLARE @Days INT = @TimeDiffInSeconds / 86400;
    DECLARE @Hours INT = (@TimeDiffInSeconds % 86400) / 3600;
    DECLARE @Minutes INT = ((@TimeDiffInSeconds % 86400) % 3600) / 60;
    DECLARE @Seconds INT = ((@TimeDiffInSeconds % 86400) % 3600) % 60;

    SET @s = CONCAT(
        @Days,   N'<sub><b>d</b>&nbsp;</sub>',
        @Hours,  N'<sub><b>h</b>&nbsp;</sub>',
        @Minutes,N'<sub><b>m</b>&nbsp;</sub>',
        @Seconds,N'<sub><b>s</b></sub>');

    RETURN @s;
END
GO

/*--------------------------------------------------------------------------
  3. View: dbo.zvwErrorLog
--------------------------------------------------------------------------*/
CREATE OR ALTER VIEW [dbo].[zvwErrorLog]
AS
/*
  SELECT * FROM zvwErrorLog
  WHERE ErrorProcedure LIKE '%stpRegistrationDelete'
*/
SELECT TOP (1000)
    ErrorProcedure,
    ErrorNumber,
    ErrorLine,
    ErrorMessage,
    ErrorLogID,
    ErrorTime,
    dbo.udfDateDiff_D_H_S_M(ErrorTime, GETDATE()) AS ErrorTime2
FROM ErrorLog
ORDER BY ErrorLogID DESC;
GO

/*--------------------------------------------------------------------------
  4. Proc: dbo.stpPrintError
  Prints error info from within a CATCH block.
--------------------------------------------------------------------------*/
CREATE OR ALTER PROCEDURE [dbo].[stpPrintError]
    @BatchLogJobId INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    -- Print error information.
    PRINT 'Error ' + CONVERT(VARCHAR(50), ERROR_NUMBER()) +
          ', Severity ' + CONVERT(VARCHAR(5), ERROR_SEVERITY()) +
          ', State ' + CONVERT(VARCHAR(5), ERROR_STATE()) +
          ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') +
          ', Line ' + CONVERT(VARCHAR(5), ERROR_LINE()) +
          ', [BatchLogJob.Id] ' + CONVERT(VARCHAR(5), @BatchLogJobId);
    PRINT ERROR_MESSAGE();
END;
GO

/*--------------------------------------------------------------------------
  5. Proc: dbo.stpLogError
  Inserts a row into ErrorLog from within a CATCH block.
--------------------------------------------------------------------------*/
CREATE OR ALTER PROCEDURE [dbo].[stpLogError]
    @BatchLogJobId INT = 0,
    @ErrorLogID INT = 0 OUTPUT -- ErrorLogID of the row inserted
AS
BEGIN
    SET NOCOUNT ON;

    -- Output parameter value of 0 indicates that error information was not logged
    SET @ErrorLogID = 0;

    BEGIN TRY
        -- Return if there is no error information to log
        IF ERROR_NUMBER() IS NULL
            RETURN;

        -- Return if inside an uncommittable transaction.
        IF XACT_STATE() = -1
        BEGIN
            PRINT 'Cannot log error since the current transaction is in an uncommittable state. '
                + 'Rollback the transaction before executing stpLogError in order to successfully log error information.';
            RETURN;
        END

        INSERT [dbo].[ErrorLog]
        (
            [UserName],
            [ErrorNumber],
            [ErrorSeverity],
            [ErrorState],
            [ErrorProcedure],
            [ErrorLine],
            [ErrorMessage],
            BatchLogJobId
        )
        VALUES
        (
            CONVERT(SYSNAME, CURRENT_USER),
            ERROR_NUMBER(),
            ERROR_SEVERITY(),
            ERROR_STATE(),
            ERROR_PROCEDURE(),
            ERROR_LINE(),
            ERROR_MESSAGE(),
            @BatchLogJobId
        );

        -- Pass back the ErrorLogID of the row inserted
        SET @ErrorLogID = @@IDENTITY;
    END TRY
    BEGIN CATCH
        PRINT 'An error occurred in stored procedure stpLogError: ';
        EXECUTE [dbo].[stpPrintError];
        RETURN -1;
    END CATCH
END;
GO

/*--------------------------------------------------------------------------
  6. Proc: dbo.stpLogErrorEmpty
  Clears all rows from ErrorLog (Admin EmptyErrorLog).
--------------------------------------------------------------------------*/
CREATE OR ALTER PROCEDURE [dbo].[stpLogErrorEmpty]
AS
/*
  DECLARE @RC int
  EXECUTE @RC = dbo.stpLogErrorEmpty
  SELECT * FROM zvwErrorLog
*/
BEGIN
    DECLARE @ProcName NVARCHAR(128) = OBJECT_NAME(@@PROCID);
    PRINT 'Start of ' + @ProcName;

    BEGIN TRY
        DELETE FROM dbo.ErrorLog;
    END TRY
    BEGIN CATCH
        EXECUTE dbo.stpPrintError;
        EXECUTE dbo.stpLogError;
    END CATCH;
END;
GO

/*--------------------------------------------------------------------------
  7. Proc: dbo.stpLogErrorTest
  Intentionally throws so Admin can verify ErrorLog pipeline.
--------------------------------------------------------------------------*/
CREATE OR ALTER PROCEDURE [dbo].[stpLogErrorTest]
AS
/*
  DECLARE @RC int
  EXECUTE @RC = dbo.stpLogErrorTest
  SELECT * FROM zvwErrorLog WHERE ErrorProcedure = 'dbo.stpLogErrorTest'
*/
BEGIN
    DECLARE @ProcName NVARCHAR(128) = OBJECT_NAME(@@PROCID);
    DECLARE @MSG NVARCHAR(200) =
        N'Testing ErrorLog related objects.  Intentionally throwing an error. Inside:' + @ProcName;
    PRINT 'Start of ' + @ProcName;

    BEGIN TRY
        RAISERROR(@MSG, 16, 1);
    END TRY
    BEGIN CATCH
        EXECUTE dbo.stpPrintError;
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        EXECUTE dbo.stpLogError;
    END CATCH;
END;
GO

/*--------------------------------------------------------------------------
  Optional smoke check (uncomment after deploy)
--------------------------------------------------------------------------*/
/*
EXEC dbo.stpLogErrorTest;
SELECT TOP 5 * FROM dbo.zvwErrorLog ORDER BY ErrorLogID DESC;
-- EXEC dbo.stpLogErrorEmpty;
*/
