CREATE TABLE [dbo].[tblLeagueUserFavorite] (
    [leagueID]    INT           NOT NULL,
    [ownerUserID] VARCHAR (128) NOT NULL,
    [Id_new]      INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [Id] PRIMARY KEY CLUSTERED ([Id_new] ASC)
);

