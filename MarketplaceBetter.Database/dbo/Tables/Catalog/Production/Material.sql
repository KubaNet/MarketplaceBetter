CREATE TABLE [dbo].[Material]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SupplierCode]	NVARCHAR (255)			NOT NULL,
	[KindId]		BIGINT					NOT NULL,
	[SupplierId]	BIGINT					NOT NULL,
	[PricePLN]		DECIMAL (19,2)			NOT NULL,
	[UnitId]		BIGINT					NOT NULL,
	CONSTRAINT		[PK_Material]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Material_Kind]		FOREIGN KEY ([KindId])		REFERENCES [dbo].[MaterialKind] ([Id]),
	CONSTRAINT		[FK_Material_Supplier]	FOREIGN KEY ([SupplierId])	REFERENCES [dbo].[Supplier] ([Id]),
	CONSTRAINT		[FK_Material_Unit]		FOREIGN KEY ([UnitId])		REFERENCES [dbo].[MeasurementUnit] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Material_Supplier_Name  ON [dbo].[Material] ([SupplierId] ASC, [Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Material_Supplier_SupplierCode  ON [dbo].[Material] ([SupplierId] ASC, [SupplierCode] ASC);
GO