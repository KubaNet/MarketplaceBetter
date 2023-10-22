CREATE TABLE [dbo].[ProductionCost]
(
	[Id]						BIGINT										NOT NULL	IDENTITY,
	[Cost]						DECIMAL(19,2)								NOT NULL,
	[CurrencyId]				BIGINT										NOT NULL,
	[ProductAccountingDataId]	BIGINT										NOT NULL,
	CONSTRAINT					[PK_ProductionCost]							PRIMARY KEY ([Id]),
	CONSTRAINT					[FK_ProductionCost_Currency]				FOREIGN KEY ([CurrencyId])	REFERENCES [dbo].[Country] ([Id]),
	CONSTRAINT					[FK_ProductionCost_ProductAccountingData]	FOREIGN KEY ([ProductAccountingDataId])	REFERENCES [dbo].[ProductAccountingData] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductionCost_Currency_ProductAccountingData  ON [dbo].[ProductionCost] ([CurrencyId] ASC, [ProductAccountingDataId] ASC);
GO