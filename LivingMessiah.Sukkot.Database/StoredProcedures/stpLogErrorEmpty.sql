-- stpLogErrorEmpty Stored Procedure
-- Empties the error log table
CREATE PROCEDURE [dbo].[stpLogErrorEmpty]
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @RowCount INT;
    
    DELETE FROM [dbo].[ErrorLog];
    
    SET @RowCount = @@ROWCOUNT;
    
    RETURN @RowCount;
END;
