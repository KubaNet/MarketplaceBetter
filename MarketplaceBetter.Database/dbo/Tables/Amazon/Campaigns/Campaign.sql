CREATE TABLE [dbo].[Campaign]
(
	[Id]						BIGINT							NOT NULL	IDENTITY,
	[Name]						NVARCHAR(255)					NOT NULL,
	[InstanceId]				BIGINT							NOT NULL,
	[PortfolioId]				BIGINT							NOT NULL,
	[TypeId]					BIGINT							NOT NULL,
	[StrategyId]				BIGINT							NOT NULL,
	[ProductId]					BIGINT							NULL,
	[DefaultBid]				DECIMAL							NOT NULL,
	[DailyBudget]				INT								NOT NULL,
	[TopOfSearchBidAdjustment]	INT								NOT NULL,
	[ProductPageBidAdjustment]	INT								NOT NULL,
	[BiddingStrategyId]			BIGINT							NOT NULL,
	[AmazonId]					NVARCHAR(255)					NULL,
	[StatusId]					BIGINT							NOT NULL,
	CONSTRAINT					[PK_Campaign]					PRIMARY KEY ([Id]),
	CONSTRAINT					[FK_Campaign_Instance]			FOREIGN KEY ([InstanceId])			REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT					[FK_Campaign_Portfolio]			FOREIGN KEY ([PortfolioId])			REFERENCES [dbo].[Portfolio] ([Id]),
	CONSTRAINT					[FK_Campaign_Type]				FOREIGN KEY ([TypeId])				REFERENCES [dbo].[CampaignType] ([Id]),
	CONSTRAINT					[FK_Campaign_Strategy]			FOREIGN KEY ([StrategyId])			REFERENCES [dbo].[CampaignStrategy] ([Id]),
	CONSTRAINT					[FK_Campaign_Product]			FOREIGN KEY ([ProductId])			REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT					[FK_Campaign_BiddingStrategy]	FOREIGN KEY ([BiddingStrategyId])	REFERENCES [dbo].[BiddingStrategy] ([Id]),
	CONSTRAINT					[FK_Campaign_Status]			FOREIGN KEY ([StatusId])			REFERENCES [dbo].[AdEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Campaign_Name  ON [dbo].[Campaign] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Campaign_AmazonId  ON [dbo].[Campaign] ([AmazonId] ASC) WHERE [AmazonId] IS NOT NULL;
GO