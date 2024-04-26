CREATE TABLE [dbo].[ReturnStatus]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_ReturnStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ReturnStatus_Name  ON [dbo].[ReturnStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_ReturnStatus_SystemName  ON [dbo].[ReturnStatus] ([SystemName] ASC);
GO