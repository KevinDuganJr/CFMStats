CREATE TABLE [dbo].[tblPlayerContract]
(
	[playerId]                  INT NOT NULL, 
	[leagueId]                  INT NOT NULL, 
    [CreatedOn]                 DATETIME2 NOT NULL,
    [ModifiedOn]                DATETIME2 NOT NULL,
	[capHit]                    INT NOT NULL, 
	[capReleaseNetSavings]      INT NOT NULL,  
	[capReleasePenalty]         INT NOT NULL, 
	[contractBonus]             INT NOT NULL, 
	[contractLength]            INT NOT NULL, 
	[contractYearsLeft]         INT NOT NULL, 
	[contractSalary]            INT NOT NULL, 
	[desiredBonus]              INT NOT NULL, 
	[desiredLength]             INT NOT NULL, 
	[desiredSalary]             INT NOT NULL, 
	[reSignStatus]              INT NOT NULL, 
    CONSTRAINT [PK_tblPlayerContract] PRIMARY KEY ([playerId])
)
