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