CREATE TABLE [dbo].[Photo]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[VariantId]			BIGINT						NOT NULL,
	[InstanceId]		BIGINT						NOT NULL,
	[TypeId]			BIGINT						NOT NULL,
	[KindId]			BIGINT						NOT NULL,
	[CloudId]			NVARCHAR (255)				NOT NULL,
	[Version]			NVARCHAR (255)				NOT NULL,
	[Url]				NVARCHAR (255)				NOT NULL,
	[FileName]			NVARCHAR (255)				NOT NULL,
	[Height]			INT							NOT NULL,
	[Width]				INT							NOT NULL,
	[Uploaded]			DATETIME2					NOT NULL,
	CONSTRAINT			[PK_Photo]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Photo_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT			[FK_Photo_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_Photo_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[PhotoType] ([Id]),
	CONSTRAINT			[FK_Photo_Kind]		FOREIGN KEY ([KindId])		REFERENCES [dbo].[PhotoKind] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Photo_CloudId  ON [dbo].[Photo] ([Url] ASC);
GO

CREATE UNIQUE INDEX UIX_Photo_Url  ON [dbo].[Photo] ([Url] ASC);
GO

CREATE UNIQUE INDEX UIX_Photo_Variant_Instance_Type  ON [dbo].[Photo] ([VariantId] ASC, [InstanceId] ASC, [TypeId] ASC);
GO