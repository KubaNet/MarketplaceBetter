CREATE TABLE [dbo].[AmazonTargeting]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Value]			NVARCHAR (255)					NOT NULL,
	[CampaignId]	BIGINT							NOT NULL,
	[TypeId]		BIGINT							NOT NULL,
	[StatusId]		BIGINT							NOT NULL,
	CONSTRAINT		[PK_AmazonTargeting]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonTargeting_Campaign]	FOREIGN KEY ([CampaignId])	REFERENCES [dbo].[AmazonCampaign] ([Id]),
	CONSTRAINT		[FK_AmazonTargeting_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[AmazonTargetingType] ([Id]),
	CONSTRAINT		[FK_AmazonTargeting_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AmazonTargetingStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonTargeting_Value_Campaign  ON [dbo].[AmazonTargeting] ([Value] ASC, [CampaignId] ASC);
GO