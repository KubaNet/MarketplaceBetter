IF NOT EXISTS (SELECT NULL FROM [dbo].[User] WHERE [Login] = 'Kuba')
    INSERT INTO [dbo].[User] ([Login], [Password], [CurrentInstanceId], [ShowDrafts], [ShowWithdrawn], [ExpandedMenu])
    SELECT 'Kuba', 'a', I.[Id], 0, 0, '1,2,4' FROM [Instance] I
    WHERE I.[SystemName] = 1
ELSE UPDATE [dbo].[User] SET [Password] = 'a', [CurrentInstanceId] = (SELECT [Id] FROM [dbo].[Instance] WHERE [SystemName] = 1), [ShowDrafts] = 0, [ShowWithdrawn] = 0, [ExpandedMenu] = '1,2,3' 
    WHERE [Login] = 'Kuba'
GO