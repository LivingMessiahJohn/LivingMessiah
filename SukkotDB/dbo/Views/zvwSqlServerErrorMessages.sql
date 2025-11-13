
CREATE   VIEW dbo.zvwSqlServerErrorMessages
AS
/*

SELECT message_id, text, severity, is_event_logged
--, language_id
FROM  dbo.zvwSqlServerErrorMessages
WHERE message_id = 515


GRANT SELECT on dbo.zvwSqlServerErrorMessages TO [UserName]
*/

SELECT message_id, text
, severity, is_event_logged
--, language_id
FROM sys.messages
WHERE language_id=1033 -- English
