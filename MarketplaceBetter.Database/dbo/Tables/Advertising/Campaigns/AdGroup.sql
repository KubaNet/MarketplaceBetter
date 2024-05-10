CREATE TABLE [dbo].[AdGroup]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[AmazonId]		NVARCHAR (255)			NULL,
	[StateId]		BIGINT					NOT NULL,
	[CampaignId]	BIGINT					NOT NULL,
	CONSTRAINT		[PK_AdGroup]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AdGroup_State]		FOREIGN KEY ([StateId])		REFERENCES [dbo].[AdEntityState] ([Id]),
	CONSTRAINT		[FK_AdGroup_Campaign]	FOREIGN KEY ([CampaignId])	REFERENCES [dbo].[Campaign] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AdGroup_AmazonId  ON [dbo].[AdGroup] ([AmazonId] ASC);
GO