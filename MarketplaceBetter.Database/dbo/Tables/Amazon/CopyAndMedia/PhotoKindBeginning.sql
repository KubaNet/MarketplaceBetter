CREATE TABLE [dbo].[PhotoKindBeginning]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL
	CONSTRAINT	[PK_PhotoKindBeginning]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_PhotoKindBeginning_Name  ON [dbo].[PhotoKindBeginning] ([Name] ASC);
GO