CREATE TABLE [dbo].[MeasurementUnit]
(
	[Id]		BIGINT					NOT NULL	IDENTITY,
	[Name]		NVARCHAR (255)			NOT NULL,
	CONSTRAINT	[PK_MeasurementUnit]	PRIMARY KEY ([Id]),
);
GO

CREATE UNIQUE INDEX UIX_MeasurementUnit_Name  ON [dbo].[MeasurementUnit] ([Name] ASC);
GO