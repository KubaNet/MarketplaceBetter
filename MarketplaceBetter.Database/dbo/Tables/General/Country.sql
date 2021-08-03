CREATE TABLE [dbo].[Country]
(
	[Id]			BIGINT			NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)	NOT NULL,
	[SystemName]	INT				NOT NULL,
	[Code]			NCHAR (2)		NOT NULL,
	CONSTRAINT		[PK_Country]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Country_Name  ON [dbo].[Country] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Country_SystemName  ON [dbo].[Country] ([SystemName] ASC);
GO

CREATE UNIQUE INDEX UIX_Country_Code  ON [dbo].[Country] ([Code] ASC);
GO