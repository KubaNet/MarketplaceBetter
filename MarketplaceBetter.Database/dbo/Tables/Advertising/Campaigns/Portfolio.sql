CREATE TABLE [dbo].[Portfolio]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)				NOT NULL,
	[AmazonId]			NVARCHAR (255)				NULL,
	[CampaignTypeId]	BIGINT						NOT NULL,
	[InstanceId]		BIGINT						NOT NULL,
	CONSTRAINT			[PK_Portfolio]				PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Portfolio_CampaignType]	FOREIGN KEY ([CampaignTypeId])	REFERENCES [dbo].[CampaignType] ([Id]),
	CONSTRAINT			[FK_Portfolio_Instance]		FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Portfolio_Instance_CampaignType  ON [dbo].[Portfolio] ([InstanceId] ASC, [CampaignTypeId] ASC);
GO

CREATE UNIQUE INDEX UIX_Portfolio_Name  ON [dbo].[Portfolio] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Portfolio_AmazonId  ON [dbo].[Portfolio] ([AmazonId] ASC) WHERE [AmazonId] IS NOT NULL;
GO