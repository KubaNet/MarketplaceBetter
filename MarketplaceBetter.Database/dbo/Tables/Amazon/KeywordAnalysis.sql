CREATE TABLE [dbo].[KeywordAnalysis]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)					NOT NULL,
	[Description]	NVARCHAR (255)					NULL,
	[InstanceId]	BIGINT							NOT NULL,
	CONSTRAINT		[PK_KeywordAnalysis]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_KeywordAnalysis_instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_KeywordAnalysis_Name  ON [dbo].[KeywordAnalysis] ([Name] ASC);
GO