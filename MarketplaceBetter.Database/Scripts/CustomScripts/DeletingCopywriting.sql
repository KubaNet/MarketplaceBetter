DELETE C FROM [dbo].[Copywriting] C JOIN Product P ON C.ProductId = P.Id JOIN CopywritingElement CE ON C.ElementId = CE.Id
      WHERE P.BrandId = 1 and CE.SystemName != 1 and CE.SystemName != 2