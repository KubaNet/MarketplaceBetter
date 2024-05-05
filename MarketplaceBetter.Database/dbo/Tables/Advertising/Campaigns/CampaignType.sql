CREATE TABLE [dbo].[AdGroupState]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_AdGroupState]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AdGroupState_Name  ON [dbo].[AdGroupState] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AdGroupState_SystemName  ON [dbo].[AdGroupState] ([SystemName] ASC);
GO