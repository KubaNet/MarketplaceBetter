IF NOT EXISTS (SELECT NULL FROM [dbo].[EntityStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[EntityStatus] ([Name], [SystemName])
    VALUES ('Szkic', 1)
ELSE UPDATE [dbo].[EntityStatus] SET [Name] = 'Szkic' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[EntityStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[EntityStatus] ([Name], [SystemName])
    VALUES ('Do wprowadzenia', 2)
ELSE UPDATE [dbo].[EntityStatus] SET [Name] = 'Do wprowadzenia' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[EntityStatus] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[EntityStatus] ([Name], [SystemName])
    VALUES ('Aktywny', 3)
ELSE UPDATE [dbo].[EntityStatus] SET [Name] = 'Aktywny' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[EntityStatus] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[EntityStatus] ([Name], [SystemName])
    VALUES ('Do wycofania', 4)
ELSE UPDATE [dbo].[EntityStatus] SET [Name] = 'Do wycofania' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[EntityStatus] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[EntityStatus] ([Name], [SystemName])
    VALUES ('Wycofany', 5)
ELSE UPDATE [dbo].[EntityStatus] SET [Name] = 'Wycofany' WHERE [SystemName] = 5
GO