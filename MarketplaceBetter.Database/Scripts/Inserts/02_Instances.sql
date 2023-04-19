IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('All', 1, 0)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'All', [IsNormal] = 0 WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('US', 2, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'US', [IsNormal] = 1 WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('CA', 3, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'CA', [IsNormal] = 1 WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('UK', 4, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'UK', [IsNormal] = 1 WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('DE', 5, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'DE', [IsNormal] = 1 WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('FR', 6, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'FR', [IsNormal] = 1 WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('IT', 7, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'IT', [IsNormal] = 1 WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('ES', 8, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'ES', [IsNormal] = 1 WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('NL', 9, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'NL', [IsNormal] = 1 WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [IsNormal])
    VALUES ('SE', 10, 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'SE', [IsNormal] = 1 WHERE [SystemName] = 10
GO