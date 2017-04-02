USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='GuildCars')
	DROP DATABASE GuildCars
	CREATE DATABASE GuildCars
GO