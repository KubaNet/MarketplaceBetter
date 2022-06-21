IF NOT EXISTS (SELECT NULL FROM [dbo].[CampaignType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[CampaignType] ([Name], [SystemName])
    VALUES ('Sponsored Products', 1)
ELSE UPDATE [dbo].[CampaignType] SET [Name] = 'Sponsored Products' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CampaignType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[CampaignType] ([Name], [SystemName])
    VALUES ('Sponsored Brands', 2)
ELSE UPDATE [dbo].[CampaignType] SET [Name] = 'Sponsored Brands' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CampaignType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[CampaignType] ([Name], [SystemName])
    VALUES ('Sponsored Display', 3)
ELSE UPDATE [dbo].[CampaignType] SET [Name] = 'Sponsored Display' WHERE [SystemName] = 3
GO