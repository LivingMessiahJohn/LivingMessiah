CREATE TABLE [dbo].[HouseRulesAgreement] (
    [Id]           INT                IDENTITY (1, 1) NOT NULL,
    [EMail]        NVARCHAR (75)      NOT NULL,
    [AcceptedDate] DATETIMEOFFSET (7) NOT NULL,
    [TimeZone]     NVARCHAR (50)      NOT NULL,
    CONSTRAINT [PK_HouseRulesAgreement] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_HouseRulesAgreement_Unique]
    ON [dbo].[HouseRulesAgreement]([EMail] ASC);


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email associated with dbo.Registration!EMail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HouseRulesAgreement', @level2type = N'COLUMN', @level2name = N'EMail';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'List of all the users who aggreed to comply with the House rules agreement. This entry is create before the related dbo.Registration entry is created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HouseRulesAgreement';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Local timezone when the user agreed', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HouseRulesAgreement', @level2type = N'COLUMN', @level2name = N'TimeZone';


GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time (type datetimeoffset) the user agreed', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HouseRulesAgreement', @level2type = N'COLUMN', @level2name = N'AcceptedDate';


GO

