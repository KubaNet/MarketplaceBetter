CREATE TABLE [dbo].[Template]
(
	[Id]		BIGINT			NOT NULL	IDENTITY,
	[FileName]	NVARCHAR(255)	NOT NULL,
	[Path]		NVARCHAR(255)	NOT NULL,
	CONSTRAINT	[PK_Template]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Template_Path_FileName  ON [dbo].[Template] ([Path] ASC, [FileName] ASC);
GO