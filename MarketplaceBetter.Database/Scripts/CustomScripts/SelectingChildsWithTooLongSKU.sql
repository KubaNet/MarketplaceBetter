SELECT [Sku], LEN([Sku])
	FROM [dbo].[Child]
	WHERE LEN([Sku]) > 39
	ORDER BY LEN([Sku])