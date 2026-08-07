/*
  Copy data from legacy local database [Sukkot] (schema Sukkot / dbo)
  into [SukkotRegistration] (all dbo).

  Prerequisites:
    - Database SukkotRegistration exists (publish dacpac first)
    - Local legacy DB Sukkot still has the Sukkot schema objects

  Local example:
    sqlcmd -S "JohnsDellDT\SQLEXPRESS" -E -C -i docs/sql/sukkot-registration/01-Copy-Data-From-Legacy-Sukkot.sql

  Safe to re-run only on an empty target (script aborts if dbo.Registration has rows).
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF DB_ID(N'SukkotRegistration') IS NULL
BEGIN
    RAISERROR(N'Database SukkotRegistration does not exist. Publish the dacpac first.', 16, 1);
    RETURN;
END;

IF DB_ID(N'Sukkot') IS NULL
BEGIN
    RAISERROR(N'Source database Sukkot does not exist.', 16, 1);
    RETURN;
END;

USE SukkotRegistration;

IF EXISTS (SELECT 1 FROM dbo.Registration)
BEGIN
    RAISERROR(N'SukkotRegistration.dbo.Registration already has rows — aborting to avoid duplicates.', 16, 1);
    RETURN;
END;

BEGIN TRAN;

-- Lookup / calendar first (Status has no identity)
INSERT INTO dbo.Status (Id, Descr)
SELECT s.Id, s.Descr
FROM Sukkot.Sukkot.Status s
WHERE NOT EXISTS (SELECT 1 FROM dbo.Status t WHERE t.Id = s.Id);
PRINT CONCAT('Status rows inserted: ', @@ROWCOUNT);

INSERT INTO dbo.Constants (
    SingleRowId,
    EarlyRegistrationFee,
    EarlyRegistrationLastDay,
    RegistrationFee,
    RegistrationLastDay,
    AttendanceMinDate,
    AttendanceMaxDate,
    StatusStartRegistrationId,
    StatusPaymentId,
    StatusCompleteId,
    RegistrationFeeSingle)
SELECT
    c.SingleRowId,
    c.EarlyRegistrationFee,
    c.EarlyRegistrationLastDay,
    c.RegistrationFee,
    c.RegistrationLastDay,
    c.AttendanceMinDate,
    c.AttendanceMaxDate,
    c.StatusStartRegistrationId,
    c.StatusPaymentId,
    c.StatusCompleteId,
    c.RegistrationFeeSingle
FROM Sukkot.Sukkot.Constants c
WHERE NOT EXISTS (SELECT 1 FROM dbo.Constants t WHERE t.SingleRowId = c.SingleRowId);
PRINT CONCAT('Constants rows inserted: ', @@ROWCOUNT);

INSERT INTO dbo.AttendanceDate (Id, [Date], [Value])
SELECT s.Id, s.[Date], s.[Value]
FROM Sukkot.Sukkot.AttendanceDate s
WHERE NOT EXISTS (SELECT 1 FROM dbo.AttendanceDate t WHERE t.Id = s.Id);
PRINT CONCAT('AttendanceDate rows inserted: ', @@ROWCOUNT);

IF OBJECT_ID(N'Sukkot.dbo.Numbers', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM dbo.Numbers)
BEGIN
    INSERT INTO dbo.Numbers ([Number])
    SELECT n.[Number] FROM Sukkot.dbo.Numbers n;
    PRINT CONCAT('Numbers rows inserted: ', @@ROWCOUNT);
END

SET IDENTITY_INSERT dbo.HouseRulesAgreement ON;
INSERT INTO dbo.HouseRulesAgreement (Id, EMail, AcceptedDate, TimeZone)
SELECT s.Id, s.EMail, s.AcceptedDate, s.TimeZone
FROM Sukkot.Sukkot.HouseRulesAgreement s
WHERE NOT EXISTS (SELECT 1 FROM dbo.HouseRulesAgreement t WHERE t.Id = s.Id);
SET IDENTITY_INSERT dbo.HouseRulesAgreement OFF;
PRINT CONCAT('HouseRulesAgreement rows inserted: ', @@ROWCOUNT);

SET IDENTITY_INSERT dbo.Registration ON;
INSERT INTO dbo.Registration (
    Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone,
    Adults, ChildBig, ChildSmall, StatusId, AttendanceBitwise, Notes, Avatar,
    HouseRulesAgreementId, DidNotAttend, AdminNotes, FeeEnumValue)
SELECT
    s.Id, s.FamilyName, s.FirstName, s.SpouseName, s.OtherNames, s.EMail, s.Phone,
    s.Adults, s.ChildBig, s.ChildSmall, s.StatusId, s.AttendanceBitwise, s.Notes, s.Avatar,
    s.HouseRulesAgreementId, s.DidNotAttend, s.AdminNotes, s.FeeEnumValue
FROM Sukkot.Sukkot.Registration s
WHERE NOT EXISTS (SELECT 1 FROM dbo.Registration t WHERE t.Id = s.Id);
SET IDENTITY_INSERT dbo.Registration OFF;
PRINT CONCAT('Registration rows inserted: ', @@ROWCOUNT);

SET IDENTITY_INSERT dbo.Donation ON;
INSERT INTO dbo.Donation (
    Id, RegistrationId, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy, Email)
SELECT
    s.Id, s.RegistrationId, s.Detail, s.Amount, s.Notes, s.ReferenceId, s.CreateDate, s.CreatedBy, s.Email
FROM Sukkot.Sukkot.Donation s
WHERE NOT EXISTS (SELECT 1 FROM dbo.Donation t WHERE t.Id = s.Id);
SET IDENTITY_INSERT dbo.Donation OFF;
PRINT CONCAT('Donation rows inserted: ', @@ROWCOUNT);

SET IDENTITY_INSERT dbo.Stripe ON;
INSERT INTO dbo.Stripe (Id, Email, RegistrationId, ModificationCount, LastModifiedDate)
SELECT s.Id, s.Email, s.RegistrationId, s.ModificationCount, s.LastModifiedDate
FROM Sukkot.Sukkot.Stripe s
WHERE NOT EXISTS (SELECT 1 FROM dbo.Stripe t WHERE t.Id = s.Id);
SET IDENTITY_INSERT dbo.Stripe OFF;
PRINT CONCAT('Stripe rows inserted: ', @@ROWCOUNT);

IF OBJECT_ID(N'Sukkot.dbo.ScheduledEventsMarkdown', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM dbo.ScheduledEventsMarkdown)
BEGIN
    INSERT INTO dbo.ScheduledEventsMarkdown ([Lock], Markdown, LastRevised)
    SELECT s.[Lock], s.Markdown, s.LastRevised
    FROM Sukkot.dbo.ScheduledEventsMarkdown s;
    PRINT CONCAT('ScheduledEventsMarkdown rows inserted: ', @@ROWCOUNT);
END

COMMIT TRAN;

PRINT 'Done. Spot-check counts:';
SELECT
  (SELECT COUNT(*) FROM dbo.Status) AS StatusCount,
  (SELECT COUNT(*) FROM dbo.Constants) AS ConstantsCount,
  (SELECT COUNT(*) FROM dbo.AttendanceDate) AS AttendanceDateCount,
  (SELECT COUNT(*) FROM dbo.HouseRulesAgreement) AS HraCount,
  (SELECT COUNT(*) FROM dbo.Registration) AS RegistrationCount,
  (SELECT COUNT(*) FROM dbo.Donation) AS DonationCount,
  (SELECT COUNT(*) FROM dbo.Stripe) AS StripeCount,
  (SELECT COUNT(*) FROM dbo.ScheduledEventsMarkdown) AS ScheduleRowCount;
