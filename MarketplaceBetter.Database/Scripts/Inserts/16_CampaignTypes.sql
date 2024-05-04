IF NOT EXISTS (SELECT NULL FROM [dbo].[CampaignType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[CampaignType] ([Name], [SystemName])
    VALUES ('Auto', 1)
ELSE UPDATE [dbo].[CampaignType] SET [Name] = 'Auto' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CampaignType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[CampaignType] ([Name], [SystemName])
    VALUES ('Main', 2)
ELSE UPDATE [dbo].[CampaignType] SET [Name] = 'Main' WHERE [SystemName] = 2
GO
