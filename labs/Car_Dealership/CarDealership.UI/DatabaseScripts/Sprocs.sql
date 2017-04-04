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
	SELECT MakeId, UserId, [Name], DateAdded
	FROM Makes
	ORDER BY [Name] ASC
END
GO

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'MakeSelectById')
		drop procedure MakeSelectById
GO

create procedure MakeSelectById (
	@MakeId int
) as
begin
	select MakeId, UserId, [Name], DateAdded
	from Makes
	where MakeId = @MakeId
end
go

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeInsert')
      DROP PROCEDURE MakeInsert
GO

CREATE PROCEDURE MakeInsert (
	@MakeId int output,
	@UserId nvarchar(128),
	@Name NVARCHAR(25),
	@DateAdded DATETIME2
) AS
BEGIN
	INSERT INTO Makes (UserId, [Name], DateAdded)
	VALUES (@UserId, @Name, @DateAdded);

	SET @MakeId = SCOPE_IDENTITY();
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
	SELECT VehicleId, MK.[Name], MD.[Name], MD.[Year], SalePrice, IsFeatured, [Image]
	FROM Vehicles V
	INNER JOIN Models MD on MD.ModelId = V.ModelId
	INNER JOIN Makes MK on MK.MakeId = MD.MakeId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelectDetails')
      DROP PROCEDURE VehicleSelectDetails
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
   WHERE ROUTINE_NAME = 'VehiclesSelectDetails')
      DROP PROCEDURE VehiclesSelectDetails
GO

CREATE PROCEDURE VehiclesSelectDetails AS
BEGIN
	SELECT VehicleId, UserId, [Year], IsUsed, IsAutomatic, IsFeatured,
		MK.[Name] as Make, MD.[Name] as Model, BS.[Description],
		IC.[Name] as InteriorColor, EC.[Name] as ExteriorColor, VIN,
		V.[Description], [Image], SalePrice, MSRP, Mileage
	FROM Vehicles V
	INNER JOIN BodyStyles BS on BS.BodyStyleId = V.BodyStyleId
	INNER JOIN InteriorColors IC on IC.InteriorColorId = V.InteriorColorId
	INNER JOIN ExteriorColors EC on EC.ExteriorColorId = V.ExteriorColorId
	INNER JOIN Models MD on MD.ModelId = V.ModelId
	INNER JOIN Makes MK on MK.MakeId = MK.MakeId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactInsert')
      DROP PROCEDURE ContactInsert
GO

CREATE PROCEDURE ContactInsert (
	@ContactId int output,
	@VehicleId int,
	@UserId nvarchar(128),
	@Name NVARCHAR(50),
	@Phone NVARCHAR(15),
	@Email NVARCHAR(50),
	@Message NVARCHAR(500)
) AS
BEGIN
	INSERT INTO Contacts(VehicleId, UserId, [Name], Phone, Email, [Message])
	VALUES (@VehicleId, @UserId, @Name, @Phone, @Email, @Message)
	SET @ContactId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactDelete')
      DROP PROCEDURE ContactDelete
GO

CREATE PROCEDURE ContactDelete (
	@ContactId int
) AS
BEGIN
	DELETE FROM Contacts
	WHERE ContactId = @ContactId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactSelect')
      DROP PROCEDURE ContactSelect
GO

CREATE PROCEDURE ContactSelect (
	@ContactId int,
	@VehicleId int,
	@UserId nvarchar(128),
	@Name NVARCHAR(50),
	@Phone NVARCHAR(15),
	@Email NVARCHAR(50),
	@Message NVARCHAR(500)
) AS
BEGIN
	SELECT ContactId, VehicleId, UserId, [Name], Phone, Email, [Message]
	FROM Contacts
	WHERE ContactId = @ContactId
END
GO