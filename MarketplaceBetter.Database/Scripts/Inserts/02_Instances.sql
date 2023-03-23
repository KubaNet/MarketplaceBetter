IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('All', 1, 'All', 0)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'All', [ShortName] = 'All', [IsNormal] = 0 WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('US', 2, 'US', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'US', [ShortName] = 'US', [IsNormal] = 1 WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('CA', 3, 'CA', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'CA', [ShortName] = 'CA', [IsNormal] = 1 WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('UK', 4, 'UK', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'UK', [ShortName] = 'UK', [IsNormal] = 1 WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('DE', 5, 'DE', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'DE', [ShortName] = 'DE', [IsNormal] = 1 WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('FR', 6, 'FR', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'FR', [ShortName] = 'FR', [IsNormal] = 1 WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('IT', 7, 'IT', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'IT', [ShortName] = 'IT', [IsNormal] = 1 WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('ES', 8, 'ES', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'ES', [ShortName] = 'ES', [IsNormal] = 1 WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('NL', 9, 'NL', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'NL', [ShortName] = 'NL' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Instance] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[Instance] ([Name], [SystemName], [ShortName], [IsNormal])
    VALUES ('SE', 10, 'SE', 1)
ELSE UPDATE [dbo].[Instance] SET [Name] = 'SE', [ShortName] = 'SE' WHERE [SystemName] = 10
GO