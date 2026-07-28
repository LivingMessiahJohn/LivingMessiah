CREATE TABLE [dbo].[Event] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [DateTime]      SMALLDATETIME  NOT NULL,
    [ShowBeginDate] SMALLDATETIME  NOT NULL,
    [ShowEndDate]   SMALLDATETIME  NOT NULL,
    [EventTypeId]   INT            NOT NULL,
    [Title]         NVARCHAR (100) NOT NULL,
    [SubTitle]      NVARCHAR (100) NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [ImageUrl]      NVARCHAR (150) NULL,
    [WebsiteUrl]    NVARCHAR (150) NULL,
    [WebsiteDescr]  NVARCHAR (150) NULL,
    [YouTubeId]     NVARCHAR (25)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

