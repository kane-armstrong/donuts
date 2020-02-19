﻿CREATE TABLE dbo.Donuts (
	Id                  UNIQUEIDENTIFIER NOT NULL,
	Flavor              NVARCHAR(127) NOT NULL,
	Price               DECIMAL(10,2) NOT NULL,
	CreatedOn           DATETIMEOFFSET NOT NULL,
	CreatedBy           UNIQUEIDENTIFIER NOT NULL,
	LastModifiedOn      DATETIMEOFFSET NOT NULL,
	LastModifiedBy      UNIQUEIDENTIFIER NOT NULL
	PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_Donuts_CreatedBy FOREIGN KEY(CreatedBy) REFERENCES Users(Id),
	CONSTRAINT FK_Donuts_LastModifiedBy FOREIGN KEY(LastModifiedBy) REFERENCES Users(Id)
)