UPDATE [dbo].[ParentInstance]
   SET [Sku] = LOWER(i.ShortName) + '_' + p.Sku
   FROM ParentInstance pai JOIN Parent p ON pai.ParentId = p.Id JOIN Instance i ON pai.InstanceId = i.Id
GO