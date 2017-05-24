/*
	Get a list of each employee first name and lastname
	and the territory names they are associated with
*/

USE Northwind;
GO

SELECT FirstName, LastName, TerritoryDescription FROM Employees E
	INNER JOIN EmployeeTerritories ET ON ET.EmployeeID = E.EmployeeID
	INNER JOIN Territories T ON T.TerritoryID = ET.TerritoryID
ORDER BY E.LastName