CREATE TABLE [dbo].[ProductPhotoType]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_ProductPhotoType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductPhotoType_Name  ON [dbo].[ProductPhotoType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_ProductPhotoType_SystemName  ON [dbo].[ProductPhotoType] ([SystemName] ASC);
GO