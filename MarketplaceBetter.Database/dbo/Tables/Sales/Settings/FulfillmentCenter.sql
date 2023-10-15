CREATE TABLE [dbo].[FulfillmentCenter]
(
	[Id]			BIGINT							NOT NULL					IDENTITY,
	[Code]			NVARCHAR (255)					NOT NULL,
	[CountryId]		BIGINT							NOT NULL,
	CONSTRAINT		[PK_FulfillmentCenter]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_FulfillmentCenter_Country]	FOREIGN KEY ([CountryId])	REFERENCES [dbo].[Country] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_FulfillmentCenter_Code  ON [dbo].[FulfillmentCenter] ([Code] ASC);
GO