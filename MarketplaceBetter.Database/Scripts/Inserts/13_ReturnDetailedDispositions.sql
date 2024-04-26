IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnDetailedDisposition] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[ReturnDetailedDisposition] ([Name], [SystemName])
    VALUES ('SELLABLE', 1)
ELSE UPDATE [dbo].[ReturnDetailedDisposition] SET [Name] = 'SELLABLE' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnDetailedDisposition] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[ReturnDetailedDisposition] ([Name], [SystemName])
    VALUES ('DAMAGED', 2)
ELSE UPDATE [dbo].[ReturnDetailedDisposition] SET [Name] = 'DAMAGED' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnDetailedDisposition] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[ReturnDetailedDisposition] ([Name], [SystemName])
    VALUES ('CUSTOMER_DAMAGED', 3)
ELSE UPDATE [dbo].[ReturnDetailedDisposition] SET [Name] = 'CUSTOMER_DAMAGED' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnDetailedDisposition] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[ReturnDetailedDisposition] ([Name], [SystemName])
    VALUES ('DEFECTIVE', 4)
ELSE UPDATE [dbo].[ReturnDetailedDisposition] SET [Name] = 'DEFECTIVE' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnDetailedDisposition] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[ReturnDetailedDisposition] ([Name], [SystemName])
    VALUES ('CARRIER_DAMAGED', 5)
ELSE UPDATE [dbo].[ReturnDetailedDisposition] SET [Name] = 'CARRIER_DAMAGED' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnDetailedDisposition] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[ReturnDetailedDisposition] ([Name], [SystemName])
    VALUES ('EXPIRED', 6)
ELSE UPDATE [dbo].[ReturnDetailedDisposition] SET [Name] = 'EXPIRED' WHERE [SystemName] = 6
GO