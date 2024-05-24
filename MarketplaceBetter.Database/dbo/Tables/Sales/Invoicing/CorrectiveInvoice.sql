CREATE TABLE [dbo].[CorrectiveInvoice]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[InvoiceId]		BIGINT							NOT NULL,
	[Number]		NVARCHAR (255)					NULL,
	[IsIssued]		BIT								NOT NULL,
	[IssueDate]		DATETIME2						NULL,
	[ApiNumber]		NVARCHAR (255)					NULL,
	[ApiError]		NVARCHAR (255)					NULL,
	CONSTRAINT		[PK_CorrectiveInvoice]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_CorrectiveInvoice_Invoice_]	FOREIGN KEY ([InvoiceId])	REFERENCES [dbo].[Invoice] ([Id]),
);
GO