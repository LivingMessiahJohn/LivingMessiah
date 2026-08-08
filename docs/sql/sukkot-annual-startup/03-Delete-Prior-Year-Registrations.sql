/*
  Sukkot Annual Startup — Step 3: Clear prior-year registration data.

  FK order (required):
    1. Donation
    2. Registration
    3. HouseRulesAgreement

  WARNING: Irreversible. Backup production before running there.
  Local/dev usually only has test rows.
*/

SET NOCOUNT ON;
USE SukkotRegistration;
GO

PRINT '=== BEFORE counts ===';
SELECT
  (SELECT COUNT(*) FROM dbo.Donation)              AS DonationCount,
  (SELECT COUNT(*) FROM dbo.Registration)          AS RegistrationCount,
  (SELECT COUNT(*) FROM dbo.HouseRulesAgreement)   AS HouseRulesAgreementCount;

BEGIN TRY
  BEGIN TRAN;

  DELETE FROM dbo.Donation;
  PRINT 'Donation deleted: ' + CAST(@@ROWCOUNT AS nvarchar(12));

  DELETE FROM dbo.Registration;
  PRINT 'Registration deleted: ' + CAST(@@ROWCOUNT AS nvarchar(12));

  DELETE FROM dbo.HouseRulesAgreement;
  PRINT 'HouseRulesAgreement deleted: ' + CAST(@@ROWCOUNT AS nvarchar(12));

  COMMIT TRAN;
  PRINT '=== Commit OK ===';
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0 ROLLBACK TRAN;
  PRINT '=== ERROR — rolled back ===';
  THROW;
END CATCH;

PRINT '=== AFTER counts (expect all 0) ===';
SELECT
  (SELECT COUNT(*) FROM dbo.Donation)              AS DonationCount,
  (SELECT COUNT(*) FROM dbo.Registration)          AS RegistrationCount,
  (SELECT COUNT(*) FROM dbo.HouseRulesAgreement)   AS HouseRulesAgreementCount;
GO
