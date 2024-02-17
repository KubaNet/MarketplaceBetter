CREATE TABLE [dbo].[Copywriting]
(
	[Id]				BIGINT						NOT NULL	IDENTITY,
	[ProductId]			BIGINT						NOT NULL,
	[InstanceId]		BIGINT						NOT NULL,
	[ElementId]			BIGINT						NOT NULL,
	[Value]				NVARCHAR (4000)				NOT NULL,
	[ByteCount]			INT							NOT NULL,
	[HasProperLength]	BIT							NOT NULL,
	CONSTRAINT			[PK_Copywriting]			PRIMARY KEY ([Id]),
	CONSTRAINT			[FK_Copywriting_Product]	FOREIGN KEY ([ProductId])	REFERENCES [dbo].[Product] ([Id]),
	CONSTRAINT			[FK_Copywriting_Instance]	FOREIGN KEY ([InstanceId])	REFERENCES [dbo].[Instance] ([Id]),
	CONSTRAINT			[FK_Copywriting_Element]	FOREIGN KEY ([ElementId])	REFERENCES [dbo].[CopywritingElement] ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_Copywriting_Product_Instance_Element  ON [dbo].[Copywriting] ([ProductId] ASC, [InstanceId] ASC, [ElementId] ASC);
GO