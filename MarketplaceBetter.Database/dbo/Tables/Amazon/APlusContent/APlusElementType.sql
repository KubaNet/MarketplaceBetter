CREATE TABLE [dbo].[APlusElementType]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_APlusElementType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusElementType_Name  ON [dbo].[APlusElementType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_APlusElementType_SystemName  ON [dbo].[APlusElementType] ([SystemName] ASC);
GO