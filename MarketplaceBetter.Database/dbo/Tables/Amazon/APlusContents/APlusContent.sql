CREATE TABLE [dbo].[APlusContent]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Name]			NVARCHAR(255)				NOT NULL,
	[InstanceId]	BIGINT						NOT NULL,
	CONSTRAINT		[PK_APlusContent]			PRIMARY KEY ([Id]),
	CONSTRAINT		[PK_APlusContent_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusContent_Name  ON [dbo].[APlusContent] ([Name] ASC);
GO