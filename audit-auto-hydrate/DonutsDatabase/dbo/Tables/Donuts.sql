CREATE TABLE dbo.Donuts (
	Id                  INT NOT NULL IDENTITY(1,1),
	Flavor              NVARCHAR(127) NOT NULL,
	Price               DECIMAL(10,2) NOT NULL,
	CreatedOn           DATETIMEOFFSET NOT NULL,
	LastModifiedOn      DATETIMEOFFSET NOT NULL,

	PRIMARY KEY CLUSTERED(Id ASC)
)