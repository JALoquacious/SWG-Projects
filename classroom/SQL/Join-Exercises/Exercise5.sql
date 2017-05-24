/*
	Find a list of all the Employees who have never found a Grant
*/

USE SWCCorp;
GO

SELECT LastName, FirstName
FROM Employee E
	LEFT JOIN [Grant] G ON G.EmpID = E.EmpID
WHERE G.GrantId IS NULL