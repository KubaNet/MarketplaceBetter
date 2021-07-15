CREATE TABLE [dbo].[Product]
(
	[Id]		BIGINT				NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)		NOT NULL,
	[BrandId]	BIGINT				NOT NULL,
	CONSTRAINT	[PK_Product]		PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_Product_Brand]	FOREIGN KEY ([BrandId])	REFERENCES [dbo].[Brand] ([Id]),
);
GO