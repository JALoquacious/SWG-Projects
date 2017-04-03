USE GuildCars
GO

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'WipeTables')
		drop procedure WipeTables
GO

CREATE PROCEDURE WipeTables AS
BEGIN
	DECLARE @TableName NVARCHAR(32)
	SELECT TOP 1 @TableName = TABLE_NAME
	FROM INFORMATION_SCHEMA.TABLES
	ORDER BY TABLE_NAME ASC

	WHILE (@@ROWCOUNT > 0)
		BEGIN
			EXECUTE('DROP TABLE [' + @TableName + ']')
			SELECT TOP 1 @TableName = TABLE_NAME
			FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_NAME > @TableName
			ORDER BY TABLE_NAME DESC
		END
END
GO


/*----------------------------------- VEHICLE INFO -----------------------------------*/

CREATE TABLE BodyStyles (
	BodyStyleId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Description] VARCHAR(25) NOT NULL
)

CREATE TABLE ExteriorColors (
	ExteriorColorId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(25) NOT NULL
)

CREATE TABLE InteriorColors (
	InteriorColorId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(25) NOT NULL
)

CREATE TABLE Makes (
	MakeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	DateAdded DATETIME2
)

CREATE TABLE Models (
	ModelId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MakeId INT FOREIGN KEY REFERENCES Makes(MakeId),
	[Name] NVARCHAR(25) NOT NULL,
	[Year] INT NOT NULL
	DateAdded DATETIME2
)

--CREATE TABLE Transmissions (
--	TransmissionId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	[Type] NVARCHAR(20) NULL
--)

CREATE TABLE Vehicles (
	VehicleId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id),
	ModelId INT FOREIGN KEY REFERENCES Models(ModelId),
	--TransmissionId INT FOREIGN KEY REFERENCES Transmissions(TransmissionId),
	BodyStyleId INT FOREIGN KEY REFERENCES BodyStyles(BodyStyleId),
	InteriorColorId INT FOREIGN KEY REFERENCES InteriorColors(InteriorColorId),
	ExteriorColorId INT FOREIGN KEY REFERENCES ExteriorColors(ExteriorColorId),
	SalePrice DECIMAL(8,2) NOT NULL,
	MSRP DECIMAL(8,2) NOT NULL,
	Mileage DECIMAL(8,2) NOT NULL,
	VIN CHAR(17) NOT NULL,
	[Description] NVARCHAR(500) NULL,
	IsUsed BIT NOT NULL,
	IsAutomatic BIT NOT NULL,
	IsFeatured BIT NOT NULL,
	[Image] NVARCHAR(100) NULL
)
/*------------------------------------------------------------------------------------*/


/*------------------------------------ SALES INFO ------------------------------------*/

CREATE TABLE PaymentTypes (
	PaymentTypeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Description] NVARCHAR(25) NOT NULL
)

CREATE TABLE Salespersons (
	SalesPersonId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(25) NOT NULL
)

CREATE TABLE Specials (
	SpecialId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(75) NOT NULL,
	[Description] NVARCHAR(500) NOT NULL
)

CREATE TABLE States (
	StateId CHAR(2) PRIMARY KEY NOT NULL,
	[Name] VARCHAR(25) NOT NULL
)

CREATE TABLE Customers (
	CustomerId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Phone NVARCHAR(15) NULL, -- required if email not provided
	Email NVARCHAR(50) NULL, -- required if phone not provided
	Street1 NVARCHAR(50) NOT NULL,
	Street2 NVARCHAR(50) NULL,
	City NVARCHAR(50) NOT NULL,
	StateId CHAR(2) FOREIGN KEY REFERENCES States(StateId) NOT NULL,
	Zip CHAR(5) NOT NULL
)

CREATE TABLE Sales (
	SaleId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehicleId) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId) NOT NULL,
	SalespersonId INT FOREIGN KEY REFERENCES Salespersons(SalesPersonId) NOT NULL,
	PaymentTypeId INT FOREIGN KEY REFERENCES PaymentTypes(PaymentTypeId) NOT NULL,
	PurchasePrice DECIMAL(8,2) NOT NULL,
	[Date] DATETIME2 NOT NULL
)
/*------------------------------------------------------------------------------------*/


/*----------------------------------- CONTACT INFO -----------------------------------*/

CREATE TABLE Contacts (
	ContactId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Phone NVARCHAR(15) NULL, -- required if email not provided
	Email NVARCHAR(50) NULL, -- required if phone not provided
)

CREATE TABLE Communications (
	CommunicationId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ContactId INT FOREIGN KEY REFERENCES Contacts(ContactId) NOT NULL,
	VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehicleId) NULL,
	[Message] NVARCHAR(500) NOT NULL	
)
/*------------------------------------------------------------------------------------*/