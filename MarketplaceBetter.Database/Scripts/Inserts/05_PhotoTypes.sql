IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Swatch', 1, 'SWCH')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Swatch', [AmazonUploadCode] = 'SWCH' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Main', 2, 'MAIN')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Main', [AmazonUploadCode] = 'MAIN' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Other 1', 3, 'PT01')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other 1', [AmazonUploadCode] = 'PT01' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Other 2', 4, 'PT02')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other 2', [AmazonUploadCode] = 'PT02' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Other 3', 5, 'PT03')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other 3', [AmazonUploadCode] = 'PT03' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Other 4', 6, 'PT04')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other 4', [AmazonUploadCode] = 'PT04' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Other 5', 7, 'PT05')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other 5', [AmazonUploadCode] = 'PT05' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoType] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[PhotoType] ([Name], [SystemName], [AmazonUploadCode])
    VALUES ('Other 6', 8, 'PT06')
ELSE UPDATE [dbo].[PhotoType] SET [Name] = 'Other 6', [AmazonUploadCode] = 'PT06' WHERE [SystemName] = 8
GO