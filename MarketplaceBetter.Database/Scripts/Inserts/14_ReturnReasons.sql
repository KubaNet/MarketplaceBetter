IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('OTHER', 1)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'OTHER' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('ORDERED_WRONG_ITEM', 2)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'ORDERED_WRONG_ITEM' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('FOUND_BETTER_PRICE', 3)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'FOUND_BETTER_PRICE' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('NO_REASON_GIVEN', 4)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'NO_REASON_GIVEN' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('QUALITY_UNACCEPTABLE', 5)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'QUALITY_UNACCEPTABLE' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('NOT_COMPATIBLE', 6)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'NOT_COMPATIBLE' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('DAMAGED_BY_FC', 7)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'DAMAGED_BY_FC' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('MISSED_ESTIMATED_DELIVERY', 8)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'MISSED_ESTIMATED_DELIVERY' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('MISSING_PARTS', 9)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'MISSING_PARTS' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('DAMAGED_BY_CARRIER', 10)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'DAMAGED_BY_CARRIER' WHERE [SystemName] = 10
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 11)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('SWITCHEROO', 11)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'SWITCHEROO' WHERE [SystemName] = 11
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 12)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('DEFECTIVE', 12)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'DEFECTIVE' WHERE [SystemName] = 12
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 13)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('EXTRA_ITEM', 13)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'EXTRA_ITEM' WHERE [SystemName] = 13
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 14)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNWANTED_ITEM', 14)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNWANTED_ITEM' WHERE [SystemName] = 14
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 15)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('WARRANTY', 15)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'WARRANTY' WHERE [SystemName] = 15
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 16)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNAUTHORISED_PURCHASE', 16)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNAUTHORISED_PURCHASE' WHERE [SystemName] = 16
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 17)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNDELIVERABLE_INSUFFICIENT_ADDRESS', 17)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNDELIVERABLE_INSUFFICIENT_ADDRESS' WHERE [SystemName] = 17
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 18)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNDELIVERABLE_FAILED_DELIVERY_ATTEMPTS', 18)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNDELIVERABLE_FAILED_DELIVERY_ATTEMPTS' WHERE [SystemName] = 18
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 19)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNDELIVERABLE_REFUSED', 19)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNDELIVERABLE_REFUSED' WHERE [SystemName] = 19
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 20)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNDELIVERABLE_UNKNOWN', 20)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNDELIVERABLE_UNKNOWN' WHERE [SystemName] = 20
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 21)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('UNDELIVERABLE_UNCLAIMED', 21)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'UNDELIVERABLE_UNCLAIMED' WHERE [SystemName] = 21
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 22)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('CLOTHING_TOO_SMALL', 22)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'CLOTHING_TOO_SMALL' WHERE [SystemName] = 22
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 23)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('CLOTHING_TOO_LARGE', 23)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'CLOTHING_TOO_LARGE' WHERE [SystemName] = 23
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 24)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('CLOTHING_STYLE', 24)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'CLOTHING_STYLE' WHERE [SystemName] = 24
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 25)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('MISORDERED', 25)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'MISORDERED' WHERE [SystemName] = 25
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 26)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('NOT_AS_DESCRIBED', 26)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'NOT_AS_DESCRIBED' WHERE [SystemName] = 26
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 27)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_TOO_SMALL', 27)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_TOO_SMALL' WHERE [SystemName] = 27
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 28)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_TOO_LARGE', 28)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_TOO_LARGE' WHERE [SystemName] = 28
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 29)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_BATTERY', 29)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_BATTERY' WHERE [SystemName] = 29
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 30)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_NO_DOCS', 30)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_NO_DOCS' WHERE [SystemName] = 30
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 31)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_BAD_CLASP', 31)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_BAD_CLASP' WHERE [SystemName] = 31
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 32)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_LOOSE_STONE', 32)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_LOOSE_STONE' WHERE [SystemName] = 32
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 33)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('JEWELLERY_NO_CERT', 33)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'JEWELLERY_NO_CERT' WHERE [SystemName] = 33
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 34)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('APPAREL_TOO_SMALL', 34)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'APPAREL_TOO_SMALL' WHERE [SystemName] = 34
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 35)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('APPAREL_TOO_LARGE', 35)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'APPAREL_TOO_LARGE' WHERE [SystemName] = 35
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnReason] WHERE [SystemName] = 36)
    INSERT INTO [dbo].[ReturnReason] ([Name], [SystemName])
    VALUES ('APPAREL_STYLE', 36)
ELSE UPDATE [dbo].[ReturnReason] SET [Name] = 'APPAREL_STYLE' WHERE [SystemName] = 36
GO