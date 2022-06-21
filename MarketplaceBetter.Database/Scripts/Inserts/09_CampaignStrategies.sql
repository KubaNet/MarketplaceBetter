IF NOT EXISTS (SELECT NULL FROM [dbo].[CampaignStrategy] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[CampaignStrategy] ([Name], [SystemName], [CampaignTypeId])
    SELECT 'Auto', 1, CT.[Id] FROM [CampaignType] CT WHERE CT.[SystemName] = 1 
ELSE UPDATE [dbo].[CampaignStrategy] SET [Name] = 'Auto' WHERE [SystemName] = 1
GO