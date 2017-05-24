/*
	Write a query to show every combination of employee and location.
*/

USE SWCCorp;
GO

SELECT LastName, FirstName, City
FROM Employee E
	CROSS JOIN [Location] L
ORDER BY LastName