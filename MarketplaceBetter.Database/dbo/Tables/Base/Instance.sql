CREATE TABLE [dbo].[Instance]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)				NOT NULL,
	[SystemName]		INT							NOT NULL,
	[IsNormal]			BIT							NOT NULL,
	CONSTRAINT			[PK_Instance]				PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Instance_Name  ON [dbo].[Instance] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Instance_SystemName  ON [dbo].[Instance] ([SystemName] ASC);
GO