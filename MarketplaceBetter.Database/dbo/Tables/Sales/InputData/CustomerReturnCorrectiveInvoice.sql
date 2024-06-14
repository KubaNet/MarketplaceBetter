CREATE TABLE [dbo].[CustomerReturnCorrectiveInvoice]
(
	[CustomerReturnId]		BIGINT													NOT NULL,
	[CorrectiveInvoiceId]	BIGINT													NOT NULL,
	CONSTRAINT				[PK_CustomerReturnCorrectiveInvoice]					PRIMARY KEY ([CustomerReturnId], [CorrectiveInvoiceId]),
	CONSTRAINT				[FK_CustomerReturnCorrectiveInvoice_CustomerReturn]		FOREIGN KEY ([CustomerReturnId])	REFERENCES [dbo].[CustomerReturn] ([Id]),
	CONSTRAINT				[FK_CustomerReturnCorrectiveInvoice_CorrectiveInvoice]	FOREIGN KEY ([CorrectiveInvoiceId])	REFERENCES [dbo].[CorrectiveInvoice] ([Id]),
)
