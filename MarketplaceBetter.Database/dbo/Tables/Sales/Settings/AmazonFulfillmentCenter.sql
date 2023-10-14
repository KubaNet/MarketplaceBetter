CREATE TABLE [dbo].[AmazonFulfillmentCenter]
(
	[Id]			BIGINT				NOT NULL			IDENTITY,
	[Code]			NVARCHAR (255)		NOT NULL,
	[CountryId]		BIGINT				NOT NULL,
	CONSTRAINT		[PK_AmazonFulfillmentCenter]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonFulfillmentCenter_Country]	FOREIGN KEY ([CountryId])			REFERENCES [dbo].[Country] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonFulfillmentCenter_Code  ON [dbo].[AmazonFulfillmentCenter] ([Code] ASC);
GO