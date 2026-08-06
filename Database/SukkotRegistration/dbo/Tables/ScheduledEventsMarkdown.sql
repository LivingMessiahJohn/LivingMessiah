CREATE TABLE [dbo].[ScheduledEventsMarkdown] (
    [Lock]        CHAR (1)       NOT NULL,
    [Markdown]    NVARCHAR (MAX) NOT NULL,
    [LastRevised] SMALLDATETIME  CONSTRAINT [DF_ScheduledEventsMarkdown_LastRevised] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ScheduledEventsMarkdown] PRIMARY KEY CLUSTERED ([Lock] ASC),
    CONSTRAINT [CK_ScheduledEventsMarkdown_Lock] CHECK ([Lock]='X')
);


GO

