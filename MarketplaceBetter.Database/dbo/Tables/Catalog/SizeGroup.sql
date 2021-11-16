CREATE TABLE [dbo].[SizeGroup]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	[BrandId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_SizeGroup]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_SizeGroup_Brand]	FOREIGN KEY ([BrandId])	REFERENCES [dbo].[Brand] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_SizeGroup_Brand_Name  ON [dbo].[SizeGroup] ([BrandId] ASC, [Name] ASC);
GO