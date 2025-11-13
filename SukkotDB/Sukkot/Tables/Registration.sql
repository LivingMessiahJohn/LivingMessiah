CREATE TABLE [Sukkot].[Registration] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [FamilyName]            NVARCHAR (75)  NOT NULL,
    [FirstName]             NVARCHAR (50)  CONSTRAINT [DF_Sukkot_Registration_FirstName] DEFAULT ('') NOT NULL,
    [SpouseName]            NVARCHAR (50)  NULL,
    [OtherNames]            NVARCHAR (255) NULL,
    [EMail]                 NVARCHAR (75)  NOT NULL,
    [Phone]                 NVARCHAR (15)  NULL,
    [Adults]                INT            NOT NULL,
    [ChildBig]              INT            NOT NULL,
    [ChildSmall]            INT            NOT NULL,
    [StatusId]              INT            NOT NULL,
    [AttendanceBitwise]     INT            NOT NULL,
    [Notes]                 NVARCHAR (800) NULL,
    [Avatar]                NVARCHAR (255) NULL,
    [HouseRulesAgreementId] INT            NOT NULL,
    [DidNotAttend]          BIT            CONSTRAINT [D_Sukkot_Registration_DidNotAttend] DEFAULT ((0)) NOT NULL,
    [AdminNotes]            VARCHAR (MAX)  NULL,
    [FeeEnumValue]          INT            NOT NULL,
    CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([HouseRulesAgreementId]) REFERENCES [Sukkot].[HouseRulesAgreement] ([Id]),
    CONSTRAINT [FK_Registration_Status] FOREIGN KEY ([StatusId]) REFERENCES [Sukkot].[Status] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Registration_EMail_Unique]
    ON [Sukkot].[Registration]([EMail] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CSV list of name who are not the registrant', @level0type = N'SCHEMA', @level0name = N'Sukkot', @level1type = N'TABLE', @level1name = N'Registration', @level2type = N'COLUMN', @level2name = N'OtherNames';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'How many days of attendance  which is used to drive the meal ticket UI. Based on Flag attribued Enums.Sukkot2018Days = Sept_22_Sat = 1 ... to Oct_01_Mon = 512', @level0type = N'SCHEMA', @level0name = N'Sukkot', @level1type = N'TABLE', @level1name = N'Registration', @level2type = N'COLUMN', @level2name = N'AttendanceBitwise';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key to Sukkot.HouseRulesAgreement!Id', @level0type = N'SCHEMA', @level0name = N'Sukkot', @level1type = N'TABLE', @level1name = N'Registration', @level2type = N'COLUMN', @level2name = N'HouseRulesAgreementId';

