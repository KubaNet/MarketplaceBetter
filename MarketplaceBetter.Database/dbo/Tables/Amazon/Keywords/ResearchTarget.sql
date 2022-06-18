CREATE TABLE [dbo].[ResearchTarget]
(
	[Id]						BIGINT							NOT NULL	IDENTITY,
	[Name]						NVARCHAR (255)					NOT NULL,
	[ResearchId]				BIGINT							NOT NULL,
	[Helium10Value]				INT								NOT NULL,
	[AmazonSearchTermsValue]	INT								NOT NULL,
	[StatusId]					BIGINT							NOT NULL,
	CONSTRAINT					[PK_ResearchTarget]				PRIMARY KEY ([Id]),
	CONSTRAINT					[FK_ResearchTarget_Research]	FOREIGN KEY ([ResearchId])	REFERENCES [dbo].[Research] ([Id]),
	CONSTRAINT					[FK_ResearchTarget_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[ResearchTargetStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ResearchTarget_Name_Research  ON [dbo].[ResearchTarget] ([Name] ASC, [ResearchId] ASC);
GO