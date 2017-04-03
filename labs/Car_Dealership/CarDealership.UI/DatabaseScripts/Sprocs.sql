USE GuildCars

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'StatesSelectAll')
      DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT StateId, [Name]
	FROM States
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakesSelectAll')
      DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT MakeId, [Name], DateAdded
	FROM Makes
	ORDER BY [Name] ASC
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInsert')
      DROP PROCEDURE VehicleInsert
GO

CREATE PROCEDURE VehicleInsert (
	@VehicleId int output,
	@UserId nvarchar(128),
	@ModelId int,
	@BodyStyleId int,
	@InteriorColorId int,
	@ExteriorColorId int,
	@SalePrice decimal(8,2),
	@MSRP decimal(8,2),
	@Mileage decimal(8,2),
	@VIN char(17),
	@Description nvarchar(500),
	@IsUsed bit,
	@IsAutomatic bit,
	@IsFeatured bit,
	@Image nvarchar(100)
) AS
BEGIN
	INSERT INTO Vehicles (UserId, ModelId, BodyStyleId, InteriorColorId, ExteriorColorId,
		SalePrice, MSRP, Mileage, VIN, [Description], IsUsed, IsAutomatic, IsFeatured, [Image])
	VALUES (@UserId, @ModelId, @BodyStyleId, @InteriorColorId, @ExteriorColorId, @SalePrice,
		@MSRP, @VIN, @Description, @IsUsed, @IsAutomatic, @IsFeatured, @Image);

	SET @VehicleId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleUpdate')
      DROP PROCEDURE VehicleUpdate
GO

CREATE PROCEDURE VehicleUpdate (
	@VehicleId int,
	@UserId nvarchar(128),
	@ModelId int,
	@BodyStyleId int,
	@InteriorColorId int,
	@ExteriorColorId int,
	@SalePrice decimal(8,2),
	@MSRP decimal(8,2),
	@Mileage decimal(8,2),
	@VIN char(17),
	@Description nvarchar(500),
	@IsUsed bit,
	@IsAutomatic bit,
	@IsFeatured bit,
	@Image nvarchar(100)
) AS
BEGIN
	UPDATE Vehicles SET
		UserId = @UserId, 
		ModelId = @ModelId, 
		BodyStyleId = @BodyStyleId,
		InteriorColorId = @InteriorColorId,
		ExteriorColorId = @ExteriorColorId,
		SalePrice = @SalePrice,
		MSRP = @MSRP,
		Mileage = @Mileage,
		VIN = @VIN,
		[Description] = @Description,
		IsUsed = @IsUsed,
		IsAutomatic = @IsAutomatic,
		IsFeatured = @IsFeatured,
		[Image] = @Image
	WHERE VehicleId = @VehicleId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleDelete')
      DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (
	@VehicleId int
) AS
BEGIN
	BEGIN TRANSACTION
	DELETE FROM Vehicles WHERE VehicleId = @VehicleId;
	-- Delete additional tables here?
	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelect')
      DROP PROCEDURE VehicleSelect
GO

CREATE PROCEDURE VehicleSelect (
	@VehicleId int
) AS
BEGIN
	SELECT UserId, ModelId, BodyStyleId, InteriorColorId, ExteriorColorId, SalePrice,
		MSRP, Mileage, VIN, [Description], IsUsed, IsAutomatic, IsFeatured, [Image]
	FROM Vehicles
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehiclesSelectFeatured')
      DROP PROCEDURE VehiclesSelectFeatured
GO

CREATE PROCEDURE VehiclesSelectFeatured AS
BEGIN
	SELECT mk.MakeId, md.ModelId, md.[Year], SalePrice, IsFeatured, [Image]
	FROM Vehicles v
	INNER JOIN Models md on md.ModelId = v.ModelId
	INNER JOIN Makes mk on mk.MakeId = md.MakeId
END

GO

CREATE PROCEDURE VehicleSelectDetails (
	@VehicleId int
) AS
BEGIN
	SELECT UserId, [Year], IsUsed, IsAutomatic, IsFeatured, MK.[Name] as Make,
		MD.[Name] as Model, BS.[Description], IC.[Name] as InteriorColor,
		EC.[Name] as ExteriorColor, VIN, V.[Description], [Image], SalePrice,
		MSRP, Mileage
	FROM Vehicles V
	INNER JOIN BodyStyles BS on BS.BodyStyleId = V.BodyStyleId
	INNER JOIN InteriorColors IC on IC.InteriorColorId = V.InteriorColorId
	INNER JOIN ExteriorColors EC on EC.ExteriorColorId = V.ExteriorColorId
	INNER JOIN Models MD on MD.ModelId = V.ModelId
	INNER JOIN Makes MK on MK.MakeId = MK.MakeId
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ListingsSelectContacts')
      DROP PROCEDURE ListingsSelectContacts
GO

CREATE PROCEDURE ListingsSelectContacts (
	@UserId nvarchar(128)
) AS 
BEGIN
	SELECT l.ListingId, u.Email, u.Id as UserId, l.[Year], l.City, l.StateId, l.Rate
	FROM Listings l 
		INNER JOIN Contacts c ON l.ListingId = c.ListingId
		INNER JOIN AspNetUsers u ON c.UserId = u.Id
	WHERE l.UserId = @UserId;
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ListingsSelectByUser')
      DROP PROCEDURE ListingsSelectByUser
GO

CREATE PROCEDURE ListingsSelectByUser (
	@UserId nvarchar(128)
) AS
BEGIN
	SELECT ListingId, UserId, [Year], City, StateId, Rate, Mileage, 
		isNew, isManual, l.MakesId, MakesName, ImageFileName
	FROM Listings l 
		INNER JOIN Makes b ON l.MakesId = b.MakesId
	WHERE UserId = @UserId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsInsert')
      DROP PROCEDURE ContactsInsert
GO

CREATE PROCEDURE ContactsInsert (
	@UserId nvarchar(128),
	@ListingId int
) AS
BEGIN
	INSERT INTO Contacts(UserId, ListingId)
	VALUES (@UserId, @ListingId)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsDelete')
      DROP PROCEDURE ContactsDelete
GO

CREATE PROCEDURE ContactsDelete (
	@UserId nvarchar(128),
	@ListingId int
) AS
BEGIN
	DELETE FROM Contacts
	WHERE UserId = @UserId AND ListingId = @ListingId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsSelect')
      DROP PROCEDURE ContactsSelect
GO

CREATE PROCEDURE ContactsSelect (
	@UserId nvarchar(128),
	@ListingId int
) AS
BEGIN
	SELECT UserId, ListingId 
	FROM Contacts
	WHERE UserId = @UserId AND ListingId = @ListingId
END
GO