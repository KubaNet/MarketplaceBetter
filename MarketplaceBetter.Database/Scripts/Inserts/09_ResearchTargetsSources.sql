IF NOT EXISTS (SELECT NULL FROM [dbo].[ResearchTargetSource] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[ResearchTargetSource] ([Name], [SystemName])
    VALUES ('Helium 10', 1)
ELSE UPDATE [dbo].[ResearchTargetSource] SET [Name] = 'Helium 10' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ResearchTargetSource] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[ResearchTargetSource] ([Name], [SystemName])
    VALUES ('Amazon Search Terms', 2)
ELSE UPDATE [dbo].[ResearchTargetSource] SET [Name] = 'Amazon Search Terms' WHERE [SystemName] = 2
GO