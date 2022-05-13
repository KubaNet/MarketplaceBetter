UPDATE [dbo].[Child]
   SET [Sku] = p.ChildSku + '_' + c.Code + '_' + s.Code
   FROM Parent p JOIN Child ch ON ch.ParentId = p.Id JOIN Variant v ON ch.VariantId = v.Id JOIN Color c ON v.ColorId = c.Id JOIN Size s ON v.SizeId = s.Id
GO

UPDATE [dbo].[Child]
   SET [Sku] = REPLACE(Child.Sku, '_OS', '')
   WHERE Sku like '%_OS'
GO