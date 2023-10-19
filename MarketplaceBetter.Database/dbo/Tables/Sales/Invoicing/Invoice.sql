CREATE TABLE [dbo].[Invoice]
(
	[Id]					BIGINT						NOT NULL	IDENTITY,
	[Number]				NVARCHAR (255)				NULL,
	[OrderId]				NVARCHAR (255)				NOT NULL,
	[PaymentDate]			DATETIME2					NOT NULL,
	[Buyer]					NVARCHAR (255)				NOT NULL,
	[BuyerFirstName]		NVARCHAR (255)				NOT NULL,
	[BuyerLastName]			NVARCHAR (255)				NOT NULL,
	[BuyerStreet]			NVARCHAR (255)				NOT NULL,
	[BuyerCity]				NVARCHAR (255)				NOT NULL,
	[BuyerPostalCode]		NVARCHAR (255)				NOT NULL,
	[BuyerState]			NVARCHAR (255)				NOT NULL,
	[BuyerCountry]			NVARCHAR (255)				NOT NULL,
	[VatRuleId]				BIGINT						NOT NULL,
	[ShippingGrossPrice]	NVARCHAR (255)				NOT NULL,
	[IsIssued]				BIT							NOT NULL,
	[ApiNumber]				NVARCHAR (255)				NOT NULL,
	[ApiError]				NVARCHAR (255)				NOT NULL,
	CONSTRAINT				[PK_Invoice]				PRIMARY KEY ([Id]),
	CONSTRAINT				[FK_Invoice_VatRule]		FOREIGN KEY ([VatRuleId])	REFERENCES [dbo].[VatRule] ([Id]),
);
GO