CREATE TABLE [dbo].[APlusSection]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[TypeId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_APlusSection]		PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_APlusSection_Type]	FOREIGN KEY ([TypeId])	REFERENCES [dbo].[APlusSectionType] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusSection_Type  ON [dbo].[APlusSection] ([TypeId] ASC);
GO