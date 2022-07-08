CREATE TABLE [dbo].[MatchType]
(
	[Id]			BIGINT			NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)	NOT NULL,
	[Value]			NVARCHAR (255)	NOT NULL,
	[SystemName]	INT				NOT NULL,
	CONSTRAINT		[PK_MatchType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_MatchType_Name  ON [dbo].[MatchType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_MatchType_Value  ON [dbo].[MatchType] ([Value] ASC);
GO

CREATE UNIQUE INDEX UIX_MatchType_SystemName  ON [dbo].[MatchType] ([SystemName] ASC);
GO