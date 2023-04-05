CREATE TABLE [dbo].[AmazonEntityStatus]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_AmazonEntityStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonEntityStatus_Name  ON [dbo].[AmazonEntityStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonEntityStatus_SystemName  ON [dbo].[AmazonEntityStatus] ([SystemName] ASC);
GO