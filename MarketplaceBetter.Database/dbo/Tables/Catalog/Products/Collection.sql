CREATE TABLE [dbo].[Collection]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	[BrandId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_Collection]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_Collection_Brand]	FOREIGN KEY ([BrandId])	REFERENCES [dbo].[Brand] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Collection_Brand_Name  ON [dbo].[Collection] ([BrandId] ASC, [Name] ASC);
GO