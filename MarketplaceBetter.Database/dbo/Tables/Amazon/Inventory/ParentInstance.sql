CREATE TABLE [dbo].[ParentInstance]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Sku]			NVARCHAR(255)					NOT NULL,
	[Asin]			NVARCHAR(255)					NULL,
	[ParentId]		BIGINT							NOT NULL,
	[InstanceId]	BIGINT							NOT NULL,
	CONSTRAINT		[PK_ParentInstance]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ParentInstance_Parent]		FOREIGN KEY ([ParentId])	REFERENCES [dbo].[Parent] ([Id]),
	CONSTRAINT		[FK_ParentInstance_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ParentInstance_Parent_Instance  ON [dbo].[ParentInstance] ([ParentId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_ParentInstance_Sku  ON [dbo].[ParentInstance] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_ParentInstance_Asin  ON [dbo].[ParentInstance] ([Asin] ASC) WHERE [Asin] IS NOT NULL;
GO