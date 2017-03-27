USE DvdLibrary
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
	DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM Dvd;
	DBCC CHECKIDENT('Dvd', RESEED, 1);

-- Seed Ratings
----------------------------------------------------------------------------
--SET IDENTITY_INSERT Rating ON

--INSERT INTO Rating (Id, [Description])
--VALUES
--	(1, 'G'),
--	(2, 'PG'),
--	(3, 'PG-13'),
--	(4, 'R'),
--	(5, 'NC-17')

--SET IDENTITY_INSERT Rating OFF
----------------------------------------------------------------------------


-- Seed Dvds
----------------------------------------------------------------------------
	SET IDENTITY_INSERT Dvd ON

	INSERT INTO Dvd (Id, Title, ReleaseYear, Director, Rating)
	VALUES
		(1, 'Back to the Future', '1985', 'Steven Spielberg', 'PG'),
		(2, 'The Wolf of Wall Street', '2013', 'Martin Scorsese', 'R'),
		(3, 'Inception', '2010', 'Christopher Nolan', 'PG-13'),
		(4, 'The Lion King', '1994', 'Roger Allers & Rob Minkoff', 'G'),
		(5, 'Alien', '1979', 'Ridley Scott', 'R'),
		(6, 'Into the Ol'' Moat', '1542', 'Arthur of Camelot', 'NC-17')

	SET IDENTITY_INSERT Dvd OFF
END
----------------------------------------------------------------------------