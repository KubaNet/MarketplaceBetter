CREATE TABLE [dbo].[Variant]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Sku]		NVARCHAR (255)			NOT NULL,
	[ProductId]	BIGINT					NOT NULL,
	[ColorId]	BIGINT					NOT NULL,
	[SizeId]	BIGINT					NOT NULL,
	[Ean]		NVARCHAR (255)			NULL,
	[StatusId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_Variant]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_Variant_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT  [FK_Variant_Color]		FOREIGN KEY ([ColorId])		REFERENCES [dbo].[Color] ([Id]),
	CONSTRAINT  [FK_Variant_Size]		FOREIGN KEY ([SizeId])		REFERENCES [dbo].[Size] ([Id]),
	CONSTRAINT  [FK_Variant_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[EntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Variant_Sku  ON [dbo].[Variant] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_Variant_Product_Color_Size  ON [dbo].[Variant] ([ProductId] ASC, [ColorId] ASC, [SizeId] ASC);
GO