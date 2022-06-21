CREATE TABLE [dbo].[CampaignStrategy]
(
	[Id]				BIGINT								NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)						NOT NULL,
	[SystemName]		INT									NOT NULL,
	[CampaignTypeId]	BIGINT								NOT NULL,
	CONSTRAINT			[PK_CampaignStrategy]				PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_CampaignStrategy_CampaignType]	FOREIGN KEY ([CampaignTypeId])		REFERENCES [dbo].[CampaignType] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_CampaignStrategy_CampaignType_Name  ON [dbo].[CampaignStrategy] ([CampaignTypeId] ASC, [Name] ASC);
GO

CREATE UNIQUE INDEX UIX_CampaignStrategy_SystemName  ON [dbo].[CampaignStrategy] ([SystemName] ASC);
GO