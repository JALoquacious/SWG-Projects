USE GuildCars
GO

IF EXISTS (
		SELECT *
		FROM INFORMATION_SCHEMA.ROUTINES
		WHERE ROUTINE_NAME = 'DbReset'
		)
	DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset
AS
BEGIN
	DELETE FROM AspNetRoles;
	DELETE FROM AspNetUserRoles;
	DELETE FROM Contacts;
	DELETE FROM Sales;
	DELETE FROM Customers;
	DELETE FROM States;
	DELETE FROM Specials;
	DELETE FROM PaymentTypes;
	DELETE FROM Vehicles;
	DELETE FROM Models;
	DELETE FROM Makes;
	DELETE FROM ExteriorColors;
	DELETE FROM InteriorColors;
	DELETE FROM BodyStyles;
	DELETE FROM AspNetUsers WHERE Id IN ('00000000-0000-0000-0000-000000000000', '11111111-1111-1111-1111-111111111111', '22222222-2222-2222-2222-222222222222');

	DBCC CHECKIDENT ('Makes', RESEED, 1)
	DBCC CHECKIDENT ('Models', RESEED, 1)
	DBCC CHECKIDENT ('Vehicles', RESEED, 1)
	DBCC CHECKIDENT ('Sales', RESEED, 1)
	DBCC CHECKIDENT ('Specials', RESEED, 1)
	DBCC CHECKIDENT ('Customers', RESEED, 1)

	INSERT INTO AspNetRoles (Id, [Name])
	VALUES
	('1', 'Admin')
	,('2', 'Sales')
	,('3', 'Disabled')

	insert into AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName)
	values('00000000-0000-0000-0000-000000000000', 0, 0, 'One1ABC@test.com', 0, 0, 0, 'test1', 'Bobby', 'Tables');
	insert into AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName)
	values('11111111-1111-1111-1111-111111111111', 0, 0, 'Two2DEF@test.com', 0, 0, 0, 'test2', 'Sally', 'Strings');
	insert into AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName)
	values('22222222-2222-2222-2222-222222222222', 0, 0, 'Three3GHI@test.com', 0, 0, 0, 'test3', 'Davey', 'Delegates');

	INSERT INTO AspNetUserRoles (UserId, RoleId)
	VALUES
	('00000000-0000-0000-0000-000000000000', '1')
	,('11111111-1111-1111-1111-111111111111', '2')
	,('22222222-2222-2222-2222-222222222222', '3')

	SET IDENTITY_INSERT BodyStyles ON;
	INSERT INTO BodyStyles (BodyStyleId, [Name])
	VALUES
		 (1, 'Car')
		,(2, 'SUV')
		,(3, 'Truck')
		,(4, 'Van')
	SET IDENTITY_INSERT BodyStyles OFF;


	SET IDENTITY_INSERT ExteriorColors ON;
	INSERT INTO ExteriorColors (ExteriorColorId, [Name])
	VALUES
		 (1, 'Black')
		,(2, 'White')
		,(3, 'Silver')
		,(4, 'Gray')
		,(5, 'Charcoal')
		,(6, 'Brown')
		,(7, 'Red')
		,(8, 'Orange')
		,(9, 'Yellow')
		,(10, 'Green')
		,(11, 'Blue')
		,(12, 'DarkRed')
		,(13, 'DarkBlue')
	SET IDENTITY_INSERT ExteriorColors OFF;


	SET IDENTITY_INSERT InteriorColors ON;
	INSERT INTO InteriorColors (InteriorColorId, [Name])
	VALUES
		 (1, 'Black')
		,(2, 'White')
		,(3, 'Gray')
		,(4, 'Tan')
		,(5, 'Beige')
		,(6, 'DarkRed')
		,(7, 'DarkBlue')
	SET IDENTITY_INSERT InteriorColors OFF;


	SET IDENTITY_INSERT Makes ON;
	INSERT INTO Makes (MakeId, [Name], UserId)
	VALUES
		 (1, 'Audi', '00000000-0000-0000-0000-000000000000')
		,(2, 'BMW', '00000000-0000-0000-0000-000000000000')
		,(3, 'Chevrolet', '00000000-0000-0000-0000-000000000000')
		,(4, 'Ford', '00000000-0000-0000-0000-000000000000')
		,(5, 'Honda', '11111111-1111-1111-1111-111111111111')
		,(6, 'Nissan', '11111111-1111-1111-1111-111111111111')
		,(7, 'Tesla', '11111111-1111-1111-1111-111111111111')
		,(8, 'Toyota', '11111111-1111-1111-1111-111111111111')
		,(9, 'Kia', '22222222-2222-2222-2222-222222222222')
	SET IDENTITY_INSERT Makes OFF;


	SET IDENTITY_INSERT Models ON;
	INSERT INTO Models (ModelId, MakeId, [Name], [Year], UserId)
	VALUES
		 -- Audi
		 (1, 1, 'A4', 2017, '00000000-0000-0000-0000-000000000000')
		,(2, 1, 'Q5', 2015, '11111111-1111-1111-1111-111111111111')
		,(3, 1, 'R8', 2017, '22222222-2222-2222-2222-222222222222')
		 -- BMW
		,(4, 2, 'Z4', 2014, '00000000-0000-0000-0000-000000000000')
		,(5, 2, 'X5', 2017, '11111111-1111-1111-1111-111111111111')
		,(6, 2, 'M6', 2013, '22222222-2222-2222-2222-222222222222')
		 -- Chevrolet
		,(7, 3, 'Tahoe', 2011, '00000000-0000-0000-0000-000000000000')
		,(8, 3, 'Colorado', 2012, '11111111-1111-1111-1111-111111111111')
		,(9, 3, 'Corvette', 2017, '22222222-2222-2222-2222-222222222222')
		 -- Ford
		,(10, 4, 'F-150', 2003, '00000000-0000-0000-0000-000000000000')
		,(11, 4, 'Fusion', 2009, '11111111-1111-1111-1111-111111111111')
		,(12, 4, 'Explorer', 2016, '22222222-2222-2222-2222-222222222222')
		 -- Honda
		,(13, 5, 'Accord', 2004, '00000000-0000-0000-0000-000000000000')
		,(14, 5, 'Ridgeline', 2011, '11111111-1111-1111-1111-111111111111')
		,(15, 5, 'Odyssey', 2010, '22222222-2222-2222-2222-222222222222')
		 -- Nissan
		,(16, 6, 'GT-R', 2015, '00000000-0000-0000-0000-000000000000')
		,(17, 6, 'Frontier', 2001, '11111111-1111-1111-1111-111111111111')
		,(18, 6, 'Pathfinder', 2013, '22222222-2222-2222-2222-222222222222')
		 -- Tesla
		,(19, 7, 'S', 2013, '00000000-0000-0000-0000-000000000000')
		,(20, 7, 'X', 2016, '11111111-1111-1111-1111-111111111111')
		,(21, 7, '3', 2018, '22222222-2222-2222-2222-222222222222')
		 -- Toyota
		,(22, 8, 'Land Cruiser', 2009, '00000000-0000-0000-0000-000000000000')
		,(23, 8, 'Camry', 2012, '11111111-1111-1111-1111-111111111111')
		,(24, 8, 'Tacoma', 1998, '22222222-2222-2222-2222-222222222222')
		 -- Kia
		,(25, 9, 'Sedona', 2018, '00000000-0000-0000-0000-000000000000')
		,(26, 9, 'Optima', 2010, '11111111-1111-1111-1111-111111111111')
		,(27, 9, 'Soul', 2014, '22222222-2222-2222-2222-222222222222')
	SET IDENTITY_INSERT Models OFF;


	SET IDENTITY_INSERT Vehicles ON;
	INSERT INTO Vehicles (
		VehicleId
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
		)
	VALUES (
		1 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,1 -- ModelId
		,1 -- BodyStyleId
		,4 -- InteriorColorId
		,11 -- ExteriorColorId
		,NULL -- SaleId
		,32000 -- SalePrice
		,34900 -- MSRP
		,236 -- Mileage
		,'1234567890ABCDEFG' -- VIN
		,'A shiny new Audi sedan, just waiting for you.' -- [Description]
		,0 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'audi_a4.jpg' -- [Image]
		),
		(
		2 -- VehicleId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,7 -- ModelId
		,2 -- BodyStyleId
		,5 -- InteriorColorId
		,7 -- ExteriorColorId
		,NULL -- SaleId
		,28000 -- SalePrice
		,30000 -- MSRP
		,67500 -- Mileage
		,'2345678901ABCDEFG' -- VIN
		,'Enough space to live in.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'chevrolet_tahoe.jpg' -- [Image]
		),
		(
		3 -- VehicleId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,7 -- ModelId
		,2 -- BodyStyleId
		,5 -- InteriorColorId
		,7 -- ExteriorColorId
		,NULL -- SaleId
		,33000 -- SalePrice
		,35000 -- MSRP
		,25500 -- Mileage
		,'3456789012ABCDEFG' -- VIN
		,'Enough space to live in.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'chevrolet_tahoe.jpg' -- [Image]
		),
		(
		4 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,12 -- ModelId
		,2 -- BodyStyleId
		,3 -- InteriorColorId
		,11 -- ExteriorColorId
		,NULL -- SaleId
		,30000 -- SalePrice
		,32000 -- MSRP
		,950 -- Mileage
		,'4567890123ABCDEFG' -- VIN
		,'Explore the world.' -- [Description]
		,0 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'ford_explorer.jpg' -- [Image]
		),
		(
		5 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,14 -- ModelId
		,3 -- BodyStyleId
		,2 -- InteriorColorId
		,6 -- ExteriorColorId
		,NULL -- SaleId
		,19000 -- SalePrice
		,21000 -- MSRP
		,62000 -- Mileage
		,'5678901234ABCDEFG' -- VIN
		,'Honda''s only pickup model.' -- [Description]
		,1 -- IsUsed
		,0 -- IsAutomatic
		,1 -- IsFeatured
		,'honda_ridgeline.jpg' -- [Image]
		),
		(
		6 -- VehicleId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,4 -- ModelId
		,1 -- BodyStyleId
		,7 -- InteriorColorId
		,3 -- ExteriorColorId
		,NULL -- SaleId
		,42000 -- SalePrice
		,44000 -- MSRP
		,25000 -- Mileage
		,'6789012345ABCDEFG' -- VIN
		,'A sleek little roadster.' -- [Description]
		,1 -- IsUsed
		,0 -- IsAutomatic
		,1 -- IsFeatured
		,'bmw_z4.jpg' -- [Image]
		),
		(
		7 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,22 -- ModelId
		,3 -- BodyStyleId
		,1 -- InteriorColorId
		,4 -- ExteriorColorId
		,NULL -- SaleId
		,32000 -- SalePrice
		,33000 -- MSRP
		,180000 -- Mileage
		,'7890123456ABCDEFG' -- VIN
		,'Rough and tumble.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'toyota_land-cruiser.jpg' -- [Image]
		),
		(
		8 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,17 -- ModelId
		,2 -- BodyStyleId
		,6 -- InteriorColorId
		,1 -- ExteriorColorId
		,NULL -- SaleId
		,3200 -- SalePrice
		,3700 -- MSRP
		,290000 -- Mileage
		,'7890123456ABCDEFG' -- VIN
		,'Still has something left to give.' -- [Description]
		,1 -- IsUsed
		,0 -- IsAutomatic
		,0 -- IsFeatured
		,'nissan_frontier.jpg' -- [Image]
		),
		(
		9 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,3 -- ModelId
		,1 -- BodyStyleId
		,6 -- InteriorColorId
		,1 -- ExteriorColorId
		,NULL -- SaleId
		,160000 -- SalePrice
		,162900 -- MSRP
		,12 -- Mileage
		,'8901234567ABCDEFG' -- VIN
		,'King of the hill.' -- [Description]
		,0 -- IsUsed
		,0 -- IsAutomatic
		,1 -- IsFeatured
		,'audi_r8.jpg' -- [Image]
		),
		(
		10 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,15 -- ModelId
		,4 -- BodyStyleId
		,1 -- InteriorColorId
		,10 -- ExteriorColorId
		,NULL -- SaleId
		,18500 -- SalePrice
		,19500 -- MSRP
		,105000 -- Mileage
		,'9012345678ABCDEFG' -- VIN
		,'Bring the whole crew.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'honda_odyssey.jpg' -- [Image]
		),
		(
		11 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,25 -- ModelId
		,4 -- BodyStyleId
		,2 -- InteriorColorId
		,9 -- ExteriorColorId
		,NULL -- SaleId
		,25600 -- SalePrice
		,26900 -- MSRP
		,100 -- Mileage
		,'9876543210ABCDEFG' -- VIN
		,'Even soccer moms can have some style.' -- [Description]
		,0 -- IsUsed
		,0 -- IsAutomatic
		,1 -- IsFeatured
		,'kia_sedona.jpg' -- [Image]
		),
		(
		12 -- VehicleId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,8 -- ModelId
		,3 -- BodyStyleId
		,3 -- InteriorColorId
		,8 -- ExteriorColorId
		,NULL -- SaleId
		,23000 -- SalePrice
		,25000 -- MSRP
		,250 -- Mileage
		,'8765432109ABCDEFG' -- VIN
		,'Haul the tools and still have room to spare.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'chevrolet_colorado.jpg' -- [Image]
		),
		(
		13 -- VehicleId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,8 -- ModelId
		,3 -- BodyStyleId
		,4 -- InteriorColorId
		,6 -- ExteriorColorId
		,NULL -- SaleId
		,23000 -- SalePrice
		,25000 -- MSRP
		,250 -- Mileage
		,'7654321098ABCEDFG' -- VIN
		,'Haul the tools and still have room to spare.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'chevrolet_colorado.jpg' -- [Image]
		),
		(
		14 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,5 -- ModelId
		,2 -- BodyStyleId
		,7 -- InteriorColorId
		,6 -- ExteriorColorId
		,NULL -- SaleId
		,55000 -- SalePrice
		,56600 -- MSRP
		,250 -- Mileage
		,'6543210987ABCDEFG' -- VIN
		,'Get around the big city or the wild country the same way -- in style.' -- [Description]
		,0 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'bmw_x5.jpg' -- [Image]
		),
		(
		15 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,23 -- ModelId
		,1 -- BodyStyleId
		,2 -- InteriorColorId
		,5 -- ExteriorColorId
		,NULL -- SaleId
		,22000 -- SalePrice
		,23840 -- MSRP
		,90000 -- Mileage
		,'5432109876ABCDEFG' -- VIN
		,'Dependability you can count ON.' -- [Description]
		,1 -- IsUsed
		,0 -- IsAutomatic
		,1 -- IsFeatured
		,'toyota_camry.jpg' -- [Image]
		),
		(
		16 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,18 -- ModelId
		,2 -- BodyStyleId
		,1 -- InteriorColorId
		,2 -- ExteriorColorId
		,NULL -- SaleId
		,24000 -- SalePrice
		,25000 -- MSRP
		,90000 -- Mileage
		,'4321098765ABCDEFG' -- VIN
		,'Find your path.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'nissan_pathfinder.jpg' -- [Image]
		),
		(
		17 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,26 -- ModelId
		,1 -- BodyStyleId
		,3 -- InteriorColorId
		,2 -- ExteriorColorId
		,NULL -- SaleId
		,16500 -- SalePrice
		,17500 -- MSRP
		,105000 -- Mileage
		,'3210987654ABCDEFG' -- VIN
		,'Optimize your commute.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'kia_optima.jpg' -- [Image]
		),
		(
		18 -- VehicleId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,10 -- ModelId
		,3 -- BodyStyleId
		,4 -- InteriorColorId
		,3 -- ExteriorColorId
		,NULL -- SaleId
		,16500 -- SalePrice
		,17500 -- MSRP
		,105000 -- Mileage
		,'2109876543ABCDEFG' -- VIN
		,'America''s best-selling truck.' -- [Description]
		,1 -- IsUsed
		,0 -- IsAutomatic
		,1 -- IsFeatured
		,'ford_f150.jpg' -- [Image]
		),
		(
		19 -- VehicleId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,20 -- ModelId
		,2 -- BodyStyleId
		,5 -- InteriorColorId
		,4 -- ExteriorColorId
		,NULL -- SaleId
		,70500 -- SalePrice
		,72500 -- MSRP
		,9000 -- Mileage
		,'1098765432ABCDEFG' -- VIN
		,'Quiet as a mouse; powerful as a bear.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,0 -- IsFeatured
		,'tesla_x.jpg' -- [Image]
		),
		(
		20 -- VehicleId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,2 -- ModelId
		,2 -- BodyStyleId
		,6 -- InteriorColorId
		,5 -- ExteriorColorId
		,NULL -- SaleId
		,35000 -- SalePrice
		,36000 -- MSRP
		,20000 -- Mileage
		,'0987654321ABCDEFG' -- VIN
		,'Refinement and class in a smooth package.' -- [Description]
		,1 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'audi_q5.jpg' -- [Image]
		)
	SET IDENTITY_INSERT Vehicles OFF;


	SET IDENTITY_INSERT Specials ON;
	INSERT INTO Specials (
		 SpecialId
		,[Name]
		,[Description]
		)
	VALUES (
		 1
		,'Free cup o'' joe'
		,'Get a steaming hot cup of coffee with the purchase of a new car.'
		)
		,(
		 2
		,'Green shirt special'
		,'You''ve got the luck of a leprechaun! $100 OFF for wearing a green shirt.'
		)
		,(
		 3
		,'No shirt special'
		,'Other dealers require a shirt. Not us. $200 OFF your purchase for being brave.'
		)
		,(
		 4
		,'Recycling special'
		,'Bring in your recycling and get $25 OFF of a recycled car.'
		)
	SET IDENTITY_INSERT Specials OFF;


	INSERT INTO States (
		 StateId
		,[Name]
		)
	VALUES (
		 'OH'
		,'Ohio'
		)
		,(
		 'KY'
		,'Kentucky'
		)
		,(
		 'MN'
		,'Minnesota'
		);


	SET IDENTITY_INSERT Customers ON;
	INSERT INTO Customers (
		CustomerId
		,UserId
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
		1
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,'Jason Bourne' -- [Name]
		,'5558675309' -- Phone
		,NULL -- Email
		,'123 Main Street' -- Street1
		,'Deposit Box G6Y13' -- Street2
		,'Podunk' -- City
		,'KY' -- StateId
		,'75204' -- Zip
		),
		(
		2
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,'Clark Kent' -- [Name]
		,NULL -- Phone
		,'kent@dailyplanet.com' -- Email
		,'123 Exchange Street' -- Street1
		,'Apt. B' -- Street2
		,'New Ohio' -- City
		,'OH' -- StateId
		,'12345' -- Zip
		),
		(
		3
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,'Johnny Loops' -- [Name]
		,NULL -- Phone
		,'johnny@loops.com' -- Email
		,'123 South Street' -- Street1
		,'Apt. 1' -- Street2
		,'Loopville' -- City
		,'MN' -- StateId
		,'54321' -- Zip
		)
	SET IDENTITY_INSERT Customers OFF;


	SET IDENTITY_INSERT PaymentTypes ON;
	INSERT INTO PaymentTypes (PaymentTypeId, [Description])
	VALUES
		 (1, 'Bank Finance')
		,(2, 'Cash')
		,(3, 'Dealer Finance')
	SET IDENTITY_INSERT PaymentTypes OFF;


	SET IDENTITY_INSERT Sales ON;
	INSERT INTO Sales (
		SaleId
		,CustomerId
		,UserId
		,PaymentTypeId
		,PurchasePrice
		,[Date]
		)
	VALUES (
		1 -- SaleId
		,1 -- CustomerId
		,'00000000-0000-0000-0000-000000000000' -- UserId
		,1 -- PaymentTypeId
		,11000 -- PurchasePrice
		,'4/1/2017' -- [Date]
		)
		,(
		2 -- SaleId
		,2 -- CustomerId
		,'11111111-1111-1111-1111-111111111111' -- UserId
		,2 -- PaymentTypeId
		,22000 -- PurchasePrice
		,'4/2/2017' -- [Date]
		)
		,(
		3 -- SaleId
		,3 -- CustomerId
		,'22222222-2222-2222-2222-222222222222' -- UserId
		,3 -- PaymentTypeId
		,33000 -- PurchasePrice
		,'4/3/2017' -- [Date]
		)
	SET IDENTITY_INSERT Sales OFF;


	SET IDENTITY_INSERT Contacts ON;
	INSERT INTO Contacts (
		ContactId
		,[Name]
		,Phone
		,Email
		,[Message]
		)
	VALUES (
		1
		,'Cornelius Vanderbilt'
		,NULL
		,'cornelius@grandcentral.com'
		,'Just how good is that cOFFee that comes with the special?'
		)
		,(
		2
		,'John Rockefeller'
		,NULL
		,'rockefeller@thebank.com'
		,'I''m deeply interested in that No Shirt Special and the Audi R8...'
		)
	SET IDENTITY_INSERT Contacts OFF;

END