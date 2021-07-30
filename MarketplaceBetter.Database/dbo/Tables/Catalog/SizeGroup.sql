CREATE TABLE [dbo].[SizeGroup]
(
	[Id]		BIGINT			NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)	NOT NULL,
	CONSTRAINT	[PK_SizeGroup]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_SizeGroup_Name  ON [dbo].[SizeGroup] ([Name] ASC);
GO