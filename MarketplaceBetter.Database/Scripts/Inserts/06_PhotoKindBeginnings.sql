IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoKindBeginning] WHERE [Name] = 'G')
    INSERT INTO [dbo].[PhotoKindBeginning] ([Name])
    VALUES ('G')
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoKindBeginning] WHERE [Name] = 'M')
    INSERT INTO [dbo].[PhotoKindBeginning] ([Name])
    VALUES ('M')
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoKindBeginning] WHERE [Name] = 'P')
    INSERT INTO [dbo].[PhotoKindBeginning] ([Name])
    VALUES ('P')
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[PhotoKindBeginning] WHERE [Name] = 'S')
    INSERT INTO [dbo].[PhotoKindBeginning] ([Name])
    VALUES ('S')
GO