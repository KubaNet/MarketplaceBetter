CREATE TABLE [dbo].[Child]
(
	[Id]				BIGINT				NOT NULL	IDENTITY,
	[Sku]				NVARCHAR(255)		NOT NULL,
	[Asin]				NVARCHAR(255)		NULL,
	[ParentId]			BIGINT				NOT NULL,
	[VariantId]			BIGINT				NOT NULL,
	[StatusId]			BIGINT				NOT NULL	DEFAULT(3),
	CONSTRAINT			[PK_Child]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Child_Parent]	FOREIGN KEY ([ParentId])	REFERENCES [dbo].[Parent] ([Id]),
	CONSTRAINT			[FK_Child_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT			[FK_Child_Status]	FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AmazonEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Child_Parent_Variant  ON [dbo].[Child] ([ParentId] ASC, [VariantId] ASC);
GO

CREATE UNIQUE INDEX UIX_Child_Sku  ON [dbo].[Child] ([Sku] ASC);
GO