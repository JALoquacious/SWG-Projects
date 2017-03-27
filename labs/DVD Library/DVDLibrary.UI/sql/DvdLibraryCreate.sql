USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

--CREATE TABLE Rating (
--	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	[Description] VARCHAR(20) NOT NULL
--)

CREATE TABLE Dvd (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	ReleaseYear INT NULL,
	Director NVARCHAR(50) NULL,
	Rating NVARCHAR(5) NULL,
	--RatingId INT FOREIGN KEY REFERENCES Rating(Id) NOT NULL,
	Notes NVARCHAR(500) NULL
)