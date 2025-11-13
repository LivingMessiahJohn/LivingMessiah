CREATE   VIEW Sukkot.zvwBatchDeleteCodeGen
AS

SELECT 'EXECUTE @RC = Sukkot.stpHouseRulesAgreementDelete ' + CHAR(39) + EMail + CHAR(39) AS CodeGen
FROM Sukkot.HouseRulesAgreement

/*

Step 1: Take a backup of the database, and or generate scripts that inserts the data in the 3 table

Step 2: execute the view below UNION script to create a CodeGen of records to Delete

  SELECT 'DECLARE @RC int' AS CodeGen
  UNION
	SELECT CodeGen FROM Sukkot.zvwBatchDeleteCodeGen


Step 3: COPY and Paste resluts to a query window.

Step 4: These rows are going to be deleted, do a before row count

SELECT COUNT(*) AS RowCnt, 'Donation' AS TableName							FROM Sukkot.Donation
UNION
SELECT COUNT(*) AS RowCnt, 'Registration' AS TableName					FROM Sukkot.Registration
UNION
SELECT COUNT(*) AS RowCnt, 'HouseRulesAgreement' AS TableName	FROM Sukkot.HouseRulesAgreement


Step 5: Execute the CodeGened Commands

Step 6: do an After row count, they should all be empty

*/


