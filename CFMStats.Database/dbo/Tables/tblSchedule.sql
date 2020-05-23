CREATE TABLE [dbo].[tblSchedule] (
    [scheduleId]      INT      NOT NULL,
    [lastUpdatedOn]   DATETIME NOT NULL,
    [leagueId]        INT      NOT NULL,
    [seasonIndex]     INT      NOT NULL,
    [weekIndex]       INT      NOT NULL,
    [awayScore]       INT      NOT NULL,
    [awayTeamID]      INT      NOT NULL,
    [homeScore]       INT      NOT NULL,
    [homeTeamId]      INT      NOT NULL,
    [isGameOfTheWeek] BIT      NOT NULL,
    [status]          INT      NOT NULL,
    [stageIndex]      INT      NOT NULL
);

