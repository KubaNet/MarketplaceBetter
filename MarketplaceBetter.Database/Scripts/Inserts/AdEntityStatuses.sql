IF NOT EXISTS (SELECT NULL FROM [dbo].[AdEntityStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[AdEntityStatus] ([Name], [SystemName])
    VALUES ('Enabled', 1)
ELSE UPDATE [dbo].[AdEntityStatus] SET [Name] = 'Enabled' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AdEntityStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[AdEntityStatus] ([Name], [SystemName])
    VALUES ('Paused', 2)
ELSE UPDATE [dbo].[AdEntityStatus] SET [Name] = 'Paused' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AdEntityStatus] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[AdEntityStatus] ([Name], [SystemName])
    VALUES ('Archived', 3)
ELSE UPDATE [dbo].[AdEntityStatus] SET [Name] = 'Archived' WHERE [SystemName] = 3
GO