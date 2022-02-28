IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon US', 1, 1, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon US', [Order] = 1, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon CA', 2, 2, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon CA', [Order] = 2, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon UK', 3, 3, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon UK', [Order] = 3, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon DE', 4, 4, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon DE', [Order] = 4, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon FR', 5, 5, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon FR', [Order] = 5, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon IT', 6, 6, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon IT', [Order] = 6, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon ES', 7, 7, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon ES', [Order] = 7, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon NL', 8, 8, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon NL', [Order] = 8, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [Order], [SalesChannelId])
    SELECT 'Amazon SE', 9, 9, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon SE', [Order] = 9, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 9
GO