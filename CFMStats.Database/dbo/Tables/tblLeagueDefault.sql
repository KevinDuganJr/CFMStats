CREATE TABLE [dbo].[tblLeagueDefault] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [leagueID] INT            NOT NULL,
    [userID]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_tblLeagueDefault] PRIMARY KEY CLUSTERED ([ID] ASC)
);

