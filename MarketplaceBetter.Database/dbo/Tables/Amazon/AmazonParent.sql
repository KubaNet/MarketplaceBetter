CREATE TABLE [dbo].[AmazonParent]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[ProductId]		BIGINT						NOT NULL,
	[InstanceId]	BIGINT						NOT NULL,
	[Sku]			NVARCHAR(255)				NOT NULL,
	[Asin]			NVARCHAR(255)				NULL,
	[ChildSku]		NVARCHAR(255)				NOT NULL,
	CONSTRAINT		[PK_AmazonParent]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonParent_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT		[FK_AmazonParent_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonParent_Product_Instance  ON [dbo].[AmazonParent] ([ProductId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonParent_Sku  ON [dbo].[AmazonParent] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonParent_Asin  ON [dbo].[AmazonParent] ([Asin] ASC) WHERE [Asin] IS NOT NULL;
GO