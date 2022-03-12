CREATE TABLE [dbo].[ProductVariant]
(
	[Id]		BIGINT						NOT NULL	IDENTITY,
	[Sku]		NVARCHAR (255)				NOT NULL,
	[ProductId]	BIGINT						NOT NULL,
	[ColorId]	BIGINT						NOT NULL,
	[SizeId]	BIGINT						NOT NULL,
	CONSTRAINT	[PK_ProductVariant]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_ProductVariant_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT  [FK_ProductVariant_Color]	FOREIGN KEY ([ColorId])		REFERENCES [dbo].[Color] ([Id]),
	CONSTRAINT  [FK_ProductVariant_Size]	FOREIGN KEY ([SizeId])		REFERENCES [dbo].[Size] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductVariant_Sku  ON [dbo].[ProductVariant] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_ProductVariant_Product_Color_Size  ON [dbo].[ProductVariant] ([ProductId] ASC, [ColorId] ASC, [SizeId] ASC);
GO