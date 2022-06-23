CREATE TABLE [dbo].[ProductAd]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[VariantId]		BIGINT						NOT NULL,
	[AdGroupId]		BIGINT						NOT NULL,
	[AmazonId]		BIGINT						NULL,
	[StatusId]		BIGINT						NOT NULL,
	CONSTRAINT		[PK_ProductAd]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ProductAd_Variant]		FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT		[FK_ProductAd_AdGroupId]	FOREIGN KEY ([AdGroupId])	REFERENCES [dbo].[AdGroup] ([Id]),
	CONSTRAINT		[FK_ProductAd_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AdEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductAd_AmazonId  ON [dbo].[ProductAd] ([AmazonId] ASC);
GO