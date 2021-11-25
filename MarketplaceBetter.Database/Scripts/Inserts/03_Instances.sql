IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon US', 1, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon US', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon CA', 2, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon CA', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon UK', 3, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon UK', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon DE', 4, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon DE', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon FR', 5, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon FR', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon IT', 6, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon IT', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon ES', 7, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon ES', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon NL', 8, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon NL', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [SalesChannelId])
    SELECT 'Amazon SE', 9, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon SE', [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 9
GO