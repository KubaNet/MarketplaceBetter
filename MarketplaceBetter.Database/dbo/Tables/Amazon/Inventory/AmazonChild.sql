CREATE TABLE [dbo].[AmazonChild]
(
	[Id]				BIGINT							NOT NULL	IDENTITY,
	[Sku]				NVARCHAR(255)					NOT NULL,
	[Asin]				NVARCHAR(255)					NULL,
	[ParentId]			BIGINT							NOT NULL,
	[ProductVariantId]	BIGINT							NOT NULL,
	CONSTRAINT			[PK_AmazonChild]				PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_AmazonChild_Parent]			FOREIGN KEY ([ParentId])			REFERENCES [dbo].[AmazonParent] ([Id]),
	CONSTRAINT			[FK_AmazonChild_ProductVariant]	FOREIGN KEY ([ProductVariantId])	REFERENCES [dbo].[ProductVariant] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonChild_Parent_ProductVariant  ON [dbo].[AmazonChild] ([ParentId] ASC, [ProductVariantId] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonChild_Sku  ON [dbo].[AmazonChild] ([Sku] ASC);
GO