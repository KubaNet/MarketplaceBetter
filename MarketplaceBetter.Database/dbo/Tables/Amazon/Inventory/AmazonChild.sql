CREATE TABLE [dbo].[AmazonChild]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Sku]				NVARCHAR(255)				NOT NULL,
	[Asin]				NVARCHAR(255)				NULL,
	[ParentId]			BIGINT						NOT NULL,
	[VariantId]			BIGINT						NOT NULL,
	CONSTRAINT			[PK_AmazonChild]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_AmazonChild_Parent]		FOREIGN KEY ([ParentId])	REFERENCES [dbo].[AmazonParent] ([Id]),
	CONSTRAINT			[FK_AmazonChild_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonChild_Parent_Variant  ON [dbo].[AmazonChild] ([ParentId] ASC, [VariantId] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonChild_Sku  ON [dbo].[AmazonChild] ([Sku] ASC);
GO