CREATE TABLE [dbo].[Child]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Sku]			NVARCHAR(255)				NOT NULL,
	[VariantId]		BIGINT						NOT NULL,
	[InstanceId]	BIGINT						NOT NULL,
	[StatusId]		BIGINT						NOT NULL,
	[Comment]		NVARCHAR (max)				NULL,
	CONSTRAINT		[PK_Child]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Child_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT		[FK_Child_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_Child_Status]	FOREIGN KEY ([StatusId])	REFERENCES [dbo].[EntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Child_Variant_Instance  ON [dbo].[Child] ([VariantId] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_Child_Sku  ON [dbo].[Child] ([Sku] ASC);
GO