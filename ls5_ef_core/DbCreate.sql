use master
go
create database Blogging
go
use Blogging
go
create table dbo . Blogs
(
BlogId int primary key identity ,
	Name nvarchar ( 100 ) not null
)
go
create table dbo . Posts
(
PostId int primary key identity ,
Title nvarchar ( 100 ) not null,
Content nvarchar ( 255 ) not null,
BlogId int foreign key references dbo . Blogs ( BlogId )
)