CREATE TABLE [dbo].[Color]
(
	[Id]		BIGINT				NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)		NOT NULL,
	[Code]		NVARCHAR (255)		NOT NULL,
	[GroupId]	BIGINT				NOT NULL,
	CONSTRAINT	[PK_Color]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_Color_Group]	FOREIGN KEY ([GroupId])	REFERENCES [dbo].[ColorGroup] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Color_Group_Name  ON [dbo].[Color] ([GroupId] ASC, [Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Color_Group_Code  ON [dbo].[Color] ([GroupId] ASC, [Code] ASC);
GO