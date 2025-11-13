CREATE TABLE [Sukkot].[Constants] (
    [SingleRowId]               INT            NOT NULL,
    [EarlyRegistrationFee]      DECIMAL (5, 2) NULL,
    [EarlyRegistrationLastDay]  SMALLDATETIME  NULL,
    [RegistrationFee]           DECIMAL (5, 2) NULL,
    [RegistrationLastDay]       SMALLDATETIME  NULL,
    [AttendanceMinDate]         SMALLDATETIME  NOT NULL,
    [AttendanceMaxDate]         SMALLDATETIME  NOT NULL,
    [StatusStartRegistrationId] INT            CONSTRAINT [DF_Sukkot_Constants_StatusStartRegistrationId] DEFAULT ((4)) NOT NULL,
    [StatusPaymentId]           INT            CONSTRAINT [DF_Sukkot_Constants_StatusPaymentId] DEFAULT ((5)) NOT NULL,
    [StatusCompleteId]          INT            CONSTRAINT [DF_Sukkot_Constants_StatusCompleteId] DEFAULT ((6)) NOT NULL,
    [RegistrationFeeSingle]     DECIMAL (5, 2) NULL,
    CONSTRAINT [PK_Constants] PRIMARY KEY CLUSTERED ([SingleRowId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'House Rules Agreement is signed so Start Registration', @level0type = N'SCHEMA', @level0name = N'Sukkot', @level1type = N'TABLE', @level1name = N'Constants', @level2type = N'COLUMN', @level2name = N'StatusStartRegistrationId';

