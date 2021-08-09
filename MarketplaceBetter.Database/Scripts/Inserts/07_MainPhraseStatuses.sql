IF NOT EXISTS (SELECT NULL FROM [dbo].[MainPhraseStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[MainPhraseStatus] ([Name], [SystemName])
    VALUES ('Very Good', 1)
ELSE UPDATE [dbo].[MainPhraseStatus] SET [Name] = 'Very Good' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[MainPhraseStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[MainPhraseStatus] ([Name], [SystemName])
    VALUES ('Good Enough', 2)
ELSE UPDATE [dbo].[MainPhraseStatus] SET [Name] = 'Good Enough' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[MainPhraseStatus] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[MainPhraseStatus] ([Name], [SystemName])
    VALUES ('Not Good', 3)
ELSE UPDATE [dbo].[MainPhraseStatus] SET [Name] = 'Not Good' WHERE [SystemName] = 3
GO