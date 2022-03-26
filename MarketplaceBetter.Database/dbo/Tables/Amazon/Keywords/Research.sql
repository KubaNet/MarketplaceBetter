CREATE TABLE [dbo].[Research]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[InstanceId]	BIGINT					NOT NULL,
	CONSTRAINT		[PK_Research]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Research_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Research_Name_Instance  ON [dbo].[Research] ([Name] ASC, [InstanceId] ASC);
GO