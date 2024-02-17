CREATE TABLE [dbo].[CopywritingElement]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[SystemName]	INT						NOT NULL,
	[MaxByteCount]	INT						NOT NULL,
	CONSTRAINT		[PK_CopywritingElement]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_CopywritingElement_Name  ON [dbo].[CopywritingElement] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_CopywritingElement_SystemName  ON [dbo].[CopywritingElement] ([SystemName] ASC);
GO