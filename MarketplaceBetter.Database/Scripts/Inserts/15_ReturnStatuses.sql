IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnStatus] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[ReturnStatus] ([Name], [SystemName])
    VALUES ('Unit returned to inventory', 1)
ELSE UPDATE [dbo].[ReturnStatus] SET [Name] = 'Unit returned to inventory' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnStatus] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[ReturnStatus] ([Name], [SystemName])
    VALUES ('Reimbursed', 2)
ELSE UPDATE [dbo].[ReturnStatus] SET [Name] = 'Reimbursed' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnStatus] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[ReturnStatus] ([Name], [SystemName])
    VALUES ('Pending repackaging', 3)
ELSE UPDATE [dbo].[ReturnStatus] SET [Name] = 'Pending repackaging' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[ReturnStatus] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[ReturnStatus] ([Name], [SystemName])
    VALUES ('Repackaged successfully', 4)
ELSE UPDATE [dbo].[ReturnStatus] SET [Name] = 'Repackaged successfully' WHERE [SystemName] = 4
GO