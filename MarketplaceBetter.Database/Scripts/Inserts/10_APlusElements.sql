-- Standard Four Images & Text

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 4', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 4', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 2)
GO

-- Standard Four Images/Text Quadrant

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 4', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 4', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 3)
GO


-- Standard Image & Dark Text Overlay
IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 70, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 70 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 300, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 300 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Background Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Background Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Background Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Background Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 4)
GO

-- Standard Image & Light Text Overlay
IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 70, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 70 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 300, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 300 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Background Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Background Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Background Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Background Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 5)
GO