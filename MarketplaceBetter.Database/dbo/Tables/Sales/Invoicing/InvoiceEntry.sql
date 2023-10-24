CREATE TABLE [dbo].[InvoiceEntry]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[OrderItemId]		NVARCHAR (255)				NOT NULL,
	[ShipmentItemId]	NVARCHAR (255)				NOT NULL,
	[InvoiceName]		NVARCHAR (255)				NOT NULL,
	[VariantId]			BIGINT						NOT NULL,
	[CurrencyId]		BIGINT						NOT NULL,
	[Quantity]			INT							NOT NULL,
	[GrossPrice]		DECIMAL (19,2)				NOT NULL,
	[InvoiceId]			BIGINT						NOT NULL,
	CONSTRAINT			[PK_InvoiceEntry]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_InvoiceEntry_Variant]	FOREIGN KEY ([VariantId])	REFERENCES [dbo].[Variant] ([Id]),
	CONSTRAINT			[FK_InvoiceEntry_Currency]	FOREIGN KEY ([CurrencyId])	REFERENCES [dbo].[Currency] ([Id]),
	CONSTRAINT			[FK_InvoiceEntry_Invoice]	FOREIGN KEY ([InvoiceId])	REFERENCES [dbo].[Invoice] ([Id]),
);
GO