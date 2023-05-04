IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Comparison Chart', 1)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Comparison Chart' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Four Images & Text', 2)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Four Images & Text' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Four Images/Text Quadrant', 3)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Four Images/Text Quadrant' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Image & Dark Text Overlay', 4)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Image & Dark Text Overlay' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Image & Light Text Overlay', 5)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Image & Light Text Overlay' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Image Header With Text', 6)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Image Header With Text' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Single Image & Highlights', 7)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Single Image & Highlights' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Single Image & Sidebar', 8)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Single Image & Sidebar' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Single Image & Specs Detail', 9)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Single Image & Specs Detail' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Single Left Image', 10)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Single Left Image' WHERE [SystemName] = 10
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 11)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Single Right Image', 11)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Single Right Image' WHERE [SystemName] = 11
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 12)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Text', 12)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Text' WHERE [SystemName] = 12
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 13)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Three Images & Text', 13)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Three Images & Text' WHERE [SystemName] = 13
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 14)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Company Logo', 14)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Company Logo' WHERE [SystemName] = 14
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 15)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Multiple Image Module A', 15)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Multiple Image Module A' WHERE [SystemName] = 15
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 16)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Product Description Text', 16)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Product Description Text' WHERE [SystemName] = 16
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[APlusSection] WHERE [SystemName] = 17)
    INSERT INTO [dbo].[APlusSection] ([Name], [SystemName])
    VALUES ('Standard Technical Specifications', 17)
ELSE UPDATE [dbo].[APlusSection] SET [Name] = 'Standard Technical Specifications' WHERE [SystemName] = 17
GO