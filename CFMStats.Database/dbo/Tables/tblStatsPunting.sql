CREATE TABLE [dbo].[tblStatsPunting] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [leagueId]      INT          NOT NULL,
    [statId]        INT          NOT NULL,
    [rosterId]      INT          NOT NULL,
    [teamId]        INT          NOT NULL,
    [scheduleId]    INT          NOT NULL,
    [seasonIndex]   INT          NOT NULL,
    [weekIndex]     INT          NOT NULL,
    [stageIndex]    INT          NOT NULL,
    [lastUpdatedOn] DATETIME     NOT NULL,
    [fullName]      VARCHAR (50) NOT NULL,
    [puntAtt]       INT          NOT NULL,
    [puntLongest]   INT          NOT NULL,
    [puntNetYds]    INT          NOT NULL,
    [puntTBs]       INT          NOT NULL,
    [puntYds]       INT          NOT NULL,
    [puntsBlocked]  INT          NOT NULL,
    [puntsIn20]     INT          NOT NULL,
    [playerId]      INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblStatsPunting] PRIMARY KEY CLUSTERED ([ID] ASC)
);

