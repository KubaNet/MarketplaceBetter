CREATE TABLE [dbo].[APlusElement]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)				NOT NULL,
	[TypeId]			BIGINT						NOT NULL,
	[CharactersLimit]	INT							NULL,
	[Order]				INT							NOT NULL,
	[SectionId]			BIGINT						NOT NULL,
	CONSTRAINT			[PK_APlusElement]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_APlusElement_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[APlusElementType] ([Id]),
	CONSTRAINT			[FK_APlusElement_Section]	FOREIGN KEY ([SectionId])	REFERENCES [dbo].[APlusSection] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusElement_Section_Order  ON [dbo].[APlusElement] ([SectionId] ASC, [Order] ASC);
GO