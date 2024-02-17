IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Title', 1, 80)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Title', [MaxByteCount] = 80 WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Description', 2, 2000)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Description', [MaxByteCount] = 2000 WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Bullet Point 1', 3, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 1', [MaxByteCount] = 200 WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Bullet Point 2', 4, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 2', [MaxByteCount] = 200 WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Bullet Point 3', 5, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 3', [MaxByteCount] = 200 WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Bullet Point 4', 6, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 4', [MaxByteCount] = 200 WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Bullet Point 5', 7, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Bullet Point 5', [MaxByteCount] = 200 WHERE [SystemName] = 7
GO