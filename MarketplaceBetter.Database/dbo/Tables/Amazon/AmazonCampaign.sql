CREATE TABLE [dbo].[AmazonCampaign]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Name]			NVARCHAR(255)					NOT NULL,
	[ProductId]		BIGINT							NOT NULL,
	[InstanceId]	BIGINT							NOT NULL,
	CONSTRAINT		[PK_AmazonCampaign]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AmazonCampaign_Product]		FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT		[FK_AmazonCampaign_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonCampaign_Name  ON [dbo].[AmazonCampaign] ([Name] ASC);
GO