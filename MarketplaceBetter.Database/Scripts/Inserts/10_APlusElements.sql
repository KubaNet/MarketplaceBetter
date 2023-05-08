-- Standard Comparison Chart

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Title (product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Title (product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('ASIN (product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 10, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'ASIN (product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 10 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Highlight Column [yes/no] (product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Highlight Column [yes/no] (product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Title (product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Title (product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('ASIN (product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 10, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'ASIN (product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 10 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Highlight Column [yes/no] (product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Highlight Column [yes/no] (product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Title (product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Title (product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('ASIN (product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 10, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'ASIN (product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 10 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Highlight Column [yes/no] (product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Highlight Column [yes/no] (product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Title (product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 18, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Title (product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('ASIN (product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 10, 19, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'ASIN (product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 10 WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Highlight Column [yes/no] (product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 20, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Highlight Column [yes/no] (product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 20 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 21 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 21, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 21 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 22 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 22, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 22 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 23 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Title (product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 23, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Title (product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 23 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 24 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('ASIN (product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 10, 24, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'ASIN (product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 10 WHERE [Order] = 24 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 25 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Highlight Column [yes/no] (product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 25, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Highlight Column [yes/no] (product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 25 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 26 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image (product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 26, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image (product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 26 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 27 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 27, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 27 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 28 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Title (product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 28, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Title (product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 28 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 29 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('ASIN (product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 10, 29, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'ASIN (product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 10 WHERE [Order] = 29 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 30 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Highlight Column [yes/no] (product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 30, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Highlight Column [yes/no] (product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 30 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 31 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Metric 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 31, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Metric 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 31 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 32 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 1, product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 32, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 1, product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 32 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 33 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 1, product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 33, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 1, product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 33 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 34 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 1, product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 34, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 1, product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 34 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 35 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 1, product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 35, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 1, product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 35 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 36 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 1, product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 36, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 1, product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 36 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 37 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 1, product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 37, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 1, product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 37 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 38 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 1, product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 38, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 1, product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 38 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 39 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 1, product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 39, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 1, product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 39 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 40 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 1, product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 40, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 1, product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 40 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 41 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 1, product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 41, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 1, product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 41 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 42 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 1, product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 42, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 1, product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 42 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 43 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 1, product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 43, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 1, product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 43 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 44 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Metric 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 44, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Metric 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 44 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 45 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 2, product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 45, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 2, product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 45 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 46 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 2, product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 46, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 2, product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 46 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 47 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 2, product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 47, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 2, product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 47 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 48 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 2, product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 48, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 2, product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 48 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 49 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 2, product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 49, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 2, product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 49 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 50 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 2, product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 50, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 2, product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 50 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 51 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 2, product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 51, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 2, product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 51 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 52 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 2, product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 52, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 2, product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 52 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 53 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 2, product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 53, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 2, product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 53 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 54 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 2, product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 54, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 2, product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 54 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 55 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 2, product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 55, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 2, product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 55 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 56 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 2, product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 56, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 2, product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 56 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 57 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Metric 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 57, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Metric 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 57 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 58 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 3, product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 58, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 3, product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 58 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 59 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 3, product 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 59, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 3, product 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 59 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 60 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 3, product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 60, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 3, product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 60 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 61 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 3, product 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 61, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 3, product 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 61 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 62 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 3, product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 62, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 3, product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 62 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 63 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 3, product 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 263, 63, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 3, product 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 263 WHERE [Order] = 63 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 64 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 3, product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 64, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 3, product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 64 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 65 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 3, product 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 65, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 3, product 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 65 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 66 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 3, product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 66, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 3, product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 66 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 67 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 3, product 5)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 67, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 3, product 5)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 67 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 68 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Checked? [yes/no or empty if using "Text"] (metric 3, product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 68, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Checked? [yes/no or empty if using "Text"] (metric 3, product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 68 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 69 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Text [empty if using "Checked?"] (metric 3, product 6)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 250, 69, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Text [empty if using "Checked?"] (metric 3, product 6)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 250 WHERE [Order] = 69 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 1)
GO

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

-- Standard Single Image & Specs Detail

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main text)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main text)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline 1 (main text)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline 1 (main text)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text 1 (main text)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 400, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text 1 (main text)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 400 WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline 2 (main text)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline 2 (main text)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text 2 (main text)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 600, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text 2 (main text)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 600 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline for Bullet Points (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline for Bullet Points (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 1 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 1 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 2 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 2 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 3 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 3 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 4 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 4 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 5 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 5 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 6 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 6 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 7 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 7 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Bullet Point Text 8 (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 18, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Bullet Point Text 8 (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Sub-Headline for Text (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 19, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Sub-Headline for Text (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (specs)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 20, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (specs)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 9)
GO

-- Standard Single Left Image

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 10)
GO

-- Standard Single Right Image

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 11)
GO

-- Standard Text

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 12))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 12))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 12)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 12))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 5000, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 12))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 5000 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 12)
GO

-- Standard Three Images & Text

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (main)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (main)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 13)
GO

-- Standard Company Logo

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 14))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 14))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 14)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 14))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 14))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 14)
GO

-- Standard Multiple Image Module A

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Caption (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Caption (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 1)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 1)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Caption (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Caption (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 2)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 2)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Caption (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Caption (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 3)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 3)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image 4', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), null, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image 4', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 3), [CharactersLimit] = null WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Keywords (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 100, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Keywords (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 100 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Image Caption (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 200, 18, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Image Caption (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 200 WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 160, 19, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 160 WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text (image 4)', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 1000, 20, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text (image 4)', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 1000 WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 15)
GO

-- Standard Product Description Text

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 16))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Body Text', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), 6000, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 16))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Body Text', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 2), [CharactersLimit] = 6000 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 16)
GO

-- Standard Technical Specifications

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Headline', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 80, 1, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Headline', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 80 WHERE [Order] = 1 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 2, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 2 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 1', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 3, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 1', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 3 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 4, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 4 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 2', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 5, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 2', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 5 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 6, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 6 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 3', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 7, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 3', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 7 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 4', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 8, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 4', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 8 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 4', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 9, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 4', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 9 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 5', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 10, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 5', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 10 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 5', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 11, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 5', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 11 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 6', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 12, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 6', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 12 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 6', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 13, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 6', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 13 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 7', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 14, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 7', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 14 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 7', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 15, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 7', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 15 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 8', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 16, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 8', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 16 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 8', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 17, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 8', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 17 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 9', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 18, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 9', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 18 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 9', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 19, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 9', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 19 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 10', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 20, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 10', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 20 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 21 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 10', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 21, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 10', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 21 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 22 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 11', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 22, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 11', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 22 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 23 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 11', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 23, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 11', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 23 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 24 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 12', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 24, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 12', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 24 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 25 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 12', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 25, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 12', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 25 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 26 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 13', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 26, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 13', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 26 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 27 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 13', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 27, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 13', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 27 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 28 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 14', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 28, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 14', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 28 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 29 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 14', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 29, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 14', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 29 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 30 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 15', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 30, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 15', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 30 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 31 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 15', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 31, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 15', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 31 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 32 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Specification 16', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 30, 32, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Specification 16', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 30 WHERE [Order] = 32 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 33 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Definition 16', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 500, 33, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Definition 16', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 500 WHERE [Order] = 33 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElement] WHERE [Order] = 34 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
    INSERT INTO [dbo].[APlusElement] ([Name], [TypeId], [CharactersLimit], [Order], [SectionId])
    VALUES ('Number of columns [one/two]', (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), 3, 34, (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17))
ELSE UPDATE [dbo].[APlusElement] SET [Name] = 'Number of columns [one/two]', [TypeId] = (SELECT [Id] FROM [APlusElementType] WHERE [SystemName] = 1), [CharactersLimit] = 3 WHERE [Order] = 34 AND [SectionId] = (SELECT [Id] FROM [APlusSection] WHERE [SystemName] = 17)
GO