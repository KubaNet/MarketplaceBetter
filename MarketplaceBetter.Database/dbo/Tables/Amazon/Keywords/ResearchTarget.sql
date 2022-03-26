CREATE TABLE [dbo].[ResearchTarget]
(
	[Id]			BIGINT								NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)						NOT NULL,
	[ResearchId]	BIGINT								NOT NULL,
	CONSTRAINT		[PK_ResearchTarget]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ResearchTarget_Research]	FOREIGN KEY ([ResearchId])	REFERENCES [dbo].[Research] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ResearchTarget_Name  ON [dbo].[ResearchTarget] ([Name] ASC);
GO