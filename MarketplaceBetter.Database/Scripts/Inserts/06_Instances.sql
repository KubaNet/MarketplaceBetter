IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon UK', 1, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon UK', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 1
GO