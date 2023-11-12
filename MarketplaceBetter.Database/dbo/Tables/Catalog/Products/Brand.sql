CREATE TABLE [dbo].[Brand]
(
	[Id]		BIGINT			NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)	NOT NULL,
	[Code]		NVARCHAR (255)	NOT NULL,
	CONSTRAINT	[PK_Brand]		PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Brand_Name  ON [dbo].[Brand] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Brand_Code  ON [dbo].[Brand] ([Code] ASC);
GO