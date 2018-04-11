CREATE DATABASE [Doors]
GO
USE [Doors]
GO

CREATE TABLE [dbo].[rooms](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[name] [sysname] NOT NULL,
	[number] [int] NOT NULL
						  )


CREATE TABLE [dbo].[department](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY, 
	[name] [sysname] NOT NULL
						       )


CREATE TABLE [dbo].[workers](
	[id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[name] [sysname] NOT NULL,
	[id_department] [int] REFERENCES [dbo].[department] ([id])
						    )


CREATE TABLE [dbo].[access_workers](
	[id_workers] [int] NOT NULL REFERENCES [dbo].[workers] ([id]),
	[id_rooms] [int] NOT NULL REFERENCES [dbo].[rooms] ([id]),
    CONSTRAINT new_pk_work PRIMARY KEY ([id_workers] , [id_rooms])
								   )


CREATE TABLE [dbo].[access_department](
	[id_department] [int] NOT NULL REFERENCES [dbo].[department] ([id]),
	[id_rooms] [int] NOT NULL REFERENCES [dbo].[rooms] ([id]),
    CONSTRAINT new_pk_departaent PRIMARY KEY ([id_department] , [id_rooms])
									  )


Insert into rooms values
    ('conference hall','1'),
	('working area 2','3'),
	('cafeteria','6'),
	('working area 37','125')

Insert into department values
    ('development'),
	('testing'),
	('marketing'),
	('research')

Insert into workers values
    ('Бруско Василий Геннадьевич','1'),
	('Мартынова Анна Петровна','3'),
	('Ковальчук Евгений Сергеевич','4'),
	('Синаев Максим Викторович',NULL)



