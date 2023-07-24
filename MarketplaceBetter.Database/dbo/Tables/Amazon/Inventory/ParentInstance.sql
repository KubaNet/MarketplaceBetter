CREATE TABLE [dbo].[ParentInstance]
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
	CONSTRAINT		[PK_ParentInstance]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ParentInstance_Product]		FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT		[FK_ParentInstance_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_ParentInstance_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[EntityStatus] ([Id]),
	CONSTRAINT		[FK_ParentInstance_Template]	FOREIGN KEY ([TemplateId])	REFERENCES [dbo].[Template] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ParentInstance_Product_Instance  ON [dbo].[ParentInstance] ([ProductId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_ParentInstance_Sku  ON [dbo].[ParentInstance] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_ParentInstance_Asin  ON [dbo].[ParentInstance] ([Asin] ASC) WHERE [Asin] IS NOT NULL;
GO