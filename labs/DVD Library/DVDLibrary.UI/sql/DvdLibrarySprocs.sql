USE DvdLibrary
GO

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdsSelectAll')
		drop procedure DvdsSelectAll
GO

create procedure DvdsSelectAll as
begin
	select *
	from Dvd
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdSelectById')
		drop procedure DvdSelectById
GO

create procedure DvdSelectById (
	@Id int
)
as
begin
	select *
	from Dvd
	where Id = @Id
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdsSelectByTitle')
		drop procedure DvdsSelectByTitle
GO

create procedure DvdsSelectByTitle (
	@Title nvarchar(100)
)
as
begin
	select *
	from Dvd
	where Title LIKE '%' + @Title + '%';
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdsSelectByReleaseYear')
		drop procedure DvdsSelectByReleaseYear
GO

create procedure DvdSelectByReleaseYear (
	@ReleaseYear int
)
as
begin
	select *
	from Dvd
	where ReleaseYear LIKE '%' + @ReleaseYear + '%';
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdsSelectByDirector')
		drop procedure DvdsSelectByDirector
GO

create procedure DvdsSelectByDirector (
	@Director nvarchar(50)
)
as
begin
	select *
	from Dvd
	where Director LIKE '%' + @Director + '%';
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdsSelectByRating')
		drop procedure DvdsSelectByRating
GO

create procedure DvdsSelectByRating (
	@Rating char(5)
)
as
begin
	select *
	from Dvd
	where Rating LIKE '%' + @Rating + '%';
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdInsert')
		drop procedure DvdInsert
GO

create procedure DvdInsert (
	@Id int output,
	@Title nvarchar(100),
	@ReleaseYear int,
	@Director nvarchar(50),
	@Rating char(5),
	@Notes nvarchar(500)
)
as
begin
	insert into Dvd (Title, ReleaseYear, Director, Rating, Notes)
	values (@Title, @ReleaseYear, @Director, @Rating, @Notes)
	set @Id = SCOPE_IDENTITY();
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdUpdate')
		drop procedure DvdUpdate
GO

create procedure DvdUpdate (
	@Id int output,
	@Title nvarchar(100),
	@ReleaseYear int,
	@Director nvarchar(50),
	@Rating char(5),
	@Notes nvarchar(500)
)
as
begin
	update Dvd set
		Title = @Title,
		ReleaseYear = @ReleaseYear,
		Director = @Director,
		Rating = @Rating,
		Notes = @Notes
	where Id = @Id
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DvdDelete')
		drop procedure DvdDelete
GO

create procedure DvdDelete (
	@Id int
) as
begin
	begin transaction
	delete from Dvd where Id = @Id;
	commit transaction
end
go