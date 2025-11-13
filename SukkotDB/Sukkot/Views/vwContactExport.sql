CREATE VIEW Sukkot.vwContactExport
AS
/*
  SELECT * FROM Sukkot.vwContactExport ORDER BY FirstName
*/
SELECT 
CASE 
 WHEN NameAndSpouse = 'Maegan  and None Hunt' THEN 'Maegan Hunt'
 WHEN NameAndSpouse = 'Darwin and Candy The Souths and Cory' THEN 'Darwin and Candy South'
 WHEN NameAndSpouse = 'Steven and Lindsay The McHenry’s' THEN 'Steven and Lindsay McHenry'
 ELSE NameAndSpouse
END AS NameAndSpouse
, ISNULL(OtherNames, '') OtherNames
, Replace(Replace(Replace(Replace(ISNULL(Phone, ''), '(', ''), ')', ''), '-', ''), 'B:', '') AS Phone
, EMail, '' Notes, Id AS Sukkot2020Id
, FirstName
FROM Sukkot.vwRegistration 
--
