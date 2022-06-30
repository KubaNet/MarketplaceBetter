CREATE TABLE [dbo].[BiddingStrategy]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	CONSTRAINT		[PK_BiddingStrategy]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_BiddingStrategy_Name  ON [dbo].[BiddingStrategy] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_BiddingStrategy_SystemName  ON [dbo].[BiddingStrategy] ([SystemName] ASC);
GO