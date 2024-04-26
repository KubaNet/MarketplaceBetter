CREATE TABLE [dbo].[ReturnDetailedDisposition]
(
	[Id]			BIGINT							NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)					NOT NULL,
	[SystemName]	INT								NOT NULL,
	CONSTRAINT		[PK_ReturnDetailedDisposition]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ReturnDetailedDisposition_Name  ON [dbo].[ReturnDetailedDisposition] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_ReturnDetailedDisposition_SystemName  ON [dbo].[ReturnDetailedDisposition] ([SystemName] ASC);
GO