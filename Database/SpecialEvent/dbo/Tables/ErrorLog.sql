CREATE TABLE [dbo].[ErrorLog]
(
    [ErrorLogID]     INT IDENTITY (1, 1) NOT NULL,
    [ErrorTime]      DATETIME NOT NULL
        CONSTRAINT [DF_ErrorLog_ErrorTime] DEFAULT (GETDATE()),
    [UserName]       SYSNAME NOT NULL,
    [ErrorNumber]    INT NOT NULL,
    [ErrorSeverity]  INT NULL,
    [ErrorState]     INT NULL,
    [ErrorProcedure] NVARCHAR (126) NULL,
    [ErrorLine]      INT NULL,
    [ErrorMessage]   NVARCHAR (4000) NOT NULL,
    [BatchLogJobId]  INT NULL
        CONSTRAINT [DF_ErrorLog_JobId] DEFAULT ((0)),
    CONSTRAINT [PK_ErrorLog_ErrorLogID] PRIMARY KEY CLUSTERED ([ErrorLogID] ASC)
);
