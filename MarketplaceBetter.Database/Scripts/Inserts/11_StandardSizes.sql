IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('One Size + M', 1)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'One Size + M' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('One Size', 2)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'One Size' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('XS', 3)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'XS' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('S', 4)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'S' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('M', 5)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'M' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('L', 6)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'L' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('XL', 7)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'XL' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('XXL', 8)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'XXL' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('Not One Size', 9)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'Not One Size' WHERE [SystemName] = 9
GO