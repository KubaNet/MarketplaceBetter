CREATE TABLE [dbo].[VariantStatus]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_VariantStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_VariantStatus_Name  ON [dbo].[VariantStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_VariantStatus_SystemName  ON [dbo].[VariantStatus] ([SystemName] ASC);
GO