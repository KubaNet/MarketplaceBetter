IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('All', 1, 'All')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'All', [ShortName] = 'All' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('US', 2, 'US')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'US', [ShortName] = 'US' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('CA', 3, 'CA')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'CA', [ShortName] = 'CA' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('UK', 4, 'UK')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'UK', [ShortName] = 'UK' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('DE', 5, 'DE')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'DE', [ShortName] = 'DE' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('FR', 6, 'FR')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'FR', [ShortName] = 'FR' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('IT', 7, 'IT')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'IT', [ShortName] = 'IT' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('ES', 8, 'ES')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'ES', [ShortName] = 'ES' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('NL', 9, 'NL')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'NL', [ShortName] = 'NL' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName])
    VALUES ('SE', 10, 'SE')
ELSE UPDATE [dbo].[Instance] SET [Name] = 'SE', [ShortName] = 'SE' WHERE [SystemName] = 10
GO