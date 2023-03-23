UPDATE [dbo].[Child]
   SET [Sku] = v.Sku
   FROM Child ch JOIN Variant v ON ch.VariantId = v.Id
GO