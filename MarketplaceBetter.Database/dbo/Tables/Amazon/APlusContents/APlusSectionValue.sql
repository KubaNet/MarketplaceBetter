CREATE TABLE [dbo].[APlusSectionValue]
(
	[Id]		BIGINT							NOT NULL	IDENTITY,
	[SectionId]	BIGINT							NOT NULL,
	[ContentId]	BIGINT							NOT NULL,
	CONSTRAINT	[PK_APlusSectionValue]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_APlusSectionValue_Section]	FOREIGN KEY ([SectionId])	REFERENCES [dbo].[APlusSection] ([Id]),
	CONSTRAINT  [FK_APlusSectionValue_Content]	FOREIGN KEY ([ContentId])	REFERENCES [dbo].[APlusContent] ([Id]),
);
GO