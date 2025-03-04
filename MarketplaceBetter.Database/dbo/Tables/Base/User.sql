CREATE TABLE [dbo].[User]
(
	[Id]					BIGINT						NOT NULL	IDENTITY,
	[Login]					NVARCHAR (255)				NOT NULL,
	[Password]				NVARCHAR (255)				NOT NULL,
	[IpAddress]				NVARCHAR (255)				NULL,
	[IpLastPing]			DATETIME2					NULL,
	[CurrentStatusId]		BIGINT						NULL,
	[CurrentBrandId]		BIGINT						NULL,
	[CurrentSizeId]			BIGINT						NULL,
	[CurrentCollectionId]	BIGINT						NULL,
	[CurrentInstanceId]		BIGINT						NOT NULL,
	[HideDrafts]			BIT							NOT NULL,
	[HideWithdrawn]			BIT							NOT NULL,
	[HideCopies]			BIT							NOT NULL DEFAULT(0),
	[ExpandedMenu]			NVARCHAR (255)				NOT NULL,
	CONSTRAINT				[PK_User]					PRIMARY KEY ([Id]),
	CONSTRAINT				[FK_User_CurrentStatus]		FOREIGN KEY ([CurrentStatusId])		REFERENCES [dbo].[EntityStatus] ([Id]),
	CONSTRAINT				[FK_User_CurrentBrand]		FOREIGN KEY ([CurrentBrandId])		REFERENCES [dbo].[Brand] ([Id]),
	CONSTRAINT				[FK_User_CurrentSize]		FOREIGN KEY ([CurrentSizeId])		REFERENCES [dbo].[StandardSize] ([Id]),
	CONSTRAINT				[FK_User_CurrentCollection]	FOREIGN KEY ([CurrentCollectionId])	REFERENCES [dbo].[Collection] ([Id]),
	CONSTRAINT				[FK_User_CurrentInstance]	FOREIGN KEY ([CurrentInstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_User_Login  ON [dbo].[User] ([Login] ASC);
GO

CREATE UNIQUE INDEX UIX_User_Password  ON [dbo].[User] ([Password] ASC);
GO