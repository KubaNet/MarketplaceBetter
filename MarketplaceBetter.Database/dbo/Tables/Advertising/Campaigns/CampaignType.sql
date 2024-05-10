CREATE TABLE [dbo].[CampaignType]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_CampaignType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_CampaignType_Name  ON [dbo].[CampaignType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_CampaignType_SystemName  ON [dbo].[CampaignType] ([SystemName] ASC);
GO