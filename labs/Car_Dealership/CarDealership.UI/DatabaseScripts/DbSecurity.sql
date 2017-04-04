USE MASTER
GO

IF Exists (SELECT LoginName FROM master.dbo.syslogins 
		   WHERE NAME = 'GuildCars')
	DROP LOGIN GuildCarsLogin
CREATE LOGIN GuildCarsLogin WITH PASSWORD = 'cars123';  
GO

USE GuildCars
GO

IF Exists (SELECT * FROM sys.database_principals
		   WHERE NAME = 'GuildCars')
	DROP USER GuildCarsUser
CREATE USER GuildCarsUser FOR LOGIN GuildCarsLogin;  
GO  

GRANT CREATE TABLE, EXECUTE TO GuildCarsUser
ALTER ROLE db_datawriter ADD MEMBER GuildCarsUser
ALTER ROLE db_datareader ADD MEMBER GuildCarsUser
ALTER ROLE db_ddladmin ADD MEMBER GuildCarsUser