CREATE TABLE [dbo].[Template]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	[BrandId]	BIGINT					NOT NULL,
	CONSTRAINT	[PK_Template]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_Template_Related]	FOREIGN KEY ([BrandId])	REFERENCES [dbo].[Brand] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Template_Name  ON [dbo].[Template] ([Name] ASC);
GO