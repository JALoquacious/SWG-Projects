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
	ALTER TABLE dbo.[Sales] DROP CONSTRAINT FK__Sales__SalespersonId
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
--		,SUM(S.SalePrice) AS TotalSales
--		,COUNT(S.VehicleId) AS TotalVehicles
		
--	FROM Sales AS S
--	INNER JOIN Salespersons AS SP ON SP.SalespersonId = S.SalespersonId
--	INNER JOIN Vehicles AS V ON S.VehicleId = V.VehicleId
--	INNER JOIN AspNetUsers AS U ON U.Id = V.UserId
--	--WHERE U.UserName = @UserName
--	GROUP BY U.UserName
--END
--GO

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
		,COUNT(MD.[Name]) AS [Count]
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
	SELECT MK.[Name]
		,DateAdded
		,Email
	FROM Makes AS MK
	INNER JOIN AspNetUsers U ON U.Id = MK.UserId
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
	SELECT MK.[Name]
		,MD.[Name]
		,MD.DateAdded
		,Email
	FROM Models AS MD
	INNER JOIN Makes AS MK ON MK.MakeId = MD.MakeId
	INNER JOIN AspNetUsers U ON U.Id = MD.UserId
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
	SELECT UserId
		,ModelId
		,BodyStyleId
		,InteriorColorId
		,ExteriorColorId
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
	,@VehicleId INT
	,@UserId NVARCHAR(128)
	,@Name NVARCHAR(50)
	,@Phone NVARCHAR(15)
	,@Email NVARCHAR(50)
	,@Message NVARCHAR(500)
	)
AS
BEGIN
	INSERT INTO Contacts (
		VehicleId
		,UserId
		,[Name]
		,Phone
		,Email
		,[Message]
		)
	VALUES (
		@VehicleId
		,@UserId
		,@Name
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
	,@VehicleId INT
	,@UserId NVARCHAR(128)
	,@Name NVARCHAR(50)
	,@Phone NVARCHAR(15)
	,@Email NVARCHAR(50)
	,@Message NVARCHAR(500)
	)
AS
BEGIN
	SELECT ContactId
		,VehicleId
		,UserId
		,[Name]
		,Phone
		,Email
		,[Message]
	FROM Contacts
	WHERE ContactId = @ContactId
END
GO

