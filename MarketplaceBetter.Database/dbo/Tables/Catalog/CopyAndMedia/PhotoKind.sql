CREATE TABLE [dbo].[PhotoKind]
(
	[Id]				BIGINT					NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)			NOT NULL
	CONSTRAINT			[PK_PhotoKind]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_PhotoKind_Name  ON [dbo].[PhotoKind] ([Name] ASC);
GO