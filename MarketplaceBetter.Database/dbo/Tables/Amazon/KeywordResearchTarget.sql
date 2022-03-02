CREATE TABLE [dbo].[KeywordResearchTarget]
(
	[Id]			BIGINT								NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)						NOT NULL,
	[ResearchId]	BIGINT								NOT NULL,
	CONSTRAINT		[PK_KeywordResearchTarget]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_KeywordResearchTarget_Research]	FOREIGN KEY ([ResearchId])	REFERENCES [dbo].[KeywordResearch] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_KeywordResearchTarget_Name  ON [dbo].[KeywordResearchTarget] ([Name] ASC);
GO