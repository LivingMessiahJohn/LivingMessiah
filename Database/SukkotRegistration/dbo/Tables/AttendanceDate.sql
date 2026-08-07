CREATE TABLE [dbo].[AttendanceDate] (
    [Id]    INT           NOT NULL,
    [Date]  SMALLDATETIME NOT NULL,
    [Value] INT           NOT NULL,
    CONSTRAINT [PK_AttendanceDate] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

