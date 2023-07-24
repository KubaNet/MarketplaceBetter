UPDATE [dbo].[Parent]
   SET [ProductId] = P.ProductId
   FROM [Parent] PIN JOIN [Parent] P ON PIN.ParentId = P.Id