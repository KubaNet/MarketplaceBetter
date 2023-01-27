CREATE TABLE [dbo].[PhotoUpload]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[InstanceId]		BIGINT						NOT NULL,
	[TypeId]			BIGINT						NOT NULL,
	[CloudId]			NVARCHAR (255)				NOT NULL,
	[Url]				NVARCHAR (255)				NOT NULL,
	[Height]			INT							NOT NULL,
	[Width]				INT							NOT NULL,
	CONSTRAINT			[PK_PhotoUpload]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_PhotoUpload_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_PhotoUpload_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[PhotoType] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_PhotoUpload_CloudId  ON [dbo].[PhotoUpload] ([Url] ASC);
GO

CREATE UNIQUE INDEX UIX_PhotoUpload_Url  ON [dbo].[PhotoUpload] ([Url] ASC);
GO