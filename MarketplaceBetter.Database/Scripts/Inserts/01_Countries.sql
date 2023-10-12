IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('United States', 1, 'US')
ELSE UPDATE [dbo].[Country] SET [Name] = 'United States', [Code] = 'US' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Canada', 2, 'CA')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Canada', [Code] = 'CA' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Great Britain', 3, 'GB')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Great Britain', [Code] = 'GB' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Germany', 4, 'DE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Germany', [Code] = 'DE' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('France', 5, 'FR')
ELSE UPDATE [dbo].[Country] SET [Name] = 'France', [Code] = 'FR' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Italy', 6, 'IT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Italy', [Code] = 'IT' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Spain', 7, 'ES')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Spain', [Code] = 'ES' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Netherlands', 8, 'NL')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Netherlands', [Code] = 'NL' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Sweden', 9, 'SE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Sweden', [Code] = 'SE' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Poland', 10, 'PL')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Poland', [Code] = 'PL' WHERE [SystemName] = 10
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 11)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Austria', 11, 'AT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Austria', [Code] = 'AT' WHERE [SystemName] = 11
GO