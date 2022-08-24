CREATE TABLE [dbo].[NegativeKeyword]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Keyword]		NVARCHAR(255)					NOT NULL,
	[MatchTypeId]	BIGINT							NOT NULL,
	[AdGroupId]		BIGINT							NOT NULL,
	[AmazonId]		NVARCHAR(255)					NULL,
	[StatusId]		BIGINT							NOT NULL,
	CONSTRAINT		[PK_NegativeKeyword]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_NegativeKeyword_MatchType]	FOREIGN KEY ([MatchTypeId])	REFERENCES [dbo].[MatchType] ([Id]),
	CONSTRAINT		[FK_NegativeKeyword_AdGroupId]	FOREIGN KEY ([AdGroupId])	REFERENCES [dbo].[AdGroup] ([Id]),
	CONSTRAINT		[FK_NegativeKeyword_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AdEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_NegativeKeyword_AmazonId  ON [dbo].[NegativeKeyword] ([AmazonId] ASC) WHERE [AmazonId] IS NOT NULL;
GO

CREATE UNIQUE INDEX UIX_NegativeKeyword_AdGroup_Keyword_MatchType  ON [dbo].[NegativeKeyword] ([AdGroupId] ASC, [Keyword] ASC, [MatchTypeId] ASC);
GO