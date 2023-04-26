IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElementType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[APlusElementType] ([Name], [SystemName])
    VALUES ('Single Line Text', 1)
ELSE UPDATE [dbo].[APlusElementType] SET [Name] = 'Single Line Text' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElementType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[APlusElementType] ([Name], [SystemName])
    VALUES ('Body Text', 2)
ELSE UPDATE [dbo].[APlusElementType] SET [Name] = 'Body Text' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusElementType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[APlusElementType] ([Name], [SystemName])
    VALUES ('Image', 3)
ELSE UPDATE [dbo].[APlusElementType] SET [Name] = 'Image' WHERE [SystemName] = 3
GO