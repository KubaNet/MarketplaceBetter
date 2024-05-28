INSERT INTO [dbo].[Copywriting]
        ([ProductId]
        ,[InstanceId]
        ,[ElementId]
        ,[Value]
        ,[ByteCount]
        ,[HasProperLength])
SELECT [ProductId], 3, [ElementId], [Value], [ByteCount], [HasProperLength] FROM [Copywriting] C
WHERE C.[InstanceId] = 2