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

--Standard Image Header With Text

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 150, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 150 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (for text)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 150, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (for text)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 150 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 6000, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 6000 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 6)
GO

-- Standard Single Image & Highlights

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 400, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 400 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 400, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 400 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (bullet points)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (bullet points)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 4', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 4', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 5', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 5', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 6', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 6', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 7', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 7', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 8', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 18, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 8', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 7)
GO

-- Standard Single Image & Sidebar

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Caption (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Caption (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 500, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 500 WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 1 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 1 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 2 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 2 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 3 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 3 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 4 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 4 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 5 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 5 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 6 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 6 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 7 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 7 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 8 (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 8 (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 500, 18, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 500 WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 1 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 19, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 1 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 2 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 20, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 2 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 21 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 3 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 21, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 3 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 21 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 22 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 4 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 22, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 4 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 22 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 23 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 5 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 23, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 5 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 23 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 24 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 6 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 24, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 6 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 24 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 25 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 7 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 25, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 7 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 25 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 26 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 8 (sidebar)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 26, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 8 (sidebar)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 26 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 8)
GO