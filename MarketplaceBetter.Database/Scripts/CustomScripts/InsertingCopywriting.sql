INSERT INTO [dbo].[Copywriting]
           ([ProductId]
           ,[InstanceId]
           ,[ElementId]
           ,[Value]
           ,[ByteCount]
           ,[HasProperLength])
     SELECT
			P.Id
           ,I.Id
           ,CE.Id
           ,'GIVE A ELEGANT TOUCH to sleeves and a second life to your coat or jacket.'
           ,DATALENGTH('GIVE A ELEGANT TOUCH to sleeves and a second life to your coat or jacket.')
           ,case when (DATALENGTH('GIVE A ELEGANT TOUCH to sleeves and a second life to your coat or jacket.') < 200) then 1 else 0 end
	FROM Product P, Instance I, CopywritingElement CE
	WHERE P.Code in ('MC','MS') and I.Name in ('CA','UK','US') and CE.Name = 'BP1'
GO