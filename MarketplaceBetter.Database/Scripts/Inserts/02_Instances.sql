IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon US', 1, 'US')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon US', [ShortName] = 'US' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon CA', 2, 'CA')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon CA', [ShortName] = 'CA' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon UK', 3, 'UK')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon UK', [ShortName] = 'UK' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon DE', 4, 'DE')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon DE', [ShortName] = 'DE' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon FR', 5, 'FR')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon FR', [ShortName] = 'FR' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon IT', 6, 'IT')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon IT', [ShortName] = 'IT' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon ES', 7, 'ES')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon ES', [ShortName] = 'ES' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon NL', 8, 'NL')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon NL', [ShortName] = 'NL' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('Amazon SE', 9, 'SE')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon SE', [ShortName] = 'SE' WHERE [SystemName] = 9
GO