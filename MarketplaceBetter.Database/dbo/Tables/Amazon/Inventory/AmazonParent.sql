CREATE TABLE [dbo].[AmazonParent]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Sku]			NVARCHAR(255)				NOT NULL,
	[ChildSku]		NVARCHAR(255)				NOT NULL,
	[ProductId]		BIGINT						NOT NULL,
	CONSTRAINT		[PK_AmazonParent]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonParent_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonParent_Product  ON [dbo].[AmazonParent] ([ProductId] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonParent_Sku  ON [dbo].[AmazonParent] ([Sku] ASC);
GO