UPDATE [dbo].[ChildInstance]
   SET [VariantId] = C.VariantId
   FROM [ChildInstance] CI JOIN [Child] C ON CI.ChildId = C.Id