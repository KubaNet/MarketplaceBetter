UPDATE [dbo].[Variant]
	SET [Sku] = LOWER(b.Code) + '_' +  p.Code + '_' + s.Code + '_' + c.Code
	FROM Brand b JOIN Product p ON p.BrandId = b.Id JOIN Variant v ON v.ProductId = p.Id JOIN COLOR c ON v.ColorId = c.Id JOIN Size s ON v.SizeId = s.Id
	WHERE p.Id = {product_id}