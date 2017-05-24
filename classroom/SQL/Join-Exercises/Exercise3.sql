/*
	Get all the order information for any order where Chai was sold.
*/

USE Northwind;
GO

SELECT * FROM Orders O
	INNER JOIN [Order Details] OD ON OD.OrderID = O.OrderID
	INNER JOIN Products P ON P.ProductID = OD.ProductID
WHERE ProductName = 'Chai'