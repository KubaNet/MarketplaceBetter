IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Great Britain', 1, 'GB')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Great Britain', [Code] = 'GB' WHERE [SystemName] = 1
GO