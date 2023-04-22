CREATE TABLE [dbo].[APlusSectionType]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_APlusSectionType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusSectionType_Name  ON [dbo].[APlusSectionType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_APlusSectionType_SystemName  ON [dbo].[APlusSectionType] ([SystemName] ASC);
GO