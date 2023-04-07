CREATE TABLE [dbo].[ChildInstance]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Sku]				NVARCHAR(255)				NOT NULL,
	[VariantId]			BIGINT						NOT NULL,
	[InstanceId]		BIGINT						NOT NULL,
	[StatusId]			BIGINT						NOT NULL,
	CONSTRAINT			[PK_ChildInstance]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_ChildInstance_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT			[FK_ChildInstance_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_ChildInstance_Status]	FOREIGN KEY ([StatusId])	REFERENCES [dbo].[EntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ChildInstance_Variant_Instance  ON [dbo].[ChildInstance] ([VariantId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_ChildInstance_Sku  ON [dbo].[ChildInstance] ([Sku] ASC);
GO