IF NOT EXISTS (SELECT NULL FROM [dbo].[SalesChannel] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[SalesChannel] ([Name], [SystemName])
    VALUES ('Amazon', 1)
ELSE UPDATE [dbo].[SalesChannel] SET [Name] = 'Amazon' WHERE [SystemName] = 1
GO