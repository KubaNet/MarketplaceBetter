CREATE TABLE [dbo].[Currency]
(
	[Id]			BIGINT			NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)	NOT NULL,
	[SystemName]	INT				NOT NULL,
	CONSTRAINT		[PK_Currency]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Currency_Name  ON [dbo].[Currency] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Currency_SystemName  ON [dbo].[Currency] ([SystemName] ASC);
GO