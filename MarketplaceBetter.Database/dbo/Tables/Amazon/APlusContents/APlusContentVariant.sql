CREATE TABLE [dbo].[APlusContentVariant]
(
	[Id]		BIGINT								NOT NULL	IDENTITY,
	[ContentId]	BIGINT								NOT NULL,
	[VariantId]	BIGINT								NOT NULL,
	CONSTRAINT	[PK_APlusContentVariant]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_APlusContentVariant_Content]	FOREIGN KEY ([ContentId])	REFERENCES [dbo].[APlusContent] ([Id]),
	CONSTRAINT  [FK_APlusContentVariant_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusContentVariant_Content_Variant  ON [dbo].[APlusContentVariant] ([ContentId] ASC, [VariantId] ASC);
GO