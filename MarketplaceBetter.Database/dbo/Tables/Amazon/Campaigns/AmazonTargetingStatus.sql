CREATE TABLE [dbo].[AmazonTargetingStatus]
(
	[Id]			BIGINT						NOT NULL	IDENTITY,
	[Name]			NVARCHAR (255)				NOT NULL,
	[SystemName]	INT							NOT NULL,
	CONSTRAINT		[PK_AmazonTargetingStatus]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AmazonTargetingStatus_Name  ON [dbo].[AmazonTargetingStatus] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AmazonTargetingStatus_SystemName  ON [dbo].[AmazonTargetingStatus] ([SystemName] ASC);
GO