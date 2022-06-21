IF NOT EXISTS (SELECT NULL FROM [dbo].[ResearchTargetStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[ResearchTargetStatus] ([Name], [SystemName])
    VALUES ('Included', 1)
ELSE UPDATE [dbo].[ResearchTargetStatus] SET [Name] = 'Included' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ResearchTargetStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[ResearchTargetStatus] ([Name], [SystemName])
    VALUES ('Excluded', 2)
ELSE UPDATE [dbo].[ResearchTargetStatus] SET [Name] = 'Excluded' WHERE [SystemName] = 2
GO