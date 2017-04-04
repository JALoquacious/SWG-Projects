USE GuildCars
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicles')
	DROP TABLE Vehicles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyles')
	DROP TABLE BodyStyles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExteriorColors')
	DROP TABLE ExteriorColors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColors')
	DROP TABLE InteriorColors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Models')
	DROP TABLE Models
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Makes')
	DROP TABLE Makes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contacts')
	DROP TABLE Contacts
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Customers')
	DROP TABLE Customers
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Sales')
	DROP TABLE Sales
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PaymentTypes')
	DROP TABLE PaymentTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Salespersons')
	DROP TABLE Salespersons
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO


/*----------------------------------- VEHICLE INFO -----------------------------------*/
CREATE TABLE BodyStyles (
	BodyStyleId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Description] VARCHAR(25) NOT NULL
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
	,UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id)
	,[Name] NVARCHAR(25) NOT NULL
	,DateAdded DATETIME2
	)

CREATE TABLE Models (
	ModelId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,MakeId INT FOREIGN KEY REFERENCES Makes(MakeId)
	,UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id)
	,[Name] NVARCHAR(25) NOT NULL
	,[Year] INT NOT NULL
	,DateAdded DATETIME2
	)

--CREATE TABLE Transmissions (
--	TransmissionId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	[Type] NVARCHAR(20) NULL
--)
CREATE TABLE Vehicles (
	VehicleId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id)
	,ModelId INT FOREIGN KEY REFERENCES Models(ModelId)
	,BodyStyleId INT FOREIGN KEY REFERENCES BodyStyles(BodyStyleId)
	,InteriorColorId INT FOREIGN KEY REFERENCES InteriorColors(InteriorColorId)
	,ExteriorColorId INT FOREIGN KEY REFERENCES ExteriorColors(ExteriorColorId)
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

/*------------------------------------------------------------------------------------*/


/*------------------------------------ SALES INFO ------------------------------------*/
CREATE TABLE PaymentTypes (
	PaymentTypeId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,[Description] NVARCHAR(25) NOT NULL
	)

CREATE TABLE Salespersons (
	SalesPersonId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,FirstName NVARCHAR(25) NOT NULL
	,LastName NVARCHAR(25) NOT NULL
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
	,UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL
	,[Name] NVARCHAR(50) NOT NULL
	,Phone NVARCHAR(15) NULL
	,-- required if email not provided
	Email NVARCHAR(50) NULL
	,-- required if phone not provided
	Street1 NVARCHAR(50) NOT NULL
	,Street2 NVARCHAR(50) NULL
	,City NVARCHAR(50) NOT NULL
	,StateId CHAR(2) FOREIGN KEY REFERENCES States(StateId) NOT NULL
	,Zip CHAR(5) NOT NULL
	)

CREATE TABLE Sales (
	SaleId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehicleId) NOT NULL
	,CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId) NOT NULL
	,SalespersonId INT FOREIGN KEY REFERENCES Salespersons(SalesPersonId) NOT NULL
	,PaymentTypeId INT FOREIGN KEY REFERENCES PaymentTypes(PaymentTypeId) NOT NULL
	,PurchasePrice DECIMAL(8, 2) NOT NULL
	,[Date] DATETIME2 NOT NULL
	)

/*------------------------------------------------------------------------------------*/


/*----------------------------------- CONTACT INFO -----------------------------------*/
CREATE TABLE Contacts (
	ContactId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL
	,VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehicleId) NOT NULL
	,UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id)
	,[Name] NVARCHAR(50) NOT NULL
	,Phone NVARCHAR(15) NULL
	,-- required if email not provided
	Email NVARCHAR(50) NULL
	,-- required if phone not provided
	[Message] NVARCHAR(500) NOT NULL
	)
	--CREATE TABLE Communications (
	--	CommunicationId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	--	ContactId INT FOREIGN KEY REFERENCES Contacts(ContactId) NOT NULL,
	--	VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehicleId) NULL,
	--	[Message] NVARCHAR(500) NOT NULL
	--)
/*------------------------------------------------------------------------------------*/