CREATE TABLE [dbo].[Supplier]
(
	[Id]		BIGINT			NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)	NOT NULL,
	CONSTRAINT	[PK_Supplier]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Supplier_Name  ON [dbo].[Supplier] ([Name] ASC);
GO