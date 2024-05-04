INSERT INTO [dbo].[Portfolio] ([Name], [CampaignTypeId], [InstanceId])
	SELECT UPPER(CT.Name), CT.Id, I.Id
	FROM [dbo].[Instance] I, [dbo].[CampaignType] CT
	WHERE I.IsNormal = 1 and NOT EXISTS (SELECT 1 FROM [dbo].[Portfolio] WHERE [InstanceId] = I.Id and [CampaignTypeId] = CT.Id)
GO