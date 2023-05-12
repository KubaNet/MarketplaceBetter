CREATE TABLE [dbo].[APlusElementValue]
(
	[Id]				BIGINT							NOT NULL	IDENTITY,
	[ElementId]			BIGINT							NOT NULL,
	[SingleLineText]	NVARCHAR (255)					NULL,
	[BodyText]			NVARCHAR (max)					NULL,
	[ImageId]			BIGINT							NULL,
	[TrueOrFalse]		BIT								NOT NULL,
	[ModuleValueId]		BIGINT							NOT NULL,
	CONSTRAINT			[PK_APlusElementValue]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_APlusElementValue_Element]	FOREIGN KEY ([ElementId])		REFERENCES [dbo].[APlusElement] ([Id]),
	CONSTRAINT			[FK_APlusElementValue_Image]	FOREIGN KEY ([ImageId])			REFERENCES [dbo].[APlusImage] ([Id]),
	CONSTRAINT			[FK_APlusElementValue_Module]	FOREIGN KEY ([ModuleValueId])	REFERENCES [dbo].[APlusModuleValue] ([Id]),
);
GO