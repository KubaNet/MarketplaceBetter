CREATE TABLE [dbo].[User]
(
	[Id]				BIGINT				NOT NULL	IDENTITY,
	[Login]				NVARCHAR (255)		NOT NULL,
	[Password]			NVARCHAR (255)		NOT NULL,
	[IpAddress]			NVARCHAR (255)		NULL,
	[IpLastPing]		DATETIME2			NULL,
	[CurrentBrandId]	BIGINT				NULL,
	[CurrentInstanceId]	BIGINT				NOT NULL,
	[ShowDrafts]		BIT					NOT NULL,
	[ShowWithdrawn]		BIT					NOT NULL,
	[ExpandedMenu]		NVARCHAR (255)		NOT NULL,
	CONSTRAINT	[PK_User]					PRIMARY KEY ([Id]),
	CONSTRAINT  [FK_User_CurrentBrand]		FOREIGN KEY ([CurrentBrandId])		REFERENCES [dbo].[Brand] ([Id]),
	CONSTRAINT  [FK_User_CurrentInstance]	FOREIGN KEY ([CurrentInstanceId])	REFERENCES [dbo].[Instance] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_User_Login  ON [dbo].[User] ([Login] ASC);
GO

CREATE UNIQUE INDEX UIX_User_Password  ON [dbo].[User] ([Password] ASC);
GO