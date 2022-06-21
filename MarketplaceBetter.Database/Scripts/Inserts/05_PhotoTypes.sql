IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Main Image', 1)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Main Image' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 1', 2)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 1' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 2', 3)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 2' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 3', 4)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 3' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 4', 5)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 4' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 5', 6)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 5' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 6', 7)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 6' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 7', 8)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 7' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Other Image 8', 9)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other Image 8' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName])
    VALUES ('Swatch Image', 10)
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Swatch Image' WHERE [SystemName] = 10
GO