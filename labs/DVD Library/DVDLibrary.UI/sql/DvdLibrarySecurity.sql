USE MASTER
GO

IF NOT Exists (SELECT LoginName FROM master.dbo.syslogins
 WHERE NAME = 'DvdLibraryApp')
	CREATE LOGIN DvdLibraryApp WITH PASSWORD = 'testing123';
GO

USE DvdLibrary
GO

IF Exists (SELECT * FROM sys.database_principals
 WHERE NAME = 'DvdLibraryApp')
	DROP USER DvdLibraryApp;
	CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp;  
GO

SELECT
   sprocs.name,
   cmd = 'GRANT EXECUTE ON '
		+ sprocs.name
		+ ' TO DvdLibraryApp'
FROM sys.procedures sprocs


SELECT 'GRANT SELECT, INSERT, UPDATE, DELETE ON '
	 + name
	 + ' TO DvdLibraryApp;'
from sysobjects tables 
where type = 'U'