IF NOT EXISTS (SELECT NULL FROM [dbo].[MatchType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[MatchType] ([Name], [Value], [SystemName])
    VALUES ('Exact', 'exact', 1)
ELSE UPDATE [dbo].[MatchType] SET [Name] = 'Exact', [Value] = 'exact' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[MatchType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[MatchType] ([Name], [Value], [SystemName])
    VALUES ('Broad', 'broad', 2)
ELSE UPDATE [dbo].[MatchType] SET [Name] = 'Broad', [Value] = 'broad' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[MatchType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[MatchType] ([Name], [Value], [SystemName])
    VALUES ('Phrase', 'phrase', 3)
ELSE UPDATE [dbo].[MatchType] SET [Name] = 'Phrase', [Value] = 'phrase' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[MatchType] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[MatchType] ([Name], [Value], [SystemName])
    VALUES ('Negative Exact', 'negativeExact', 4)
ELSE UPDATE [dbo].[MatchType] SET [Name] = 'Negative Exact', [Value] = 'negativeExact' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[MatchType] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[MatchType] ([Name], [Value], [SystemName])
    VALUES ('Negative Phrase', 'negativePhrase', 5)
ELSE UPDATE [dbo].[MatchType] SET [Name] = 'Negative Phrase', [Value] = 'negativePhrase' WHERE [SystemName] = 5
GO