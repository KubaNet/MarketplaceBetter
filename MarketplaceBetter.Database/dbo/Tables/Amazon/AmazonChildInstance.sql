CREATE TABLE [dbo].[AmazonChildInstance]
(
	[Id]				BIGINT								NOT NULL	IDENTITY,
	[ChildId]			BIGINT								NOT NULL,
	[InstanceId]		BIGINT								NOT NULL,
	[Sku]				NVARCHAR(255)						NOT NULL,
	CONSTRAINT			[PK_AmazonChildInstance]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_AmazonChildInstance_Child]		FOREIGN KEY ([ChildId])		REFERENCES [dbo].[AmazonChild] ([Id]),
	CONSTRAINT			[FK_AmazonChildInstance_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonChildInstance_Child_Instance  ON [dbo].[AmazonChildInstance] ([ChildId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonChildInstance_Sku  ON [dbo].[AmazonChildInstance] ([Sku] ASC);
GO