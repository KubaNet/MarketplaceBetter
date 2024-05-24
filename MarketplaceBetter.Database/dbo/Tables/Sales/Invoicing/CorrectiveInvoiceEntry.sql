CREATE TABLE [dbo].[CorrectiveInvoiceEntry]
(
	[Id]					BIGINT											NOT NULL	IDENTITY,
	[Quantity]				INT												NOT NULL,
	[GrossPrice]			DECIMAL (19,2)									NOT NULL,
	[CorrectiveInvoiceId]	BIGINT											NOT NULL,
	[InvoiceEntryId]		BIGINT											NOT NULL,
	CONSTRAINT				[PK_CorrectiveInvoiceEntry]						PRIMARY KEY ([Id]),
	CONSTRAINT				[FK_CorrectiveInvoiceEntry_CorrectiveInvoice]	FOREIGN KEY ([CorrectiveInvoiceId])	REFERENCES [dbo].[CorrectiveInvoice] ([Id]),
	CONSTRAINT				[FK_CorrectiveInvoiceEntry_InvoiceEntry]		FOREIGN KEY ([InvoiceEntryId])	REFERENCES [dbo].[InvoiceEntry] ([Id]),
);
GO