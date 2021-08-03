CREATE TABLE [dbo].[AmazonParent]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[InstanceId]	BIGINT						NOT NULL,
	[ProductId]		BIGINT						NOT NULL,
	[Sku]			NVARCHAR(255)				NOT NULL,
	[Asin]			NVARCHAR(255)				NOT NULL,
	CONSTRAINT		[PK_AmazonParent]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonParent_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_AmazonParent_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonParent_Sku  ON [dbo].[AmazonParent] ([Sku] ASC);
GO