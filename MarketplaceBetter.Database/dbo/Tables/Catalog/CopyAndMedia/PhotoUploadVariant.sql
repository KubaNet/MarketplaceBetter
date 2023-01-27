CREATE TABLE [dbo].[PhotoUploadVariant]
(
	[Id]				BIGINT								NOT NULL	IDENTITY,
	[PhotoUploadId]		BIGINT								NOT NULL,
	[VariantId]			BIGINT								NOT NULL,
	CONSTRAINT			[PK_PhotoUploadVariant]				PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_PhotoUploadVariant_PhotoUpload]	FOREIGN KEY ([PhotoUploadId])	REFERENCES [dbo].[PhotoUpload] ([Id]),
	CONSTRAINT			[FK_PhotoUploadVariant_Variant]		FOREIGN KEY ([VariantId])		REFERENCES [dbo].[Variant] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_PhotoUploadVariant_PhotoUpload_Variant  ON [dbo].[PhotoUploadVariant] ([PhotoUploadId] ASC, [VariantId] ASC);
GO