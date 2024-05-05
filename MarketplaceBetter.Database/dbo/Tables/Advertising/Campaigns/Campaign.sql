CREATE TABLE [dbo].[Campaign]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[AmazonId]		NVARCHAR (255)			NULL,
	[InstanceId]	BIGINT					NOT NULL,
	[PortfolioId]	BIGINT					NOT NULL,
	[TypeId]		BIGINT					NOT NULL,
	[ProductId]		BIGINT					NOT NULL,
	CONSTRAINT		[PK_Campaign]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Campaign_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_Campaign_Portfolio]	FOREIGN KEY ([PortfolioId])	REFERENCES [dbo].[Portfolio] ([Id]),
	CONSTRAINT		[FK_Campaign_Type]		FOREIGN KEY ([TypeId])	REFERENCES [dbo].[CampaignType] ([Id]),
	CONSTRAINT		[FK_Campaign_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Campaign_Name  ON [dbo].[Campaign] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Campaign_AmazonId  ON [dbo].[Campaign] ([AmazonId] ASC);
GO