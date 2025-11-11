-- ErrorLog Table
-- Stores application error logs
CREATE TABLE [dbo].[ErrorLog]
(
    [ErrorLogID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ErrorTime] DATETIME NOT NULL DEFAULT GETDATE(),
    [UserName] NVARCHAR(128) NULL,
    [ErrorNumber] INT NULL,
    [ErrorSeverity] INT NULL,
    [ErrorState] INT NULL,
    [ErrorProcedure] NVARCHAR(126) NULL,
    [ErrorLine] INT NULL,
    [ErrorMessage] NVARCHAR(4000) NULL
);
