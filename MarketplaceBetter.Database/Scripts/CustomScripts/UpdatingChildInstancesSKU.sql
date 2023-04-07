UPDATE [dbo].[ChildInstance]
   SET [Sku] = LOWER(i.ShortName) + '_' + c.Sku
   FROM ChildInstance ci JOIN Variant c ON ci.VariantId = c.Id JOIN Instance i ON ci.InstanceId = i.Id
GO