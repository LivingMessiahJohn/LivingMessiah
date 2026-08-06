CREATE TABLE [dbo].[Stripe] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Email]             NVARCHAR (75) NOT NULL,
    [RegistrationId]    INT           NOT NULL,
    [ModificationCount] INT           NOT NULL,
    [LastModifiedDate]  SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_Sukkot_Stripe] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Sukkot_Stripe_Unique]
    ON [dbo].[Stripe]([Email] ASC);


GO

