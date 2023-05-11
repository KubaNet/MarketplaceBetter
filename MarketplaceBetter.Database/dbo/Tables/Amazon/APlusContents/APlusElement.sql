CREATE TABLE [dbo].[APlusElement]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[Name]				NVARCHAR (255)				NOT NULL,
	[TypeId]			BIGINT						NOT NULL,
	[CharactersLimit]	INT							NULL,
	[Order]				INT							NOT NULL,
	[ModuleId]			BIGINT						NOT NULL,
	CONSTRAINT			[PK_APlusElement]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_APlusElement_Type]		FOREIGN KEY ([TypeId])		REFERENCES [dbo].[APlusElementType] ([Id]),
	CONSTRAINT			[FK_APlusElement_Module]	FOREIGN KEY ([ModuleId])	REFERENCES [dbo].[APlusModule] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusElement_Module_Order  ON [dbo].[APlusElement] ([ModuleId] ASC, [Order] ASC);
GO

CREATE UNIQUE INDEX UIX_APlusElement_Module_Name  ON [dbo].[APlusElement] ([ModuleId] ASC, [Name] ASC);
GO