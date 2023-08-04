CREATE TABLE [dbo].[Size]
(
	[Id]				BIGINT					NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)			NOT NULL,
	[Code]				NVARCHAR (255)			NOT NULL,
	[IsOneSize]			BIT						NOT NULL,
	[GroupId]			BIGINT					NOT NULL,
	[StandardSizeId]	BIGINT					NOT NULL DEFAULT(1),
	CONSTRAINT			[PK_Size]				PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Size_Group]			FOREIGN KEY ([GroupId])	REFERENCES [dbo].[SizeGroup] ([Id]),
	CONSTRAINT			[FK_Size_StandardSize]	FOREIGN KEY ([StandardSizeId])	REFERENCES [dbo].[StandardSize] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Size_Group_Name  ON [dbo].[Size] ([GroupId] ASC, [Name] ASC);
GO

CREATE UNIQUE INDEX UIX_Size_Group_Code  ON [dbo].[Size] ([GroupId] ASC, [Code] ASC);
GO