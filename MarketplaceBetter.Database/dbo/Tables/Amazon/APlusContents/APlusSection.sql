CREATE TABLE [dbo].[APlusSection]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_APlusSection]		PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusSection_Name  ON [dbo].[APlusSection] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_APlusSection_SystemName  ON [dbo].[APlusSection] ([SystemName] ASC);
GO