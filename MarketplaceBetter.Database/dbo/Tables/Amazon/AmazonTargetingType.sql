CREATE TABLE [dbo].[AmazonTargetingType]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)				NOT NULL,
	[SystemName]	INT							NOT NULL,
	CONSTRAINT		[PK_AmazonTargetingType]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonTargetingType_Name  ON [dbo].[AmazonTargetingType] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonTargetingType_SystemName  ON [dbo].[AmazonTargetingType] ([SystemName] ASC);
GO