CREATE TABLE [dbo].[KeywordData]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[SourceId]		BIGINT						NOT NULL,
	[MainPhraseId]	BIGINT						NOT NULL,
	CONSTRAINT		[PK_KeywordData]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_KeywordData_Source]		FOREIGN KEY ([SourceId])		REFERENCES [dbo].[KeywordDataSource] ([Id]),
	CONSTRAINT		[FK_KeywordData_MainPhrase]	FOREIGN KEY ([MainPhraseId])	REFERENCES [dbo].[MainPhrase] ([Id]),
);
GO