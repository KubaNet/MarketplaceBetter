CREATE TABLE [dbo].[ResearchTargetStatus]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)				NOT NULL,
	[SystemName]	INT							NOT NULL,
	CONSTRAINT		[PK_ResearchTargetStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ResearchTargetStatus_Name  ON [dbo].[ResearchTargetStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_ResearchTargetStatus_SystemName  ON [dbo].[ResearchTargetStatus] ([SystemName] ASC);
GO