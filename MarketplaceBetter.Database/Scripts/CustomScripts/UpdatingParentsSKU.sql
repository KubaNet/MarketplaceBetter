UPDATE [dbo].[Parent]
   SET [Sku] = LOWER(b.Code) + '_' + p.Code
   FROM Parent pt JOIN Product p ON pt.ProductId = p.Id JOIN Brand b ON p.BrandId = b.Id
GO