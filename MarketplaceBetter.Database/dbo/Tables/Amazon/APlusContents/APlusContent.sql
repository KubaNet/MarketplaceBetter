CREATE TABLE [dbo].[APlusContent]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Name]			NVARCHAR(255)				NOT NULL,
	[ProductId]		BIGINT						NOT NULL,
	[InstanceId]	BIGINT						NOT NULL,
	[StatusId]		BIGINT						NOT NULL,
	[AllVariants]	BIT							NOT NULL,
	CONSTRAINT		[PK_APlusContent]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_APlusContent_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT		[FK_APlusContent_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT		[FK_APlusContent_Status]	FOREIGN KEY ([StatusId])	REFERENCES [dbo].[EntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusContent_Name  ON [dbo].[APlusContent] ([Name] ASC);
GO