INSERT INTO [dbo].[ColorTranslation] ([InstanceId],[ColorId],[Translation]) SELECT 'InstanceId', [Id], 'Translation' FROM [Color] WHERE [Code] = 'ColorCode' and [GroupId] = 'GroupId' GO
