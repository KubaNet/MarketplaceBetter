IF NOT EXISTS (SELECT NULL FROM [dbo].[User] WHERE [Login] = 'Kuba')
    INSERT INTO [dbo].[User] ([Login], [Password], [CurrentInstanceId], [ShowDrafts], [ShowWithdrawn], [ExpandedMenu])
    SELECT 'Kuba', 'r5#y7Qn', I.[Id], 0, 0, '1;2;3' FROM [Instance] I
    WHERE I.[SystemName] = 1
ELSE UPDATE [dbo].[User] SET [Password] = 'r5#y7Qn', [CurrentInstanceId] = (SELECT [Id] FROM [dbo].[Instance] WHERE [SystemName] = 1), [ShowDrafts] = 0, [ShowWithdrawn] = 0, [ExpandedMenu] = '1;2;3' 
    WHERE [Login] = 'Kuba'
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[User] WHERE [Login] = 'Alek')
    INSERT INTO [dbo].[User] ([Login], [Password], [CurrentInstanceId], [ShowDrafts], [ShowWithdrawn], [ExpandedMenu])
    SELECT 'Alek', '4E%9nWe', I.[Id], 0, 0, '1;2;3' FROM [Instance] I
    WHERE I.[SystemName] = 1
ELSE UPDATE [dbo].[User] SET [Password] = '4E%9nWe', [CurrentInstanceId] = (SELECT [Id] FROM [dbo].[Instance] WHERE [SystemName] = 1), [ShowDrafts] = 0, [ShowWithdrawn] = 0, [ExpandedMenu] = '1;2;3' 
    WHERE [Login] = 'Alek'
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[User] WHERE [Login] = 'Bartek')
    INSERT INTO [dbo].[User] ([Login], [Password], [CurrentInstanceId], [ShowDrafts], [ShowWithdrawn], [ExpandedMenu])
    SELECT 'Bartek', '9Kr&2dG', I.[Id], 0, 0, '1;2;3' FROM [Instance] I
    WHERE I.[SystemName] = 1
ELSE UPDATE [dbo].[User] SET [Password] = '9Kr&2dG', [CurrentInstanceId] = (SELECT [Id] FROM [dbo].[Instance] WHERE [SystemName] = 1), [ShowDrafts] = 0, [ShowWithdrawn] = 0, [ExpandedMenu] = '1;2;3' 
    WHERE [Login] = 'Bartek'
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[User] WHERE [Login] = 'Pawel')
    INSERT INTO [dbo].[User] ([Login], [Password], [CurrentInstanceId], [ShowDrafts], [ShowWithdrawn], [ExpandedMenu])
    SELECT 'Pawel', 'rH2@mC6', I.[Id], 0, 0, '1;2;3' FROM [Instance] I
    WHERE I.[SystemName] = 1
ELSE UPDATE [dbo].[User] SET [Password] = 'rH2@mC6', [CurrentInstanceId] = (SELECT [Id] FROM [dbo].[Instance] WHERE [SystemName] = 1), [ShowDrafts] = 0, [ShowWithdrawn] = 0, [ExpandedMenu] = '1;2;3' 
    WHERE [Login] = 'Pawel'
GO