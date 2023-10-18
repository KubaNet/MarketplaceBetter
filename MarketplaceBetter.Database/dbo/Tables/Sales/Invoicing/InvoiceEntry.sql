CREATE TABLE [dbo].[InvoiceEntry]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[OrderItemId]		NVARCHAR (255)				NOT NULL,
	[ShipmentItemId]	NVARCHAR (255)				NOT NULL,
	[Sku]				NVARCHAR (255)				NOT NULL,
	[ProductName]		NVARCHAR (255)				NOT NULL,
	[CurrencyId]		BIGINT						NOT NULL,
	[Quantity]			NVARCHAR (255)				NOT NULL,
	[GrossPrice]		DECIMAL (19,2)				NOT NULL,
	[VariantId]			BIGINT						NOT NULL,
	CONSTRAINT			[PK_InvoiceEntry]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_InvoiceEntry_Currency]	FOREIGN KEY ([CurrencyId])	REFERENCES [dbo].[Currency] ([Id]),
	CONSTRAINT			[FK_InvoiceEntry_VatRule]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
);
GO