IF NOT EXISTS (SELECT NULL FROM [dbo].[VariantStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[VariantStatus] ([Name], [SystemName])
    VALUES ('Aktualny', 1)
ELSE UPDATE [dbo].[VariantStatus] SET [Name] = 'Aktualny' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[VariantStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[VariantStatus] ([Name], [SystemName])
    VALUES ('Wycofany', 2)
ELSE UPDATE [dbo].[VariantStatus] SET [Name] = 'Wycofany' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[VariantStatus] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[VariantStatus] ([Name], [SystemName])
    VALUES ('Do wycofania', 3)
ELSE UPDATE [dbo].[VariantStatus] SET [Name] = 'Do wycofania' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[VariantStatus] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[VariantStatus] ([Name], [SystemName])
    VALUES ('Do wprowadzenia', 4)
ELSE UPDATE [dbo].[VariantStatus] SET [Name] = 'Do wprowadzenia' WHERE [SystemName] = 4
GO