-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 18
-- Description:	Select league teams standings
-- =============================================

CREATE PROCEDURE [dbo].[TeamStandings_select] @stageIndex  INT, 
                                              @seasonIndex INT, 
                                              @leagueId    INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        SELECT s.teamId, 
               s.lastUpdatedOn, 
               i.injuryCount, 
               teamName, 
               teamOvr, 
               calendarYear, 
               weekIndex, 
               seasonIndex, 
        (
            SELECT SUM(offPtsPerGame)
            FROM tblStatsTeam
            WHERE teamId = i.teamid
                  AND stageIndex = @stageIndex
                  AND seasonIndex = @seasonIndex
                  AND leagueId = @leagueId
                  AND weekIndex BETWEEN 0 AND 16
        ) AS PointsFor, 
        (
            SELECT SUM(defPtsPerGame)
            FROM tblStatsTeam
            WHERE teamId = i.teamid
                  AND stageIndex = @stageIndex
                  AND seasonIndex = @seasonIndex
                  AND leagueId = @leagueId
                  AND weekIndex BETWEEN 0 AND 16
        ) AS PointsAgainst, 
        (
            SELECT(SUM(Age) * 1.0 / COUNT(*))
            FROM tblPlayerProfile
            WHERE isOnPracticeSquad = 0
                  AND leagueId = @leagueId
                  AND teamId = i.teamId
        ) AS AverageAge, 
        (
            SELECT(SUM(pr.playerBestOvr) * 1.0 / COUNT(*))
            FROM tblPlayerProfile pp
            JOIN tblPlayerRatings pr
            ON pp.playerId = pr.playerId
            AND pp.leagueId = pr.leagueId
            WHERE isOnPracticeSquad = 0
                  AND pp.leagueId = @leagueId
                  AND pp.teamId = i.teamId
        ) AS AverageOvr, 
        (
            SELECT(SUM(speedRating) * 1.0 / COUNT(*))
            FROM tblPlayerProfile pp
                 JOIN tblPlayerRatings pr ON pp.playerId = pr.playerId
            WHERE isOnPracticeSquad = 0
                  AND pp.leagueId = @leagueId
                  AND pp.teamId = i.teamId
        ) AS AverageSpd, 
               capRoom, 
               capSpent, 
               conferenceId, 
               conferenceName, 
               defPassYds, 
               defPassYdsRank, 
               defRushYds, 
               defRushYdsRank, 
               defTotalYds, 
               defTotalYdsRank, 
               divisionId, 
               divisionName, 
               netPts, 
               offPassYds, 
               offRushYds, 
               offTotalYds, 
               playoffStatus, 
               prevRank, 
               ptsAgainst, 
               ptsFor, 
               [rank], 
               seed, 
               stageIndex, 
               tODiff, 
               totalLosses, 
               totalTies, 
               totalWins, 
               winLossStreak, 
               winPct
        FROM tblTeamStandings AS S
             LEFT JOIN tblTeamInfo AS I ON S.teamId = I.teamId
                                           AND S.leagueId = I.leagueId
        WHERE stageIndex = @stageIndex
              AND seasonIndex = @seasonIndex
              AND S.leagueId = @leagueId
        ORDER BY s.rank ASC, 
                 s.teamOvr DESC, 
                 s.teamName ASC;
    END;