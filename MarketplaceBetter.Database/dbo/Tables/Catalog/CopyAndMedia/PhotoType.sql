CREATE TABLE [dbo].[PhotoType]
(
	[Id]				BIGINT					NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)			NOT NULL,
	[AmazonUploadCode]	NVARCHAR (255)			NOT NULL,
	[SystemName]		INT						NOT NULL,
	CONSTRAINT			[PK_PhotoType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_PhotoType_Name  ON [dbo].[PhotoType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_PhotoType_SystemName  ON [dbo].[PhotoType] ([SystemName] ASC);
GO