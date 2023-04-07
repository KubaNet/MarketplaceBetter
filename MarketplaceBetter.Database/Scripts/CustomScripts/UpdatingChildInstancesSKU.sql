UPDATE [dbo].[ChildInstance]
   SET [Sku] = LOWER(i.ShortName) + '_' + v.Sku
   FROM ChildInstance ci JOIN Variant v ON ci.VariantId = v.Id JOIN Instance i ON ci.InstanceId = i.Id
GO