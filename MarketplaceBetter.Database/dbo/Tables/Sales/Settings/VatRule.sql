CREATE TABLE [dbo].[VatRule]
(
	[Id]			BIGINT						NOT NULL						IDENTITY,
	[CountryFromId]	BIGINT						NOT NULL,
	[CountryToId]	BIGINT						NOT NULL,
	[VatValue]		INT							NOT NULL,
	[VatNumber]		NVARCHAR (255)				NOT NULL,
	CONSTRAINT		[PK_VatRule]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_VatRule_CountryFrom]	FOREIGN KEY ([CountryFromId])	REFERENCES [dbo].[Country] ([Id]),
	CONSTRAINT		[FK_VatRule_CountryTo]		FOREIGN KEY ([CountryToId])		REFERENCES [dbo].[Country] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_VatRule_CountryFrom_CountryTo  ON [dbo].[VatRule] ([CountryFromId] ASC, [CountryToId] ASC);
GO