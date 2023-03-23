UPDATE [dbo].[ChildInstance]
   SET [Sku] = LOWER(i.ShortName) + '_' + c.Sku
   FROM ChildInstance ci JOIN Child c ON ci.ChildId = c.Id JOIN Instance i ON ci.InstanceId = i.Id
GO