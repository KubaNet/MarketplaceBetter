CREATE TABLE [dbo].[KeywordDataSource]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_KeywordDataSource]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_KeywordDataSource_Name  ON [dbo].[KeywordDataSource] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_KeywordDataSource_SystemName  ON [dbo].[KeywordDataSource] ([SystemName] ASC);
GO