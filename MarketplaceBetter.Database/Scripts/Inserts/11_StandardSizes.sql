IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('One Size', 1)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'One Size' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('XS', 2)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'XS' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('S', 3)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'S' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('M', 4)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'M' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[StandardSize] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[StandardSize] ([Name], [SystemName])
    VALUES ('L', 5)
ELSE UPDATE [dbo].[StandardSize] SET [Name] = 'L' WHERE [SystemName] = 5
GO