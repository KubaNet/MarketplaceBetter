CREATE TABLE [dbo].[AmazonTargeting]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Targeting]		NVARCHAR (255)					NOT NULL,
	[StatusId]		BIGINT							NOT NULL,
	[CampaignId]	BIGINT							NOT NULL,
	CONSTRAINT		[PK_AmazonTargeting]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonTargeting_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AmazonTargetingStatus] ([Id]),
	CONSTRAINT		[FK_AmazonTargeting_Campaign]	FOREIGN KEY ([CampaignId])	REFERENCES [dbo].[AmazonCampaign] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonTargeting_Targeting_Campaign  ON [dbo].[AmazonTargeting] ([Targeting] ASC, [CampaignId] ASC);
GO