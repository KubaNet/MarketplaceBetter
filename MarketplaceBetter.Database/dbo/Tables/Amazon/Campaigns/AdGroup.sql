CREATE TABLE [dbo].[AdGroup]
(
	[Id]			BIGINT					NOT NULL	IDENTITY,
	[Name]			NVARCHAR(255)			NOT NULL,
	[CampaignId]	BIGINT					NOT NULL,
	[AmazonId]		NVARCHAR(255)			NULL,
	[StatusId]		BIGINT					NOT NULL,
	CONSTRAINT		[PK_AdGroup]			PRIMARY KEY ([Id]),
	CONSTRAINT		[FK_AdGroup_Campaign]	FOREIGN KEY ([CampaignId])	REFERENCES [dbo].[Campaign] ([Id]),
	CONSTRAINT		[FK_AdGroup_Status]		FOREIGN KEY ([StatusId])	REFERENCES [dbo].[AdEntityStatus] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_AdGroup_Name  ON [dbo].[AdGroup] ([Name] ASC);
GO

CREATE UNIQUE INDEX UIX_AdGroup_AmazonId  ON [dbo].[AdGroup] ([AmazonId] ASC);
GO