CREATE TABLE [dbo].[APlusModuleValue]
(
	[Id]		BIGINT							NOT NULL	IDENTITY,
	[ModuleId]	BIGINT							NOT NULL,
	[Order]		INT								NOT NULL,
	[ContentId]	BIGINT							NOT NULL,
	CONSTRAINT	[PK_APlusModuleValue]			PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_APlusModuleValue_Module]	FOREIGN KEY ([ModuleId])	REFERENCES [dbo].[APlusModule] ([Id]),
	CONSTRAINT  [FK_APlusModuleValue_Content]	FOREIGN KEY ([ContentId])	REFERENCES [dbo].[APlusContent] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_APlusModuleValue_Content_Order  ON [dbo].[APlusModuleValue] ([ContentId] ASC, [Order] ASC);
GO