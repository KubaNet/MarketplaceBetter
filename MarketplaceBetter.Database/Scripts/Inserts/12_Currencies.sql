IF NOT EXISTS (SELECT NULL FROM [dbo].[Currency] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Currency] ([Name], [SystemName])
    VALUES ('PLN', 1)
ELSE UPDATE [dbo].[Currency] SET [Name] = 'PLN' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Currency] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Currency] ([Name], [SystemName])
    VALUES ('EUR', 2)
ELSE UPDATE [dbo].[Currency] SET [Name] = 'EUR' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Currency] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Currency] ([Name], [SystemName])
    VALUES ('GBP', 3)
ELSE UPDATE [dbo].[Currency] SET [Name] = 'GBP' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Currency] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Currency] ([Name], [SystemName])
    VALUES ('SEK', 4)
ELSE UPDATE [dbo].[Currency] SET [Name] = 'SEK' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Currency] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Currency] ([Name], [SystemName])
    VALUES ('USD', 5)
ELSE UPDATE [dbo].[Currency] SET [Name] = 'USD' WHERE [SystemName] = 5
GO