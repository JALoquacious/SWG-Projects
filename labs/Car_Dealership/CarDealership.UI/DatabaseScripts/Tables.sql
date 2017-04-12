USE GuildCars
GO

--EXEC DropConstraints
--GO

--EXEC WipeTables
--GO

/*----------------------------------- TABLE DROPS ------------------------------------*/
IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Contacts')
	DROP TABLE Contacts
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Sales')
	DROP TABLE Sales
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Customers')
	DROP TABLE Customers
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='States')
	DROP TABLE States
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Salespersons')
	DROP TABLE Salespersons
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='PaymentTypes')
	DROP TABLE PaymentTypes
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Vehicles')
	DROP TABLE Vehicles
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Models')
	DROP TABLE Models
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='Makes')
	DROP TABLE Makes
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='InteriorColors')
	DROP TABLE InteriorColors
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='ExteriorColors')
	DROP TABLE ExteriorColors
GO

IF EXISTS(SELECT * FROM GuildCars.sys.tables WHERE name='BodyStyles')
	DROP TABLE BodyStyles
GO
/*------------------------------------------------------------------------------------*/


/*----------------------------------- VEHICLE INFO -----------------------------------*/
CREATE TABLE BodyStyles (
	BodyStyleId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Name] VARCHAR(25) NOT NULL
	)

CREATE TABLE ExteriorColors (
	ExteriorColorId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Name] NVARCHAR(25) NOT NULL
	)

CREATE TABLE InteriorColors (
	InteriorColorId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Name] NVARCHAR(25) NOT NULL
	)

CREATE TABLE Makes (
	MakeId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,UserId NVARCHAR(128) CONSTRAINT FK__Makes__UserId FOREIGN KEY REFERENCES AspNetUsers(Id)
	,[Name] NVARCHAR(25) NOT NULL
	,DateAdded DATETIME2
	)

CREATE TABLE Models (
	ModelId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,MakeId INT CONSTRAINT FK__Models__MakeId FOREIGN KEY REFERENCES Makes(MakeId)
	,UserId NVARCHAR(128) CONSTRAINT FK__Models__UserId FOREIGN KEY REFERENCES AspNetUsers(Id)
	,[Name] NVARCHAR(25) NOT NULL
	,[Year] INT NOT NULL
	,DateAdded DATETIME2
	)

	CREATE TABLE PaymentTypes (
	PaymentTypeId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Description] NVARCHAR(25) NOT NULL
	)

CREATE TABLE Specials (
	SpecialId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Name] NVARCHAR(75) NOT NULL
	,[Description] NVARCHAR(500) NOT NULL
	)

CREATE TABLE States (
	StateId CHAR(2) PRIMARY KEY NOT NULL
	,[Name] VARCHAR(25) NOT NULL
	)

CREATE TABLE Customers (
	CustomerId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,UserId NVARCHAR(128) CONSTRAINT FK__Customers__UserId FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,Phone NVARCHAR(15) NULL
	,-- required if email not provided
	Email NVARCHAR(50) NULL
	,-- required if phone not provided
	Street1 NVARCHAR(50) NOT NULL
	,Street2 NVARCHAR(50) NULL
	,City NVARCHAR(50) NOT NULL
	,StateId CHAR(2) CONSTRAINT FK__Customers__StateId FOREIGN KEY REFERENCES States(StateId) NOT NULL
	,Zip CHAR(5) NOT NULL
	)

CREATE TABLE Sales (
	SaleId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,CustomerId INT CONSTRAINT FK__Sales__CustomerId FOREIGN KEY REFERENCES Customers(CustomerId) NOT NULL
	,UserId NVARCHAR(128) CONSTRAINT FK__Sales__UserId FOREIGN KEY REFERENCES AspNetUsers(Id)
	,PaymentTypeId INT CONSTRAINT FK__Sales__PaymentTypeId FOREIGN KEY REFERENCES PaymentTypes(PaymentTypeId) NOT NULL
	,PurchasePrice DECIMAL(8, 2) NOT NULL
	,[Date] DATETIME2 NOT NULL
	)

CREATE TABLE Vehicles (
	VehicleId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,UserId NVARCHAR(128) CONSTRAINT FK__Vehicles__UserId FOREIGN KEY REFERENCES AspNetUsers(Id)
	,ModelId INT CONSTRAINT FK__Vehicles__ModelId FOREIGN KEY REFERENCES Models(ModelId)
	,BodyStyleId INT CONSTRAINT FK__Vehicles__BodyStyleId FOREIGN KEY REFERENCES BodyStyles(BodyStyleId)
	,InteriorColorId INT CONSTRAINT FK__Vehicles__InteriorColorId FOREIGN KEY REFERENCES InteriorColors(InteriorColorId)
	,ExteriorColorId INT CONSTRAINT FK__Vehicles__ExteriorColorId FOREIGN KEY REFERENCES ExteriorColors(ExteriorColorId)
	,SaleId INT CONSTRAINT FK__Vehicles__SaleId FOREIGN KEY REFERENCES Sales(SaleId) NULL
	,SalePrice DECIMAL(8, 2) NOT NULL
	,MSRP DECIMAL(8, 2) NOT NULL
	,Mileage DECIMAL(8, 2) NOT NULL
	,VIN CHAR(17) NOT NULL
	,[Description] NVARCHAR(500) NULL
	,IsUsed BIT NOT NULL
	,IsAutomatic BIT NOT NULL
	,IsFeatured BIT NOT NULL
	,[Image] NVARCHAR(100) NULL
	)

CREATE TABLE Contacts (
	ContactId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,Phone NVARCHAR(15) NULL -- required if email not provided
	,Email NVARCHAR(50) NULL -- required if phone not provided
	,[Message] NVARCHAR(500) NOT NULL
	)