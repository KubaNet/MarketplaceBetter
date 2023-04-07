UPDATE [dbo].[Product]
   SET [Sku] = LOWER(b.Code) + '_' + p.Code
   FROM Product pt JOIN Product p ON pt.ProductId = p.Id JOIN Brand b ON p.BrandId = b.Id
GO