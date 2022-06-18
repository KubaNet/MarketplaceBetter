CREATE TABLE [dbo].[ResearchResult]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Phrase]		NVARCHAR (255)					NOT NULL,
	[Score]			INT								NOT NULL,
	[WordsCount]	INT								NOT NULL,
	[Frequency]		INT								NOT NULL,
	[IsOriginal]	BIT								NOT NULL,
	[TargetId]		BIGINT							NULL,
	[ResearchId]	BIGINT							NOT NULL,
	CONSTRAINT		[PK_ResearchResult]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ResearchResult_Target]		FOREIGN KEY ([TargetId])	REFERENCES [dbo].[ResearchTarget] ([Id]),
	CONSTRAINT		[FK_ResearchResult_Research]	FOREIGN KEY ([ResearchId])	REFERENCES [dbo].[Research] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ResearchResult_Phrase_Research  ON [dbo].[ResearchResult] ([Phrase] ASC, [ResearchId] ASC);
GO