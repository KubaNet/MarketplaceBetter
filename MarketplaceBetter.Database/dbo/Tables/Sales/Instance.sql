CREATE TABLE [dbo].[Instance]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)				NOT NULL,
	[SystemName]		INT							NOT NULL,
	[Order]				INT							NOT NULL,
	[SalesChannelId]	BIGINT						NOT NULL,
	CONSTRAINT			[PK_Instance]				PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Instance_SalesChannel]	FOREIGN KEY ([SalesChannelId])	REFERENCES [dbo].[SalesChannel] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Instance_Name  ON [dbo].[Instance] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Instance_SystemName  ON [dbo].[Instance] ([SystemName] ASC);
GO