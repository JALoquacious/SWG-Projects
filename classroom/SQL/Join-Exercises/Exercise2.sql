/*
	Get the Company Name, Order Date, and each order detail’s 
	Product name for USA customers only.
*/

USE Northwind;
GO

SELECT CompanyName, OrderDate, ProductName
FROM Customers C
	INNER JOIN Orders O ON O.CustomerID = C.CustomerID
	INNER JOIN [Order Details] OD ON OD.OrderID = O.OrderID
	INNER JOIN Products P ON P.ProductID = OD.ProductID
WHERE Country = 'USA'