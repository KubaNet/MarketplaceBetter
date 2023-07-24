UPDATE [dbo].[Child]
   SET [VariantId] = C.VariantId
   FROM [Child] CI JOIN [Child] C ON CI.ChildId = C.Id