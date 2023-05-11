CREATE TABLE [dbo].[APlusModule]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_APlusModule]		PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusModule_Name  ON [dbo].[APlusModule] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_APlusModule_SystemName  ON [dbo].[APlusModule] ([SystemName] ASC);
GO