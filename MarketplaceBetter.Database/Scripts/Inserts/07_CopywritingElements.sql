IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Title', 1)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Title' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Description', 2)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Description' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Bullet Point 1', 3)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 1' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Bullet Point 2', 4)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 2' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Bullet Point 3', 5)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 3' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Bullet Point 4', 6)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 4' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName])
    VALUES ('Bullet Point 5', 7)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 5' WHERE [SystemName] = 7
GO