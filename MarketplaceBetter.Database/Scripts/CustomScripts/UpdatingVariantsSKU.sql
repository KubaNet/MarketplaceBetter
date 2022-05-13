UPDATE [dbo].[Variant]
   SET [Sku] = b.Code + '_' + p.Code + '_' + c.Code + '_' + s.Code
   FROM Brand b JOIN Product p ON p.BrandId = b.Id JOIN Variant v ON v.ProductId = p.Id JOIN COLOR c ON v.ColorId = c.Id JOIN Size s ON v.SizeId = s.Id
GO