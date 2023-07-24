CREATE TABLE [dbo].[Parent]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Sku]			NVARCHAR(255)					NOT NULL,
	[Asin]			NVARCHAR(255)					NULL,
	[ProductId]		BIGINT							NOT NULL,
	[InstanceId]	BIGINT							NOT NULL,
	[StatusId]		BIGINT							NOT NULL,
	[TemplateId]	BIGINT							NULL,
	[Category]		NVARCHAR(255)					NULL,
	[Comment]		NVARCHAR (max)					NULL,
	CONSTRAINT		[PK_Parent]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Parent_Product]		FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT		[FK_Parent_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_Parent_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[EntityStatus] ([Id]),
	CONSTRAINT		[FK_Parent_Template]	FOREIGN KEY ([TemplateId])	REFERENCES [dbo].[Template] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Parent_Product_Instance  ON [dbo].[Parent] ([ProductId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_Parent_Sku  ON [dbo].[Parent] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_Parent_Asin  ON [dbo].[Parent] ([Asin] ASC) WHERE [Asin] IS NOT NULL;
GO