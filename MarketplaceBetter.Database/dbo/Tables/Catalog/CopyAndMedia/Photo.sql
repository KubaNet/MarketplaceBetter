CREATE TABLE [dbo].[Photo]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[VariantId]			BIGINT						NOT NULL,
	[ForAllInstances]	BIT							NOT NULL,
	[InstanceId]		BIGINT						NULL,
	[TypeId]			BIGINT						NOT NULL,
	[CloudId]			NVARCHAR (255)				NOT NULL,
	[Url]				NVARCHAR (255)				NOT NULL,
	CONSTRAINT			[PK_Photo]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Photo_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT			[FK_Photo_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_Photo_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[PhotoType] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Photo_CloudId  ON [dbo].[Photo] ([Url] ASC);
GO

CREATE UNIQUE INDEX UIX_Photo_Url  ON [dbo].[Photo] ([Url] ASC);
GO

CREATE UNIQUE INDEX UIX_Photo_Variant_Instance_Type  ON [dbo].[Photo] ([VariantId] ASC, [InstanceId] ASC, [TypeId] ASC);
GO