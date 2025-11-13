
CREATE PROCEDURE Sukkot.stpPopulateRegistrationTestData (
@EMail as nvarchar(75),
@Scenario as nvarchar(75)
)
AS
/*
	DECLARE @RC int; EXEC @RC = Sukkot.stpPopulateRegistrationTestData 'aeaij@yahoo.com', 'AgreementNotSigned'
	DECLARE @RC int; EXEC @RC = Sukkot.stpPopulateRegistrationTestData 'aeaij@yahoo.com', 'StartRegistration'
	DECLARE @RC int; EXEC @RC = Sukkot.stpPopulateRegistrationTestData 'aeaij@yahoo.com', 'Payment-Partial'
	DECLARE @RC int; EXEC @RC = Sukkot.stpPopulateRegistrationTestData 'aeaij@yahoo.com', 'Payment-Full'
	DECLARE @RC int; EXEC @RC = Sukkot.stpPopulateRegistrationTestData 'aeaij@yahoo.com', 'Complete'
	DECLARE @RC int; EXEC @RC = Sukkot.stpPopulateRegistrationTestData 'aeaij@yahoo.com', 'Test Unrecognized Scenario'

	SELECT hra.Id, hra.EMail, hra.TimeZone, FORMAT(hra.AcceptedDate, N'MM/dd hh:mm') AS AcceptedDate2
	, r.Id,	r.HouseRulesAgreementId, r.FirstName, r.FamilyName, r.EMail, r.StatusId, r.Status
	--, hra.AcceptedDate
	FROM  Sukkot.HouseRulesAgreement hra
		LEFT OUTER JOIN Sukkot.vwRegistration r 
			ON hra.Id = r.HouseRulesAgreementId

	SELECT * FROM zvwErrorLog


*/

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName + '; EMail:' + @EMail + '; Scenario:' + @Scenario

DECLARE @RegistrationId int = 0

DECLARE @SpouseName nvarchar(75) = null
DECLARE @OtherNames nvarchar(255) = null
DECLARE @Phone nvarchar(15) = '6025551212'
DECLARE @Adults int = 1
DECLARE @ChildBig int = 0
DECLARE @ChildSmall int = 0
DECLARE @FeeEnumValue as int = 1 -- Single = 1; Family = 2;
DECLARE @AttendanceBitwise int = 5
DECLARE @Notes nvarchar(800)  = null
DECLARE @Avatar nvarchar(255) = null
DECLARE @NewRegistrationId int

DECLARE @dt smalldatetime = GETDATE()
DECLARE @NewDonationId int

DECLARE @FamilyName nvarchar(75)
DECLARE @FirstName nvarchar(75)
DECLARE @StatusId int
DECLARE @HouseRulesAgreementId int

BEGIN TRY

	EXECUTE @RC = Sukkot.stpHouseRulesAgreementDelete @Email
  IF @Scenario = 'AgreementNotSigned' 
		BEGIN
			PRINT 'Noting to do after delete'
		END

	ELSE IF @Scenario = 'StartRegistration'
		BEGIN
			INSERT INTO Sukkot.HouseRulesAgreement (
							EMail,	AcceptedDate,													TimeZone)
			SELECT  @EMail, '2025-07-03 03:45:00.0000000 -06:00', 'Central America Standard Time'
		END

	ELSE IF @Scenario = 'Payment-Partial'
		BEGIN
			INSERT INTO Sukkot.HouseRulesAgreement (
							EMail,	AcceptedDate,													TimeZone)
			SELECT  @EMail, '2025-07-05 05:45:00.0000000 -06:00', 'Canada Central Standard Time'

			SET @HouseRulesAgreementId = @@IDENTITY 
			PRINT 'HouseRulesAgreementId=' + CAST(@HouseRulesAgreementId AS NVARCHAR(12))

			SELECT @StatusId = StatusPaymentId FROM Sukkot.Constants;
			SET @FirstName='Michael'
			SET @FamilyName = 'Palin'

			-- @HouseRulesAgreementId, 
			EXECUTE @RC = Sukkot.stpRegistrationInsert 
					@FamilyName, @FirstName, @SpouseName, @OtherNames, @EMail, @Phone
				, @Adults, @ChildBig, @ChildSmall, @FeeEnumValue
				, @StatusId, @AttendanceBitwise, @Notes, @Avatar, @NewRegistrationId OUTPUT
		
			PRINT 'New Registration Id=' + CAST(@NewRegistrationId AS NVARCHAR(12))

			EXEC @RC = Sukkot.stpDonationInsert @NewRegistrationId, 30.00, 'test, add half of registraion', 'Confirmation# XXXXXX', 'Admin@LivingMessiah.com', @dt, @NewDonationId OUTPUT
			SELECT @RC AS RC, @NewDonationId AS NewId

		END

	ELSE IF @Scenario = 'Payment-Full'
		BEGIN
			INSERT INTO Sukkot.HouseRulesAgreement (
							EMail,	AcceptedDate,													TimeZone)
			SELECT  @EMail, '2025-07-05 05:45:00.0000000 -06:00', 'Canada Central Standard Time'

			SET @HouseRulesAgreementId = @@IDENTITY 
			PRINT 'HouseRulesAgreementId=' + CAST(@HouseRulesAgreementId AS NVARCHAR(12))

			SELECT @StatusId = StatusPaymentId FROM Sukkot.Constants;
			SET @FirstName='Alowishus Devadander'
			SET @FamilyName = 'Abercrombie (AKA Mud)'

			-- @HouseRulesAgreementId, 
			EXECUTE @RC = Sukkot.stpRegistrationInsert 
					@FamilyName, @FirstName, @SpouseName, @OtherNames, @EMail, @Phone
				, @Adults, @ChildBig, @ChildSmall, @FeeEnumValue
				, @StatusId, @AttendanceBitwise, @Notes, @Avatar, @NewRegistrationId OUTPUT
		
			PRINT 'New Registration Id=' + CAST(@NewRegistrationId AS NVARCHAR(12))

			EXEC @RC = Sukkot.stpDonationInsert @NewRegistrationId, 60.00, 'test, add all of registraion', 'Confirmation# XXXXXX', 'Admin@LivingMessiah.com', @dt, @NewDonationId OUTPUT
			SELECT @RC AS RC, @NewDonationId AS NewId

		END

	ELSE IF @Scenario = 'Complete'
		BEGIN
			INSERT INTO Sukkot.HouseRulesAgreement (
				EMail,	AcceptedDate,													TimeZone)
				SELECT  @EMail, '2025-07-06 06:45:00.0000000 -04:00', 'Haiti Standard Time'

			SET @HouseRulesAgreementId = @@IDENTITY 
			PRINT 'HouseRulesAgreementId=' + CAST(@HouseRulesAgreementId AS NVARCHAR(12))

			SELECT @StatusId = StatusCompleteId  FROM Sukkot.Constants;
			SET @FirstName='John'
			SET @FamilyName = 'Cleese'

			--@HouseRulesAgreementId, 
			EXECUTE @RC = Sukkot.stpRegistrationInsert 
					@FamilyName, @FirstName, @SpouseName, @OtherNames, @EMail, @Phone
				, @Adults, @ChildBig, @ChildSmall, @FeeEnumValue
				, @StatusId, @AttendanceBitwise, @Notes, @Avatar, @NewRegistrationId OUTPUT
		
			PRINT 'New Registration Id=' + CAST(@NewRegistrationId AS NVARCHAR(12))

			EXEC @RC = Sukkot.stpDonationInsert @NewRegistrationId, 60.00, 'test, full registraion', 'Confirmation# XXXXXX', 'Admin@LivingMessiah.com', @dt, @NewDonationId OUTPUT
			SELECT @RC AS RC, @NewDonationId AS NewId

		END

	ELSE 
		BEGIN
			PRINT 'NO ACTION FOR @Scenario=' + @Scenario
		END


	PRINT 'End of ' + @ProcName ;

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
END CATCH;

