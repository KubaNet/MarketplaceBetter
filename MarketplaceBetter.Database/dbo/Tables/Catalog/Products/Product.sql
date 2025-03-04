CREATE TABLE [dbo].[Product]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[Code]			NVARCHAR (255)		NOT NULL,
	[BrandId]		BIGINT				NOT NULL,
	[CollectionId]	BIGINT				NOT NULL,
	[ColorGroupId]	BIGINT				NOT NULL,
	[SizeGroupId]	BIGINT				NOT NULL,
	[Order]			INT					NOT NULL,
	[StatusId]		BIGINT				NOT NULL,
	[Comment]		NVARCHAR (max)		NULL,
	[IsSizeCopy]	BIT					NOT NULL DEFAULT(0),
	CONSTRAINT	[PK_Product]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_Product_Brand]		FOREIGN KEY ([BrandId])			REFERENCES [dbo].[Brand] ([Id]),
	CONSTRAINT  [FK_Product_Collection]	FOREIGN KEY ([CollectionId])	REFERENCES [dbo].[Collection] ([Id]),
	CONSTRAINT  [FK_Product_ColorGroup]	FOREIGN KEY ([ColorGroupId])	REFERENCES [dbo].[ColorGroup] ([Id]),
	CONSTRAINT  [FK_Product_SizeGroup]	FOREIGN KEY ([SizeGroupId])		REFERENCES [dbo].[SizeGroup] ([Id]),
	CONSTRAINT  [FK_Product_Status]		FOREIGN KEY ([StatusId])		REFERENCES [dbo].[EntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Product_Brand_Name  ON [dbo].[Product] ([BrandId] ASC, [Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Product_Brand_Code  ON [dbo].[Product] ([BrandId] ASC, [Code] ASC);
GO