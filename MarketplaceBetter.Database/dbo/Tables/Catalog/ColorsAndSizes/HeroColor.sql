CREATE TABLE [dbo].[HeroColor]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[ProductId]	BIGINT					NOT NULL,
	[ColorId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_HeroColor]			PRIMARY KEY ([Id]),
	CONSTRAINT	[FK_HeroColor_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT	[FK_HeroColor_Color]	FOREIGN KEY ([ColorId])		REFERENCES [dbo].[Color] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_HeroColor_Product  ON [dbo].[HeroColor] ([ProductId] ASC);
GO