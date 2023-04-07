UPDATE [dbo].[ParentInstance]
   SET [Sku] = LOWER(i.ShortName) + '_' + LOWER(b.Code) + '_' + p.Code
   FROM ParentInstance pai JOIN Product p ON pai.ProductId = p.Id JOIN Instance i ON pai.InstanceId = i.Id JOIN Brand b ON p.BrandId = b.Id
GO