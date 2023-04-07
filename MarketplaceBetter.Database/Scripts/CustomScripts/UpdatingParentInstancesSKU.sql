UPDATE [dbo].[ParentInstance]
   SET [Sku] = LOWER(i.ShortName) + '_' + p.Sku
   FROM ParentInstance pai JOIN Product p ON pai.ProductId = p.Id JOIN Instance i ON pai.InstanceId = i.Id
GO