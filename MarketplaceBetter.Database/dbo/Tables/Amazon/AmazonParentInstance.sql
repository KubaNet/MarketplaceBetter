CREATE TABLE [dbo].[AmazonParentInstance]
(
	[Id]			BIGINT								NOT NULL	IDENTITY,
	[ParentId]		BIGINT								NOT NULL,
	[InstanceId]	BIGINT								NOT NULL,
	[Sku]			NVARCHAR(255)						NOT NULL,
	[Asin]			NVARCHAR(255)						NULL,
	CONSTRAINT		[PK_AmazonParentInstance]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonParentInstance_Parent]	FOREIGN KEY ([ParentId])	REFERENCES [dbo].[AmazonParent] ([Id]),
	CONSTRAINT		[FK_AmazonParentInstance_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonParentInstance_Parent_Instance  ON [dbo].[AmazonParentInstance] ([ParentId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonParentInstance_Sku  ON [dbo].[AmazonParentInstance] ([Sku] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonParentInstance_Asin  ON [dbo].[AmazonParentInstance] ([Asin] ASC) WHERE [Asin] IS NOT NULL;
GO