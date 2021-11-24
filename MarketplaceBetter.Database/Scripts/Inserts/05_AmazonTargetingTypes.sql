IF NOT EXISTS (SELECT NULL FROM [dbo].[AmazonTargetingType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[AmazonTargetingType] ([Name], [SystemName])
    VALUES ('Unknown', 1)
ELSE UPDATE [dbo].[AmazonTargetingType] SET [Name] = 'Unknown' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AmazonTargetingType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[AmazonTargetingType] ([Name], [SystemName])
    VALUES ('Keyword', 2)
ELSE UPDATE [dbo].[AmazonTargetingType] SET [Name] = 'Keyword' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AmazonTargetingType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[AmazonTargetingType] ([Name], [SystemName])
    VALUES ('Product', 3)
ELSE UPDATE [dbo].[AmazonTargetingType] SET [Name] = 'Product' WHERE [SystemName] = 3
GO