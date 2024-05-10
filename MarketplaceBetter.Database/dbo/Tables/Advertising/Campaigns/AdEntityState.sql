CREATE TABLE [dbo].[AdEntityState]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_AdEntityState]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AdEntityState_Name  ON [dbo].[AdEntityState] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AdEntityState_SystemName  ON [dbo].[AdEntityState] ([SystemName] ASC);
GO