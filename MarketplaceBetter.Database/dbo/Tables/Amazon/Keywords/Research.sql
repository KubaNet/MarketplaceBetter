CREATE TABLE [dbo].[Research]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	CONSTRAINT	[PK_Research]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Research_Name  ON [dbo].[Research] ([Name] ASC);
GO