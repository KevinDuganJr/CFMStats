CREATE TABLE [dbo].[__DatabaseVersions]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Major] INT NOT NULL,
	[Minor] INT NOT NULL,
	[Patch] INT NOT NULL,
	[LastDeployedDateTime] DATETIME2 NOT NULL
)
