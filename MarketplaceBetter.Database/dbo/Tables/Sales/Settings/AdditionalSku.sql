CREATE TABLE [dbo].[AdditionalSku]
(
	[Id]			BIGINT						NOT NULL						IDENTITY,
	[VariantId]		BIGINT						NOT NULL,
	[Sku]			NVARCHAR (255)				NOT NULL,
	CONSTRAINT		[PK_AdditionalSku]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AdditionalSku_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AdditionalSku_Sku  ON [dbo].[AdditionalSku] ([Sku] ASC);
GO