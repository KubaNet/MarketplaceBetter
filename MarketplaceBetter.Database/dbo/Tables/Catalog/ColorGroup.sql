CREATE TABLE [dbo].[ColorGroup]
(
	[Id]		BIGINT			NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)	NOT NULL,
	CONSTRAINT	[PK_ColorGroup]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ColorGroup_Name  ON [dbo].[ColorGroup] ([Name] ASC);
GO