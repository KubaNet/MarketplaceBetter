CREATE TABLE [dbo].[ReturnReason]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_ReturnReason]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ReturnReason_Name  ON [dbo].[ReturnReason] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_ReturnReason_SystemName  ON [dbo].[ReturnReason] ([SystemName] ASC);
GO