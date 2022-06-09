CREATE TABLE [dbo].[ResearchTargetSource]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)				NOT NULL,
	[SystemName]	INT							NOT NULL,
	CONSTRAINT		[PK_ResearchTargetSource]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ResearchTargetSource_Name  ON [dbo].[ResearchTargetSource] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_ResearchTargetSource_SystemName  ON [dbo].[ResearchTargetSource] ([SystemName] ASC);
GO