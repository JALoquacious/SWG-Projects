USE GuildCars

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'DropConstraints'
		)
	DROP PROCEDURE DropConstraints
GO

CREATE PROCEDURE DropConstraints
AS
BEGIN
	/* Procedure to gather existing constraints */
	--select 'ALTER TABLE ' +  CONSTRAINT_SCHEMA + '.[' + Table_Name + '] DROP CONSTRAINT ' +
	--	CONSTRAINT_NAME from GuildCars.INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS C
	--WHERE C.CONSTRAINT_TYPE = 'FOREIGN KEY' AND TABLE_NAME NOT LIKE 'AspNet%'
	--ORDER BY TABLE_NAME

	ALTER TABLE dbo.[Contacts] DROP CONSTRAINT FK__Contacts__VehicleId
	ALTER TABLE dbo.[Contacts] DROP CONSTRAINT FK__Contacts__UserId
	ALTER TABLE dbo.[Customers] DROP CONSTRAINT FK__Customers__UserId
	ALTER TABLE dbo.[Customers] DROP CONSTRAINT FK__Customers__StateId
	ALTER TABLE dbo.[Makes] DROP CONSTRAINT FK__Makes__UserId
	ALTER TABLE dbo.[Models] DROP CONSTRAINT FK__Models__MakeId
	ALTER TABLE dbo.[Models] DROP CONSTRAINT FK__Models__UserId
	ALTER TABLE dbo.[Sales] DROP CONSTRAINT FK__Sales__VehicleId
	ALTER TABLE dbo.[Sales] DROP CONSTRAINT FK__Sales__CustomerId
	ALTER TABLE dbo.[Sales] DROP CONSTRAINT FK__Sales__PaymentTypeId
	ALTER TABLE dbo.[Vehicles] DROP CONSTRAINT FK__Vehicles__UserId
	ALTER TABLE dbo.[Vehicles] DROP CONSTRAINT FK__Vehicles__ModelId
	ALTER TABLE dbo.[Vehicles] DROP CONSTRAINT FK__Vehicles__BodyStyleId
	ALTER TABLE dbo.[Vehicles] DROP CONSTRAINT FK__Vehicles__InteriorColorId
	ALTER TABLE dbo.[Vehicles] DROP CONSTRAINT FK__Vehicles__ExteriorColorId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'WipeTables'
		)
	DROP PROCEDURE WipeTables
GO

CREATE PROCEDURE WipeTables
AS
BEGIN
	DECLARE @TableName NVARCHAR(32)

	SELECT TOP 1 @TableName = TABLE_NAME
	FROM GuildCars.INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME NOT LIKE 'AspNet%'
	ORDER BY TABLE_NAME DESC

	WHILE (@@ROWCOUNT > 0)
	BEGIN
		EXECUTE ('DROP TABLE [' + @TableName + ']')

		SELECT TOP 1 @TableName = TABLE_NAME
		FROM GuildCars.INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME > @TableName
		ORDER BY TABLE_NAME DESC
	END
END
GO

--IF EXISTS (
--		SELECT *
--		FROM INFORMATION_SCHEMA.ROUTINES
--		WHERE ROUTINE_NAME = 'SalesReport'
--		)
--	DROP PROCEDURE SalesReport
--GO

--CREATE PROCEDURE SalesReport (@UserName NVARCHAR(256), @FromDate DATETIME2, @ToDate DATETIME2)
--AS
--BEGIN
--	SELECT MAX(SP.FirstName + ' ' + SP.LastName) AS [User] -- Change MAX
--		,U.UserName
--		,SUM(S.PurchasePrice) AS TotalSales
--		,COUNT(S.VehicleId) AS TotalVehicles
		
--	FROM Sales AS S
--	INNER JOIN Vehicles AS V ON S.VehicleId = V.VehicleId
--	INNER JOIN AspNetUsers AS U ON U.Id = V.UserId
--	--WHERE U.UserName = @UserName
--	GROUP BY U.UserName
--END
--GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'SpecialDelete'
		)
	DROP PROCEDURE SpecialDelete
GO

CREATE PROCEDURE SpecialDelete (@SpecialId INT)
AS
BEGIN
	BEGIN TRANSACTION

	DELETE
	FROM Specials
	WHERE SpecialId = @SpecialId;

	COMMIT TRANSACTION
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'SpecialInsert'
		)
	DROP PROCEDURE SpecialInsert
GO

CREATE PROCEDURE SpecialInsert (
	@SpecialId INT OUTPUT
	,@Name NVARCHAR(25)
	,@Description NVARCHAR(500)
	)
AS
BEGIN
	INSERT INTO Specials (
		[Name]
		,[Description]
		)
	VALUES (
		@Name
		,@Description
		);

	SET @SpecialId = SCOPE_IDENTITY();
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'SpecialsSelectAll'
		)
	DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll
AS
BEGIN
	SELECT SpecialId
		,[Name]
		,[Description]
	FROM Specials
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'InteriorColorsSelectAll'
		)
	DROP PROCEDURE InteriorColorsSelectAll
GO

CREATE PROCEDURE InteriorColorsSelectAll
AS
BEGIN
	SELECT InteriorColorId
		,[Name]
	FROM InteriorColors
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ExteriorColorsSelectAll'
		)
	DROP PROCEDURE ExteriorColorsSelectAll
GO

CREATE PROCEDURE ExteriorColorsSelectAll
AS
BEGIN
	SELECT ExteriorColorId
		,[Name]
	FROM ExteriorColors
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'InventoryReport'
		)
	DROP PROCEDURE InventoryReport
GO

CREATE PROCEDURE InventoryReport (@IsUsed BIT)
AS
BEGIN
	SELECT MD.[Year]
		,MK.[Name] AS Make
		,MD.[Name] AS Model
		,COUNT(V.ModelId) AS [Count]
		,SUM(V.SalePrice) AS StockValue
	FROM Vehicles AS V
	INNER JOIN Models AS MD ON V.ModelId = MD.ModelId
	INNER JOIN Makes AS MK ON MK.MakeId = MD.MakeId
	WHERE V.IsUsed = @IsUsed
	GROUP BY MK.[Name], MD.[Year], MD.[Name]
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'StatesSelectAll'
		)
	DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll
AS
BEGIN
	SELECT StateId
		,[Name]
	FROM States
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'MakesSelectAll'
		)
	DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll
AS
BEGIN
	SELECT MakeId
		,UserId
		,[Name]
		,DateAdded
	FROM Makes
	ORDER BY [Name] ASC
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'MakeSelectById'
		)
	DROP PROCEDURE MakeSelectById
GO

CREATE PROCEDURE MakeSelectById (@MakeId INT)
AS
BEGIN
	SELECT MakeId
		,UserId
		,[Name]
		,DateAdded
	FROM Makes
	WHERE MakeId = @MakeId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'MakeInsert'
		)
	DROP PROCEDURE MakeInsert
GO

CREATE PROCEDURE MakeInsert (
	@MakeId INT OUTPUT
	,@UserId NVARCHAR(128)
	,@Name NVARCHAR(25)
	,@DateAdded DATETIME2
	)
AS
BEGIN
	INSERT INTO Makes (
		UserId
		,[Name]
		,DateAdded
		)
	VALUES (
		@UserId
		,@Name
		,@DateAdded
		);

	SET @MakeId = SCOPE_IDENTITY();
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'MakeAddView'
		)
	DROP PROCEDURE MakeAddView
GO

CREATE PROCEDURE MakeAddView
AS
BEGIN
	SELECT MK.[Name] AS MakeName
		,DateAdded
		,Email
	FROM Makes AS MK
	INNER JOIN AspNetUsers U ON U.Id = MK.UserId
	ORDER BY MK.[Name]
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ModelAddView'
		)
	DROP PROCEDURE ModelAddView
GO

CREATE PROCEDURE ModelAddView
AS
BEGIN
	SELECT MK.[Name] AS MakeName
		,MD.[Name] AS ModelName
		,MD.DateAdded
		,Email
	FROM Models AS MD
	INNER JOIN Makes AS MK ON MK.MakeId = MD.MakeId
	INNER JOIN AspNetUsers U ON U.Id = MD.UserId
	ORDER BY MakeName, ModelName
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ModelSelectByMakeId'
		)
	DROP PROCEDURE ModelSelectByMakeId
GO

CREATE PROCEDURE ModelSelectByMakeId (@MakeId INT)
AS
BEGIN
	select MD.ModelId, MD.MakeId, MD.UserId, MD.[Name], MD.[Year] from Models AS MD
	inner join Makes as MK on MK.MakeId = MD.MakeId
	where MK.MakeId = @MakeId
	order by MD.[Name]
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ModelsSelectAll'
		)
	DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll
AS
BEGIN
	SELECT ModelId
		,MakeId
		,UserId
		,[Name]
		,[Year]
		,DateAdded
	FROM Models
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ModelInsert'
		)
	DROP PROCEDURE ModelInsert
GO

CREATE PROCEDURE ModelInsert (
	@ModelId INT OUTPUT
	,@MakeId INT
	,@UserId NVARCHAR(128)
	,@Year INT
	,@Name NVARCHAR(25)
	,@DateAdded DATETIME2
	)
AS
BEGIN
	INSERT INTO Models (
		MakeId
		,UserId
		,[Name]
		,[Year]
		,DateAdded
		)
	VALUES (
		@MakeId
		,@UserId
		,@Name
		,@Year
		,@DateAdded
		);

	SET @ModelId = SCOPE_IDENTITY();
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehicleInsert'
		)
	DROP PROCEDURE VehicleInsert
GO

CREATE PROCEDURE VehicleInsert (
	@VehicleId INT OUTPUT
	,@UserId NVARCHAR(128)
	,@ModelId INT
	,@BodyStyleId INT
	,@InteriorColorId INT
	,@ExteriorColorId INT
	,@SaleId INT
	,@SalePrice DECIMAL(8, 2)
	,@MSRP DECIMAL(8, 2)
	,@Mileage DECIMAL(8, 2)
	,@VIN CHAR(17)
	,@Description NVARCHAR(500)
	,@IsUsed BIT
	,@IsAutomatic BIT
	,@IsFeatured BIT
	,@Image NVARCHAR(100)
	)
AS
BEGIN
	INSERT INTO Vehicles (
		UserId
		,ModelId
		,BodyStyleId
		,InteriorColorId
		,ExteriorColorId
		,SaleId
		,SalePrice
		,MSRP
		,Mileage
		,VIN
		,[Description]
		,IsUsed
		,IsAutomatic
		,IsFeatured
		,[Image]
		)
	VALUES (
		@UserId
		,@ModelId
		,@BodyStyleId
		,@InteriorColorId
		,@ExteriorColorId
		,@SaleId
		,@SalePrice
		,@MSRP
		,@Mileage
		,@VIN
		,@Description
		,@IsUsed
		,@IsAutomatic
		,@IsFeatured
		,@Image
		);

	SET @VehicleId = SCOPE_IDENTITY();
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehicleUpdate'
		)
	DROP PROCEDURE VehicleUpdate
GO

CREATE PROCEDURE VehicleUpdate (
	@VehicleId INT
	,@UserId NVARCHAR(128)
	,@ModelId INT
	,@BodyStyleId INT
	,@InteriorColorId INT
	,@ExteriorColorId INT
	,@SaleId INT = null
	,@SalePrice DECIMAL(8, 2)
	,@MSRP DECIMAL(8, 2)
	,@Mileage DECIMAL(8, 2)
	,@VIN CHAR(17)
	,@Description NVARCHAR(500)
	,@IsUsed BIT
	,@IsAutomatic BIT
	,@IsFeatured BIT
	,@Image NVARCHAR(100)
	)
AS
BEGIN
	UPDATE Vehicles
	SET UserId = @UserId
		,ModelId = @ModelId
		,BodyStyleId = @BodyStyleId
		,InteriorColorId = @InteriorColorId
		,ExteriorColorId = @ExteriorColorId
		,SaleId = @SaleId
		,SalePrice = @SalePrice
		,MSRP = @MSRP
		,Mileage = @Mileage
		,VIN = @VIN
		,[Description] = @Description
		,IsUsed = @IsUsed
		,IsAutomatic = @IsAutomatic
		,IsFeatured = @IsFeatured
		,[Image] = @Image
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehicleDelete'
		)
	DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (@VehicleId INT)
AS
BEGIN
	BEGIN TRANSACTION

	DELETE
	FROM Vehicles
	WHERE VehicleId = @VehicleId;

	COMMIT TRANSACTION
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehicleSelect'
		)
	DROP PROCEDURE VehicleSelect
GO

CREATE PROCEDURE VehicleSelect (@VehicleId INT)
AS
BEGIN
	SELECT VehicleId
		,UserId
		,ModelId
		,BodyStyleId
		,InteriorColorId
		,ExteriorColorId
		,SaleId
		,SalePrice
		,MSRP
		,Mileage
		,VIN
		,[Description]
		,IsUsed
		,IsAutomatic
		,IsFeatured
		,[Image]
	FROM Vehicles
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehiclesSelectFeatured'
		)
	DROP PROCEDURE VehiclesSelectFeatured
GO

CREATE PROCEDURE VehiclesSelectFeatured
AS
BEGIN
	SELECT VehicleId
		,MK.[Name] AS Make
		,MD.[Name] AS Model
		,MD.[Year]
		,SalePrice
		,[Image]
	FROM Vehicles V
	INNER JOIN Models MD ON MD.ModelId = V.ModelId
	INNER JOIN Makes MK ON MK.MakeId = MD.MakeId
	WHERE V.IsFeatured = 1
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehicleSelectDetails'
		)
	DROP PROCEDURE VehicleSelectDetails
GO

CREATE PROCEDURE VehicleSelectDetails (@VehicleId INT)
AS
BEGIN
	SELECT VehicleId
		,SaleId
		,V.UserId
		,[Year]
		,IsUsed
		,IsAutomatic
		,IsFeatured
		,MK.[Name] AS Make
		,MD.[Name] AS Model
		,BS.[Name] AS BodyStyle
		,IC.[Name] AS InteriorColor
		,EC.[Name] AS ExteriorColor
		,VIN
		,V.[Description]
		,[Image]
		,SalePrice
		,MSRP
		,Mileage
	FROM Vehicles V
	INNER JOIN BodyStyles BS ON BS.BodyStyleId = V.BodyStyleId
	INNER JOIN InteriorColors IC ON IC.InteriorColorId = V.InteriorColorId
	INNER JOIN ExteriorColors EC ON EC.ExteriorColorId = V.ExteriorColorId
	INNER JOIN Models MD ON MD.ModelId = V.ModelId
	INNER JOIN Makes MK ON MK.MakeId = MD.MakeId
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'VehiclesSelectDetails'
		)
	DROP PROCEDURE VehiclesSelectDetails
GO

CREATE PROCEDURE VehiclesSelectDetails
AS
BEGIN
	SELECT VehicleId
		,SaleId
		,V.UserId
		,[Year]
		,IsUsed
		,IsAutomatic
		,IsFeatured
		,MK.[Name] AS Make
		,MD.[Name] AS Model
		,BS.[Name] AS BodyStyle
		,IC.[Name] AS InteriorColor
		,EC.[Name] AS ExteriorColor
		,VIN
		,V.[Description]
		,[Image]
		,SalePrice
		,MSRP
		,Mileage
	FROM Vehicles V
	INNER JOIN BodyStyles BS ON BS.BodyStyleId = V.BodyStyleId
	INNER JOIN InteriorColors IC ON IC.InteriorColorId = V.InteriorColorId
	INNER JOIN ExteriorColors EC ON EC.ExteriorColorId = V.ExteriorColorId
	INNER JOIN Models MD ON MD.ModelId = V.ModelId
	INNER JOIN Makes MK ON MK.MakeId = MD.MakeId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ContactInsert'
		)
	DROP PROCEDURE ContactInsert
GO

CREATE PROCEDURE ContactInsert (
	@ContactId INT OUTPUT
	,@Name NVARCHAR(50)
	,@Phone NVARCHAR(15)
	,@Email NVARCHAR(50)
	,@Message NVARCHAR(500)
	)
AS
BEGIN
	INSERT INTO Contacts (
		[Name]
		,Phone
		,Email
		,[Message]
		)
	VALUES (
		@Name
		,@Phone
		,@Email
		,@Message
		)

	SET @ContactId = SCOPE_IDENTITY();
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'SaleInsert'
		)
	DROP PROCEDURE SaleInsert
GO

CREATE PROCEDURE SaleInsert (
	-- Customer fields
	@CustomerId INT OUTPUT
	,@UserId NVARCHAR(128)
	,@Name NVARCHAR(50)
	,@Phone NVARCHAR(15)
	,@Email NVARCHAR(50)
	,@Street1 NVARCHAR(50)
	,@Street2 NVARCHAR(50) = null
	,@City NVARCHAR(50)
	,@StateId CHAR(2)
	,@Zip CHAR(5)

	-- Sale fields
	,@SaleId INT OUTPUT
	,@PaymentTypeId INT
	,@PurchasePrice DECIMAL(8,2)
	,@Date DATETIME2

	-- Vehicle fields
	,@VehicleId INT
	)
AS
BEGIN
	INSERT INTO Customers (
		UserId
		,[Name]
		,Phone
		,Email
		,Street1
		,Street2
		,City
		,StateId
		,Zip
		)
	VALUES (
		@UserId
		,@Name
		,@Phone
		,@Email
		,@Street1
		,@Street2
		,@City
		,@StateId
		,@Zip
		)
	SET @CustomerId = SCOPE_IDENTITY();

	INSERT INTO Sales (
		CustomerId
		,UserId
		,PaymentTypeId
		,PurchasePrice
		,[Date]
		)
	VALUES (
		@CustomerId
		,@UserId
		,@PaymentTypeId
		,@PurchasePrice
		,@Date
		)
	SET @SaleId = SCOPE_IDENTITY();

	UPDATE Vehicles
	SET SaleId = @SaleId
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ContactDelete'
		)
	DROP PROCEDURE ContactDelete
GO

CREATE PROCEDURE ContactDelete (@ContactId INT)
AS
BEGIN
	DELETE
	FROM Contacts
	WHERE ContactId = @ContactId
END
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'ContactSelect'
		)
	DROP PROCEDURE ContactSelect
GO

CREATE PROCEDURE ContactSelect (
	@ContactId INT
	,@Name NVARCHAR(50)
	,@Phone NVARCHAR(15)
	,@Email NVARCHAR(50)
	,@Message NVARCHAR(500)
	)
AS
BEGIN
	SELECT ContactId
		,[Name]
		,Phone
		,Email
		,[Message]
	FROM Contacts
	WHERE ContactId = @ContactId
END
GO

