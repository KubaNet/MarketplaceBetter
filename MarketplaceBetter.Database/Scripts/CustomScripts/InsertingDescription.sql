INSERT INTO [dbo].[Copywriting]
        ([ProductId]
        ,[InstanceId]
        ,[ElementId]
        ,[Value]
        ,[ByteCount]
        ,[HasProperLength])
SELECT [ProductId], 3, 2, [Value], [ByteCount], [HasProperLength] FROM [Copywriting] C
WHERE C.[ElementId] = 2 and C.[InstanceId] = 2