CREATE TABLE [dbo].[CustomerReturn]
(
	[Id]					BIGINT									NOT NULL	IDENTITY,
	[ReturnDate]			DATETIME2								NOT NULL,
	[OrderId]				NVARCHAR (255)							NOT NULL,
	[Sku]					NVARCHAR (255)							NOT NULL,
	[Asin]					NVARCHAR (255)							NOT NULL,
	[Fnsku]					NVARCHAR (255)							NOT NULL,
	[ProductName]			NVARCHAR (255)							NOT NULL,
	[Quantity]				INT										NOT NULL,
	[FulfillmentCenterId]	NVARCHAR (255)							NOT NULL,
	[DetailedDispositionId]	BIGINT									NOT NULL,
	[ReasonId]				BIGINT									NOT NULL,
	[StatusId]				BIGINT									NOT NULL,
	[LicensePlateNumber]	NVARCHAR (255)							NOT NULL,
	[CustomerComments]		NVARCHAR (2000)							NULL,
	CONSTRAINT				[PK_CustomerReturn]						PRIMARY KEY ([Id]),
	CONSTRAINT				[FK_CustomerReturn_DetailedDisposition]	FOREIGN KEY ([DetailedDispositionId])	REFERENCES [dbo].[ReturnDetailedDisposition] ([Id]),
	CONSTRAINT				[FK_CustomerReturn_Reason]				FOREIGN KEY ([ReasonId])	REFERENCES [dbo].[ReturnReason] ([Id]),
	CONSTRAINT				[FK_CustomerReturn_Invoice]				FOREIGN KEY ([StatusId])	REFERENCES [dbo].[ReturnStatus] ([Id]),
);
GO