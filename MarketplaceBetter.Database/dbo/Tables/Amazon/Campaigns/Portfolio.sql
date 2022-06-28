CREATE TABLE [dbo].[Portfolio]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)			NOT NULL,
	[InstanceId]	BIGINT					NOT NULL,
	[AmazonId]		NVARCHAR (255)			NULL,
	CONSTRAINT		[PK_Portfolio]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_Portfolio_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Portfolio_Name_Instance  ON [dbo].[Portfolio] ([Name] ASC, [InstanceId] ASC);
GO

CREATE UNIQUE INDEX UIX_Portfolio_AmazonId  ON [dbo].[Portfolio] ([AmazonId] ASC) WHERE [AmazonId] IS NOT NULL;
GO