CREATE TABLE [dbo].[Campaign]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR(255)			NOT NULL,
	[InstanceId]	BIGINT					NOT NULL,
	[TypeId]		BIGINT					NOT NULL,
	[StrategyId]	BIGINT					NOT NULL,
	[ProductId]		BIGINT					NULL,
	[DefaultBid]	DECIMAL					NOT NULL,
	CONSTRAINT		[PK_Campaign]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Campaign_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_Campaign_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[CampaignType] ([Id]),
	CONSTRAINT		[FK_Campaign_Strategy]	FOREIGN KEY ([StrategyId])	REFERENCES [dbo].[CampaignStrategy] ([Id]),
	CONSTRAINT		[FK_Campaign_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Campaign_Name  ON [dbo].[Campaign] ([Name] ASC);
GO