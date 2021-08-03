CREATE TABLE [dbo].[SalesChannel]
(
	[Id]			BIGINT				NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)		NOT NULL,
	[SystemName]	INT					NOT NULL,
	CONSTRAINT		[PK_SalesChannel]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_SalesChannel_Name  ON [dbo].[SalesChannel] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_SalesChannel_SystemName  ON [dbo].[SalesChannel] ([SystemName] ASC);
GO