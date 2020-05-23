CREATE TABLE [dbo].[tblPlayerAbilities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlayerId] INT NOT NULL, 
    [AbilityId] INT NOT NULL, 
    [LeagueId] INT NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL, 
    [ModifiedOn] DATETIME2 NOT NULL, 
    [IsEmpty] BIT NOT NULL, 
    [IsLocked] BIT NOT NULL, 
    [OvrThreshold] INT NOT NULL
)
