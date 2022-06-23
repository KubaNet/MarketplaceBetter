CREATE TABLE [dbo].[AdEntityStatus]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_AdEntityStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AdEntityStatus_Name  ON [dbo].[AdEntityStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AdEntityStatus_SystemName  ON [dbo].[AdEntityStatus] ([SystemName] ASC);
GO