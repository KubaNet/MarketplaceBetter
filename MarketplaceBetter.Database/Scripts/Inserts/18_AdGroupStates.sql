IF NOT EXISTS (SELECT NULL FROM [dbo].[AdGroupState] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[AdGroupState] ([Name], [SystemName])
    VALUES ('Enabled', 1)
ELSE UPDATE [dbo].[AdGroupState] SET [Name] = 'Enabled' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AdGroupState] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[AdGroupState] ([Name], [SystemName])
    VALUES ('Paused', 2)
ELSE UPDATE [dbo].[AdGroupState] SET [Name] = 'Paused' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AdGroupState] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[AdGroupState] ([Name], [SystemName])
    VALUES ('Archived', 3)
ELSE UPDATE [dbo].[AdGroupState] SET [Name] = 'Archived' WHERE [SystemName] = 3
GO
