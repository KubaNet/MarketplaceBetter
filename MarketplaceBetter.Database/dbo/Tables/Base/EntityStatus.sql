CREATE TABLE [dbo].[EntityStatus]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_EntityStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_EntityStatus_Name  ON [dbo].[EntityStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_EntityStatus_SystemName  ON [dbo].[EntityStatus] ([SystemName] ASC);
GO