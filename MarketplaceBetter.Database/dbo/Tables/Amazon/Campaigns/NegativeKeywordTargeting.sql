CREATE TABLE [dbo].[NegativeKeywordTargeting]
(
	[Id]			BIGINT									NOT NULL	IDENTITY,
	[Keyword]		NVARCHAR(255)							NOT NULL,
	[MatchTypeId]	BIGINT									NOT NULL,
	[AdGroupId]		BIGINT									NOT NULL,
	[AmazonId]		NVARCHAR(255)							NULL,
	[StatusId]		BIGINT									NOT NULL,
	CONSTRAINT		[PK_NegativeKeywordTargeting]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_NegativeKeywordTargeting_MatchType]	FOREIGN KEY ([MatchTypeId])	REFERENCES [dbo].[MatchType] ([Id]),
	CONSTRAINT		[FK_NegativeKeywordTargeting_AdGroupId]	FOREIGN KEY ([AdGroupId])	REFERENCES [dbo].[AdGroup] ([Id]),
	CONSTRAINT		[FK_NegativeKeywordTargeting_Status]	FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AdEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_NegativeKeywordTargeting_AmazonId  ON [dbo].[NegativeKeywordTargeting] ([AmazonId] ASC) WHERE [AmazonId] IS NOT NULL;
GO