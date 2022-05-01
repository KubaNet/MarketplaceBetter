CREATE TABLE [dbo].[ProductPhoto]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[VariantId]			BIGINT						NOT NULL,
	[ForAllInstances]	BIT							NOT NULL,
	[InstanceId]		BIGINT						NULL,
	[TypeId]			BIGINT						NOT NULL,
	[Url]				NVARCHAR (255)				NOT NULL,
	CONSTRAINT			[PK_ProductPhoto]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_ProductPhoto_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT			[FK_ProductPhoto_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_ProductPhoto_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[ProductPhotoType] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductPhoto_Url  ON [dbo].[ProductPhoto] ([Url] ASC);
GO

CREATE UNIQUE INDEX UIX_ProductPhoto_Variant_Instance_Type  ON [dbo].[ProductPhoto] ([VariantId] ASC, [InstanceId] ASC, [TypeId] ASC);
GO