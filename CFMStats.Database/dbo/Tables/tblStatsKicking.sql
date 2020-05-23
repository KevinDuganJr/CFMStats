﻿CREATE TABLE [dbo].[tblStatsKicking] (
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
    [fG50PlusAtt]   INT          NOT NULL,
    [fG50PlusMade]  INT          NOT NULL,
    [fGAtt]         INT          NOT NULL,
    [fGLongest]     INT          NOT NULL,
    [fGMade]        INT          NOT NULL,
    [fullName]      VARCHAR (50) NOT NULL,
    [kickPts]       INT          NOT NULL,
    [kickoffAtt]    INT          NOT NULL,
    [kickoffTBs]    INT          NOT NULL,
    [xPAtt]         INT          NOT NULL,
    [xPMade]        INT          NOT NULL,
    [playerId]      INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblStatsKicking] PRIMARY KEY CLUSTERED ([ID] ASC)
);
