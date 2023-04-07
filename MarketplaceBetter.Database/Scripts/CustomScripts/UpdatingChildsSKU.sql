UPDATE [dbo].[Variant]
   SET [Sku] = v.Sku
   FROM Variant ch JOIN Variant v ON ch.VariantId = v.Id
GO