CREATE TABLE [dbo].[Ad]
(
	[Id]		BIGINT			NOT NULL	IDENTITY,
	[AmazonId]	NVARCHAR (255)	NULL,
	[GroupId]	BIGINT			NOT NULL,
	[StateId]	BIGINT			NOT NULL,
	CONSTRAINT	[PK_Ad]			PRIMARY KEY ([Id]),
	CONSTRAINT	[FK_Ad_Group]	FOREIGN KEY ([GroupId])	REFERENCES [dbo].[AdGroup] ([Id]),
	CONSTRAINT	[FK_Ad_State]	FOREIGN KEY ([StateId])	REFERENCES [dbo].[AdEntityState] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Ad_AmazonId  ON [dbo].[Ad] ([AmazonId] ASC);
GO