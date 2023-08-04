CREATE TABLE [dbo].[StandardSize]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_StandardSize]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_StandardSize_Name  ON [dbo].[StandardSize] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_StandardSize_SystemName  ON [dbo].[StandardSize] ([SystemName] ASC);
GO