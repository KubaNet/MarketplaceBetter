IF NOT EXISTS (SELECT NULL FROM [dbo].[AdEntityState] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[AdEntityState] ([Name], [SystemName])
    VALUES ('Enabled', 1)
ELSE UPDATE [dbo].[AdEntityState] SET [Name] = 'Enabled' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AdEntityState] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[AdEntityState] ([Name], [SystemName])
    VALUES ('Paused', 2)
ELSE UPDATE [dbo].[AdEntityState] SET [Name] = 'Paused' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AdEntityState] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[AdEntityState] ([Name], [SystemName])
    VALUES ('Archived', 3)
ELSE UPDATE [dbo].[AdEntityState] SET [Name] = 'Archived' WHERE [SystemName] = 3
GO
