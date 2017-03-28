-- Start on root ---------------------------------------------------------
USE Master;
GO
--------------------------------------------------------------------------


-- Renew hotel database --------------------------------------------------
IF EXISTS(SELECT * FROM sys.databases WHERE NAME='HotelReservationSchema')
DROP DATABASE HotelReservationSchema;
GO
CREATE DATABASE HotelReservationSchema;
GO
USE HotelReservationSchema;
GO
---------------------------------------------------------------------------


-- Initialize tables ------------------------------------------------------
CREATE TABLE Taxes (
	TaxID INT IDENTITY(1,1) PRIMARY KEY,
	TaxRate DECIMAL(5,2) NOT NULL,
	Location VARCHAR(45) NULL
)

CREATE TABLE Promos (
	PromoID INT IDENTITY(1,1) PRIMARY KEY,
	FromDate DATETIME2 NOT NULL,
	ToDate DATETIME2 NOT NULL,
	DiscountAmount DECIMAL(7,2) NULL,
	DiscountRate DECIMAL(5,2) NULL
)

CREATE TABLE RoomType (
	RoomTypeID TINYINT IDENTITY(1,1) PRIMARY KEY,
	TypeOfRoom VARCHAR(20) NOT NULL,
	RoomTypeDescription VARCHAR(1000) NULL,
	Price DECIMAL(7,2) NOT NULL
)

CREATE TABLE Reservations (
	ReservationID INT IDENTITY(1,1) PRIMARY KEY,
	FromDate DATETIME2 NOT NULL,
	ToDate DATETIME2 NOT NULL,
	Total DECIMAL(8,2) NOT NULL,
	TaxID INT NOT NULL,
	PromoID INT NULL,
   	CONSTRAINT FK_Tax
		FOREIGN KEY (TaxID)
			REFERENCES Taxes(TaxID),
	CONSTRAINT FK_Promo
		FOREIGN KEY (PromoID)
			REFERENCES Promos(PromoID)
)

CREATE TABLE Rooms (
	RoomID INT IDENTITY(1,1) PRIMARY KEY,
	RoomNumber SMALLINT NOT NULL,
	[Floor] TINYINT NOT NULL,
	OccupancyLimit TINYINT NOT NULL,
	RoomTypeID TINYINT NOT NULL,
	ReservationID INT NOT NULL,
	CONSTRAINT FK_RoomType_Rooms
		FOREIGN KEY (RoomTypeID)
			REFERENCES RoomType(RoomTypeID),
	CONSTRAINT FK_Reservation_Rooms
		FOREIGN KEY (ReservationID)
			REFERENCES Reservations(ReservationID)
)

CREATE TABLE Persons (
	PersonID INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(30) NOT NULL,
	Phone VARCHAR(15) NULL,
	Email VARCHAR(45) NULL,
	IsCustomer BIT NOT NULL,
	Age TINYINT NULL,
	ReservationID INT NOT NULL,
	CONSTRAINT FK_Reservation_Persons
		FOREIGN KEY (ReservationID)
			REFERENCES Reservations(ReservationID)
)

CREATE TABLE Amenities (
	AmenityID SMALLINT IDENTITY(1,1) PRIMARY KEY,
	AmenityDescription VARCHAR(1000) NULL,
	Price DECIMAL(5,2) NOT NULL
)

CREATE TABLE RoomAmenities (
	RoomAmenityID INT IDENTITY(1,1) PRIMARY KEY,
	RoomID INT NOT NULL,
	AmenityID SMALLINT NOT NULL,
	CONSTRAINT FK_Room_RoomAmenities
		FOREIGN KEY (RoomID)
			REFERENCES Rooms(RoomID),
	CONSTRAINT FK_Amenity_RoomAmenities
		FOREIGN KEY (AmenityID)
			REFERENCES Amenities(AmenityID)
)

CREATE TABLE AddOns (
	AddOnID SMALLINT IDENTITY(1,1) PRIMARY KEY,
	AddOnType VARCHAR(30) NOT NULL,
	AddOnDescription VARCHAR(1000) NULL,
	AddOnPrice DECIMAL(7,2) NOT NULL
)

CREATE TABLE PersonAddOns (
	PersonAddOnID INT IDENTITY(1,1) PRIMARY KEY,
	PersonID INT NOT NULL,
	AddOnID SMALLINT NOT NULL,
	CONSTRAINT FK_Persons_PersonAddOns
		FOREIGN KEY (PersonID)
			REFERENCES Persons(PersonID),
	CONSTRAINT FK_AddOn_PersonAddOns
		FOREIGN KEY (AddOnID)
			REFERENCES AddOns(AddOnID)
)
GO
---------------------------------------------------------------------------