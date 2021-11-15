CREATE TABLE [dbo].[ColorTranslation]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[InstanceId]	BIGINT						NOT NULL,
	[ColorId]		BIGINT						NOT NULL,
	[Translation]	NVARCHAR (255)				NOT NULL,
	[Mapping]		NVARCHAR (255)				NULL,
	CONSTRAINT	[PK_ColorTranslation]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_ColorTranslation_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT  [FK_ColorTranslation_Color]		FOREIGN KEY ([ColorId])		REFERENCES [dbo].[Color] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_ColorTranslation_Instance_Color  ON [dbo].[ColorTranslation] ([InstanceId] ASC, [ColorId] ASC);
GO