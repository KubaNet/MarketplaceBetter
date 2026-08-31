IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Title', 1, 75)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Title', [MaxByteCount] = 75 WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Description', 2, 2000)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Description', [MaxByteCount] = 2000 WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('BP1', 3, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'BP1', [MaxByteCount] = 200 WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('BP2', 4, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'BP2', [MaxByteCount] = 200 WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('BP3', 5, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'BP3', [MaxByteCount] = 200 WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('BP4', 6, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'BP4', [MaxByteCount] = 200 WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('BP5', 7, 200)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'BP5', [MaxByteCount] = 200 WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Generic Keywords', 8, 500)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Generic Keywords', [MaxByteCount] = 500 WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[CopywritingElement] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[CopywritingElement] ([Name], [SystemName], [MaxByteCount])
    VALUES ('Highlights', 9, 125)
ELSE UPDATE [dbo].[CopywritingElement] SET [Name] = 'Highlights', [MaxByteCount] = 125 WHERE [SystemName] = 9
GO