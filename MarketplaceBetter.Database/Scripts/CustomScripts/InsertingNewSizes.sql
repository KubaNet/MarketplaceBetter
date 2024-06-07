INSERT INTO [dbo].[Variant]
           ([Sku]
           ,[ProductId]
           ,[ColorId]
           ,[SizeId]
           ,[Ean]
           ,[Asin]
           ,[StatusId]
           ,[Comment])
SELECT CONVERT(nvarchar(255), NEWID()), [ProductId], [ColorId], S.[Id] as SizeId, null, null, 1, null FROM [Variant] V, [Size] S
WHERE V.[Id] in (192,193,194,195,196,197,198,199,200,201,202,499,500,501,502,503,504) and S.[Id] in (2, 4, 5, 10012)