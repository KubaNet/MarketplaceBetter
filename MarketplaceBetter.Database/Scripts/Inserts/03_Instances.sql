IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon US', 1, 'US', 1, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon US', [ShortName] = 'US', [Order] = 1, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon CA', 2, 'CA', 2, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon CA', [ShortName] = 'CA', [Order] = 2, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon UK', 3, 'UK', 3, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon UK', [ShortName] = 'UK', [Order] = 3, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon DE', 4, 'DE', 4, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon DE', [ShortName] = 'DE', [Order] = 4, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon FR', 5, 'FR', 5, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon FR', [ShortName] = 'FR', [Order] = 5, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon IT', 6, 'IT', 6, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon IT', [ShortName] = 'IT', [Order] = 6, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon ES', 7, 'ES', 7, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon ES', [ShortName] = 'ES', [Order] = 7, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon NL', 8, 'NL', 8, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon NL', [ShortName] = 'NL', [Order] = 8, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [Order], [SalesChannelId])
    SELECT 'Amazon SE', 9, 'SE', 9, [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon SE', [ShortName] = 'SE', [Order] = 9, [SalesChannelId] = (SELECT [dbo].[SalesChannel].[Id] FROM [dbo].[SalesChannel] WHERE [dbo].[SalesChannel].[SystemName] = 1) WHERE [SystemName] = 9
GO