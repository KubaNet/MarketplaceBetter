IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName])
    VALUES ('Amazon UK', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'Amazon UK' WHERE [SystemName] = 1
GO