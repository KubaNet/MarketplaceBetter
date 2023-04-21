CREATE TABLE [dbo].[ProductDimensions]
(
	[Id]					BIGINT							NOT NULL	IDENTITY,
	[ProductId]				BIGINT							NOT NULL,
	[SizeId]				BIGINT							NOT NULL,
	[WeightInGrams]			FLOAT							NULL,
	[WeightInPounds]		FLOAT							NULL,
	[DepthInCentimeters]	FLOAT							NULL,
	[DepthInInches]			FLOAT							NULL,
	[LengthInCentimeters]	FLOAT							NULL,
	[LengthInInches]		FLOAT							NULL,
	[WidthInCentimeters]	FLOAT							NULL,
	[WidthInInches]			FLOAT							NULL,
	[HeightInCentimeters]	FLOAT							NULL,
	[HeightInInches]		FLOAT							NULL,
	CONSTRAINT				[PK_ProductDimensions]			PRIMARY KEY ([Id]),
	CONSTRAINT				[FK_ProductDimensions_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT				[FK_ProductDimensions_Size]		FOREIGN KEY ([SizeId])		REFERENCES [dbo].[Size] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductDimensions_Product_Size  ON [dbo].[ProductDimensions] ([ProductId] ASC, [SizeId] ASC);
GO