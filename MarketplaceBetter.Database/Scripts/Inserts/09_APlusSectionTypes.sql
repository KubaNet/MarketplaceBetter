IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSectionType] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[APlusSectionType] ([Name], [SystemName])
    VALUES ('Standard Comparison Chart', 1)
ELSE UPDATE [dbo].[APlusSectionType] SET [Name] = 'Standard Comparison Chart' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSectionType] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[APlusSectionType] ([Name], [SystemName])
    VALUES ('Standard Four Images & Text', 2)
ELSE UPDATE [dbo].[APlusSectionType] SET [Name] = 'Standard Four Images & Text' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSectionType] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[APlusSectionType] ([Name], [SystemName])
    VALUES ('Standard Four Images/Text Quadrant', 3)
ELSE UPDATE [dbo].[APlusSectionType] SET [Name] = 'Standard Four Images/Text Quadrant' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSectionType] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[APlusSectionType] ([Name], [SystemName])
    VALUES ('Standard Image & Dark Text Overlay', 4)
ELSE UPDATE [dbo].[APlusSectionType] SET [Name] = 'Standard Image & Dark Text Overlay' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSectionType] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[APlusSectionType] ([Name], [SystemName])
    VALUES ('Standard Image & Light Text Overlay', 5)
ELSE UPDATE [dbo].[APlusSectionType] SET [Name] = 'Standard Image & Light Text Overlay' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSectionType] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[APlusSectionType] ([Name], [SystemName])
    VALUES ('Standard Image Header With Text', 6)
ELSE UPDATE [dbo].[APlusSectionType] SET [Name] = 'Standard Image Header With Text' WHERE [SystemName] = 6
GO