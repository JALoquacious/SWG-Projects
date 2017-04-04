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
	INSERT INTO BodyStyles ([Description])
	VALUES
		 ('Car')
		,('SUV')
		,('Truck')
		,('Van')

	INSERT INTO ExteriorColors ([Name])
	VALUES
		 ('Black')
		,('White')
		,('Silver')
		,('Gray')
		,('Charcoal')
		,('Brown')
		,('Red')
		,('Orange')
		,('Yellow')
		,('Green')
		,('Blue')
		,('DarkRed')
		,('DarkBlue')

	INSERT INTO InteriorColors ([Name])
	VALUES
		 ('Black')
		,('White')
		,('Gray')
		,('Tan')
		,('Beige')
		,('DarkRed')
		,('DarkBlue')

	INSERT INTO Makes ([Name], UserId)
	VALUES
		 ('Audi', '00000000-0000-0000-0000-000000000000')
		,('BMW', '00000000-0000-0000-0000-000000000000')
		,('Chevrolet', '00000000-0000-0000-0000-000000000000')
		,('Ford', '00000000-0000-0000-0000-000000000000')
		,('Honda', '11111111-1111-1111-1111-111111111111')
		,('Nissan', '11111111-1111-1111-1111-111111111111')
		,('Tesla', '11111111-1111-1111-1111-111111111111')
		,('Toyota', '11111111-1111-1111-1111-111111111111')

	INSERT INTO Models (MakeId, [Name], [Year], UserId)
	VALUES
		 -- Audi
		 (1, 'A4', 2017, '00000000-0000-0000-0000-000000000000')
		,(1, 'A4', 2017, '00000000-0000-0000-0000-000000000000')
		,(1, 'A4', 2017, '00000000-0000-0000-0000-000000000000')
		,(1, 'Q5', 2015, '00000000-0000-0000-0000-000000000000')
		,(1, 'R8', 2017, '11111111-1111-1111-1111-111111111111')
		 -- BMW
		,(2, 'Z4', 2014, '00000000-0000-0000-0000-000000000000')
		,(2, 'X5', 2017, '00000000-0000-0000-0000-000000000000')
		,(2, 'M6', 2013, '11111111-1111-1111-1111-111111111111')
		,(2, 'M6', 2013, '11111111-1111-1111-1111-111111111111')
		 -- Chevrolet
		,(3, 'Tahoe', 2011, '00000000-0000-0000-0000-000000000000')
		,(3, 'Colorado', 2012, '00000000-0000-0000-0000-000000000000')
		,(3, 'Corvette', 2017, '11111111-1111-1111-1111-111111111111')
		,(3, 'Corvette', 2017, '11111111-1111-1111-1111-111111111111')
		 -- Ford
		,(4, 'F-150', 2003, '00000000-0000-0000-0000-000000000000')
		,(4, 'Fusion', 2009, '00000000-0000-0000-0000-000000000000')
		,(4, 'Explorer', 2016, '11111111-1111-1111-1111-111111111111')
		,(4, 'Explorer', 2016, '11111111-1111-1111-1111-111111111111')
		 -- Honda
		,(5, 'Accord', 2004, '00000000-0000-0000-0000-000000000000')
		,(5, 'Ridgeline', 2011, '00000000-0000-0000-0000-000000000000')
		,(5, 'Odyssey', 2010, '11111111-1111-1111-1111-111111111111')
		,(5, 'Odyssey', 2010, '11111111-1111-1111-1111-111111111111')
		 -- Nissan
		,(6, 'GT-R', 2015, '00000000-0000-0000-0000-000000000000')
		,(6, 'Frontier', 2001, '00000000-0000-0000-0000-000000000000')
		,(6, 'Pathfinder', 2013, '11111111-1111-1111-1111-111111111111')
		,(6, 'Pathfinder', 2013, '11111111-1111-1111-1111-111111111111')
		 -- Tesla
		,(7, 'S', 2013, '00000000-0000-0000-0000-000000000000')
		,(7, 'X', 2016, '00000000-0000-0000-0000-000000000000')
		,(7, '3', 2018, '11111111-1111-1111-1111-111111111111')
		,(7, '3', 2018, '11111111-1111-1111-1111-111111111111')
		 -- Toyota
		,(8, 'Land Cruiser', 2009, '00000000-0000-0000-0000-000000000000')
		,(8, 'Camry', 2012, '00000000-0000-0000-0000-000000000000')
		,(8, 'Tacoma', 1998, '11111111-1111-1111-1111-111111111111')
		,(8, 'Tacoma', 1998, '11111111-1111-1111-1111-111111111111')

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
		 '00000000-0000-0000-0000-000000000000' -- UserId
		,1 -- ModelId
		,1 -- BodyStyleId
		,4 -- InteriorColorId
		,11 -- ExteriorColorId
		,32000 -- SalePrice
		,34900 -- MSRP
		,236 -- Mileage
		,'1234567890ABCDEFG' -- VIN
		,'A shiny new Audi sedan, just waiting for you.' -- [Description]
		,0 -- IsUsed
		,1 -- IsAutomatic
		,1 -- IsFeatured
		,'placeholder.img' -- [Image]
		)

	INSERT INTO Specials (
		 SpecialId
		,[Name]
		,[Description]
		)
	VALUES (
		 1
		,'Free cup of joe'
		,'Get a steaming hot cup of coffee with the purchase of a new car.'
		)
		,(
		 2
		,'Green shirt special'
		,'You''ve got the luck of a leprechaun! $100 off for wearing a green shirt.'
		)

	INSERT INTO Salespersons (
		 FirstName
		,LastName
		)
	VALUES (
		 'John'
		,'Smith'
		)
		,(
		 'Jane'
		,'Doe'
		)

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
		 '00000000-0000-0000-0000-000000000000' -- UserId
		,'Jason Bourne' -- [Name]
		,'5558675309' -- Phone
		,NULL -- Email
		,'123 Main Street' -- Street1
		,'Deposit Box G6Y13' -- Street2
		,'Podunk' -- City
		,2 -- StateId
		,'75204' -- Zip
		)

	INSERT INTO PaymentTypes ([Description])
	VALUES
		 ('Bank-financed')
		,('Cash')
		,('Dealer-financed')

	INSERT INTO Sales (
		 VehicleId
		,CustomerId
		,SalesPersonId
		,PaymentTypeId
		,PurchasePrice
		,[Date]
		)
	VALUES (
		 11 -- VehicleId
		,3 -- CustomerId
		,2 -- SalesPersonId
		,23740 -- PurchasePrice
		,1 -- PurchaseTypeId
		,'4/1/2017' -- [Date]
		)

	INSERT INTO Contacts (
		 [Name]
		,Phone
		,Email
		)
	VALUES (
		'Cornelius Vanderbilt'
		,NULL
		,'cornelius@grandcentral.com'
		)

	--INSERT INTO Communications (
	--	 ContactId
	--	,VehicleId
	--	,[Message]
	--	)
	--VALUES (
	--	 1
	--	,NULL
	--	,'Just how good is that coffee that comes with the special?'
	--	)
END