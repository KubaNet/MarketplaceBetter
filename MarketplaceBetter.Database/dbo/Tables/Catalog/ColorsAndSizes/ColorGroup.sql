CREATE TABLE [dbo].[ColorGroup]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	[BrandId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_ColorGroup]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_ColorGroup_Brand]	FOREIGN KEY ([BrandId])	REFERENCES [dbo].[Brand] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ColorGroup_Brand_Name  ON [dbo].[ColorGroup] ([BrandId] ASC, [Name] ASC);
GO