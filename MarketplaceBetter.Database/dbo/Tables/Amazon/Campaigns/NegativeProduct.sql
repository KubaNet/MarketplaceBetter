CREATE TABLE [dbo].[NegativeProduct]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Asin]			NVARCHAR(255)					NOT NULL,
	[AdGroupId]		BIGINT							NOT NULL,
	[AmazonId]		NVARCHAR(255)					NULL,
	[StatusId]		BIGINT							NOT NULL,
	CONSTRAINT		[PK_NegativeProduct]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_NegativeProduct_AdGroupId]	FOREIGN KEY ([AdGroupId])	REFERENCES [dbo].[AdGroup] ([Id]),
	CONSTRAINT		[FK_NegativeProduct_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AdEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_NegativeProduct_AmazonId  ON [dbo].[NegativeProduct] ([AmazonId] ASC) WHERE [AmazonId] IS NOT NULL;
GO

CREATE UNIQUE INDEX UIX_NegativeProduct_AdGroup_Asin  ON [dbo].[NegativeProduct] ([AdGroupId] ASC, [Asin] ASC);
GO