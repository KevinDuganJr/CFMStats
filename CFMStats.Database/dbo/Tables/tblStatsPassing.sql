CREATE TABLE [dbo].[tblStatsPassing] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [leagueId]      INT          NOT NULL,
    [statId]        INT          NOT NULL,
    [rosterId]      INT          NOT NULL,
    [teamId]        INT          NOT NULL,
    [scheduleId]    INT          NOT NULL,
    [seasonIndex]   INT          NOT NULL,
    [weekIndex]     INT          NOT NULL,
    [stageIndex]    INT          NOT NULL,
    [lastUpdatedOn] DATETIME     NULL,
    [fullName]      VARCHAR (50) NOT NULL,
    [passAtt]       INT          NOT NULL,
    [passComp]      INT          NOT NULL,
    [passInts]      INT          NOT NULL,
    [passLongest]   INT          NOT NULL,
    [passPts]       INT          NOT NULL,
    [passSacks]     INT          NOT NULL,
    [passTDs]       INT          NOT NULL,
    [passYds]       INT          NOT NULL,
    [playerId]      INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblStatsPassing] PRIMARY KEY CLUSTERED ([ID] ASC)
);

