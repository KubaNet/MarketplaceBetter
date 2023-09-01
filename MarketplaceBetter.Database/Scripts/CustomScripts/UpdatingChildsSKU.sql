UPDATE [dbo].[Child]
	SET [Sku] = LOWER(i.Name) + '_' + v.Sku
	FROM Child c JOIN Variant v ON c.VariantId = v.Id JOIN Instance i ON c.InstanceId = i.Id JOIN Product p ON p.Id = v.ProductId
	WHERE p.Id = {product_id}