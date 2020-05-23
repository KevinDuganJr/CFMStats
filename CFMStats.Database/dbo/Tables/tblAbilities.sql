CREATE TABLE [dbo].[tblAbilities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SignatureLogoId] INT NOT NULL,
	[LeagueId] INT NOT NULL,
    [SignatureTitle] NVARCHAR(50) NOT NULL, 
    [SignatureDescription] NVARCHAR(250) NOT NULL,
	[SignatureActivationDescription] NVARCHAR(250) NOT NULL, 
    [SignatureDeactivationDescription] NVARCHAR(250) NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [ModifiedOn] DATETIME2 NOT NULL
)
