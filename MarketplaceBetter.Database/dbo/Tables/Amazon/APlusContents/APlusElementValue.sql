CREATE TABLE [dbo].[APlusElementValue]
(
	[Id]				BIGINT							NOT NULL	IDENTITY,
	[ElementId]			BIGINT							NOT NULL,
	[SingleLineText]	NVARCHAR (255)					NULL,
	[MultiLineText]		NVARCHAR (max)					NULL,
	[ImageId]			BIGINT							NULL,
	[SectionValueId]	BIGINT							NOT NULL,
	CONSTRAINT			[PK_APlusElementValue]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_APlusElementValue_Element]	FOREIGN KEY ([ElementId])		REFERENCES [dbo].[APlusElement] ([Id]),
	CONSTRAINT			[FK_APlusElementValue_Image]	FOREIGN KEY ([ImageId])			REFERENCES [dbo].[APlusImage] ([Id]),
	CONSTRAINT			[FK_APlusElementValue_Section]	FOREIGN KEY ([SectionValueId])	REFERENCES [dbo].[APlusSectionValue] ([Id]),
);
GO