CREATE TABLE [dbo].[PhraseData]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Phrase]		NVARCHAR (255)				NOT NULL,
	[Value]			FLOAT						NOT NULL,
	[IsIncluded]	BIT							NOT NULL,
	[KeywordDataId]	BIGINT						NOT NULL,
	CONSTRAINT		[PK_PhraseData]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_PhraseData_KeywordData]	FOREIGN KEY ([KeywordDataId])	REFERENCES [dbo].[KeywordData] ([Id]),
);
GO