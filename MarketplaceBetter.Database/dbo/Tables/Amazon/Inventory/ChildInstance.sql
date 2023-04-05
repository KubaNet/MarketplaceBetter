CREATE TABLE [dbo].[ChildInstance]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Sku]				NVARCHAR(255)				NOT NULL,
	[ChildId]			BIGINT						NOT NULL,
	[InstanceId]		BIGINT						NOT NULL,
	[StatusId]			BIGINT						NOT NULL	DEFAULT(3),
	CONSTRAINT			[PK_ChildInstance]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_ChildInstance_Child]	FOREIGN KEY ([ChildId])		REFERENCES [dbo].[Child] ([Id]),
	CONSTRAINT			[FK_ChildInstance_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_ChildInstance_Status]	FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AmazonEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ChildInstance_Child_Instance  ON [dbo].[ChildInstance] ([ChildId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_ChildInstance_Sku  ON [dbo].[ChildInstance] ([Sku] ASC);
GO