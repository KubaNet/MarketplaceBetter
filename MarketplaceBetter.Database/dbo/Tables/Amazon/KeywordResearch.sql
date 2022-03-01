CREATE TABLE [dbo].[KeywordResearch]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	CONSTRAINT	[PK_KeywordResearch]			PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_KeywordResearch_Name  ON [dbo].[KeywordResearch] ([Name] ASC);
GO