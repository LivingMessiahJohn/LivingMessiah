CREATE TABLE [Sukkot].[Donation] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [RegistrationId] INT            NOT NULL,
    [Detail]         INT            NOT NULL,
    [Amount]         MONEY          NOT NULL,
    [Notes]          NVARCHAR (255) NULL,
    [ReferenceId]    NVARCHAR (100) NOT NULL,
    [CreateDate]     SMALLDATETIME  NOT NULL,
    [CreatedBy]      NVARCHAR (25)  NOT NULL,
    [Email]          NVARCHAR (75)  NOT NULL,
    CONSTRAINT [PK_Sukkot.Donation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Donation_Registration] FOREIGN KEY ([RegistrationId]) REFERENCES [Sukkot].[Registration] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Sukkot.Donation_Unqiue]
    ON [Sukkot].[Donation]([RegistrationId] ASC, [Detail] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'This along with RegistrationId gives this table logical uniqueness ', @level0type = N'SCHEMA', @level0name = N'Sukkot', @level1type = N'TABLE', @level1name = N'Donation', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'This is a number that can be traced back to if from PayPal.  If it''s a manual entry, put in the User.Email ', @level0type = N'SCHEMA', @level0name = N'Sukkot', @level1type = N'TABLE', @level1name = N'Donation', @level2type = N'COLUMN', @level2name = N'ReferenceId';

