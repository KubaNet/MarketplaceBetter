IF NOT EXISTS (SELECT NULL FROM [dbo].[AmazonTargetingStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[AmazonTargetingStatus] ([Name], [SystemName])
    VALUES ('Active', 1)
ELSE UPDATE [dbo].[AmazonTargetingStatus] SET [Name] = 'Active' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AmazonTargetingStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[AmazonTargetingStatus] ([Name], [SystemName])
    VALUES ('Negative', 2)
ELSE UPDATE [dbo].[AmazonTargetingStatus] SET [Name] = 'Negative' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[AmazonTargetingStatus] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[AmazonTargetingStatus] ([Name], [SystemName])
    VALUES ('New', 3)
ELSE UPDATE [dbo].[AmazonTargetingStatus] SET [Name] = 'New' WHERE [SystemName] = 3
GO