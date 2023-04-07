UPDATE [dbo].[ParentInstance]
   SET [ProductId] = P.ProductId
   FROM [ParentInstance] PIN JOIN [Parent] P ON PIN.ParentId = P.Id