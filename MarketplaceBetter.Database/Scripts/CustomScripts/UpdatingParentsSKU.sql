UPDATE [dbo].[Parent]
	SET [Sku] = LOWER(i.Name) + '_' + LOWER(b.Code) + '_' + p.Code
	FROM Parent pa JOIN Product p ON pa.ProductId = p.Id JOIN Instance i ON pa.InstanceId = i.Id JOIN Brand b ON p.BrandId = b.Id
	WHERE pa.ProductId = {product_id}