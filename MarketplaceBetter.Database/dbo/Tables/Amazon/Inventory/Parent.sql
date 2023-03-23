CREATE TABLE [dbo].[Parent]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Sku]			NVARCHAR(255)		NOT NULL,
	[ProductId]		BIGINT				NOT NULL,
	CONSTRAINT		[PK_Parent]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Parent_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Parent_Product  ON [dbo].[Parent] ([ProductId] ASC);
GO

CREATE UNIQUE INDEX UIX_Parent_Sku  ON [dbo].[Parent] ([Sku] ASC);
GO