CREATE TABLE [dbo].[MaterialKind]
(
	[Id]		BIGINT				NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)		NOT NULL,
	CONSTRAINT	[PK_MaterialKind]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_MaterialKind_Name  ON [dbo].[MaterialKind] ([Name] ASC);
GO