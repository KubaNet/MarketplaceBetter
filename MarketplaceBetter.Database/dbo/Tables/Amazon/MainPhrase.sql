CREATE TABLE [dbo].[MainPhrase]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[AnalysisId]	BIGINT						NOT NULL,
	[Name]			NVARCHAR (255)				NOT NULL,
	[Description]	NVARCHAR (255)				NULL,
	[StatusId]		BIGINT						NOT NULL,
	CONSTRAINT		[PK_MainPhrase]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_MainPhrase_Analysis]	FOREIGN KEY ([AnalysisId])	REFERENCES [dbo].[KeywordAnalysis] ([Id]),
	CONSTRAINT		[FK_MainPhrase_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[MainPhraseStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_MainPhrase_KaywordAnalysis_Name  ON [dbo].[MainPhrase] ([AnalysisId] ASC, [Name] ASC);
GO