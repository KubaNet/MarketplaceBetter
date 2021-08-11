IF NOT EXISTS (SELECT NULL FROM [dbo].[KeywordDataSource] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[KeywordDataSource] ([Name], [SystemName])
    VALUES ('Amazon Search Terms', 1)
ELSE UPDATE [dbo].[KeywordDataSource] SET [Name] = 'Amazon Search Terms' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[KeywordDataSource] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[KeywordDataSource] ([Name], [SystemName])
    VALUES ('Helium 10', 2)
ELSE UPDATE [dbo].[KeywordDataSource] SET [Name] = 'Helium 10' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[KeywordDataSource] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[KeywordDataSource] ([Name], [SystemName])
    VALUES ('Advertising Reports', 3)
ELSE UPDATE [dbo].[KeywordDataSource] SET [Name] = 'Advertising Reports' WHERE [SystemName] = 3
GO