CREATE TABLE [dbo].[ProductAccountingData]
(
	[Id]			BIGINT									NOT NULL					IDENTITY,
	[ProductId]		BIGINT									NOT NULL,
	[InvoiceName]	NVARCHAR (255)							NOT NULL,
	[CommodityCode]	NVARCHAR (255)							NOT NULL,
	[Weight]		FLOAT									NOT NULL,
	CONSTRAINT		[PK_ProductAccountingData]				PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_ProductAccountingData_ProductId]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ProductAccountingData_ProductId  ON [dbo].[ProductAccountingData] ([ProductId] ASC);
GO