CREATE TABLE [dbo].[MainPhraseStatus]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_MainPhraseStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_MainPhraseStatus_Name  ON [dbo].[MainPhraseStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_MainPhraseStatus_SystemName  ON [dbo].[MainPhraseStatus] ([SystemName] ASC);
GO