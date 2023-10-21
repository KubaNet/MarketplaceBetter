CREATE TABLE [dbo].[ProductionCost]
(
	[Id]			BIGINT							NOT NULL					IDENTITY,
	[Cost]			DECIMAL(19,2)					NOT NULL,
	[CurrencyId]	BIGINT							NOT NULL,
	CONSTRAINT		[PK_ProductionCost]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ProductionCost_Currency]	FOREIGN KEY ([CurrencyId])	REFERENCES [dbo].[Country] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductionCost_Currency_CountryTo  ON [dbo].[ProductionCost] ([CurrencyId]);
GO