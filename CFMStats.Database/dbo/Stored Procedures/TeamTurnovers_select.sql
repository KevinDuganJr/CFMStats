-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 11
-- Description:	Select team turnovers
-- =============================================
CREATE PROCEDURE [dbo].[TeamTurnovers_select] @stageIndex  INT, 
                                              @seasonIndex INT, 
                                              @weekindex   INT = NULL, 
                                              @leagueId    INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @endWeekindex INT;

        -- set the WeekIndex
        IF @weekIndex IS NULL
            BEGIN
                SET @weekIndex = 0;
                SET @endWeekindex = 16;
        END;
            ELSE
            SET @endWeekindex = @weekIndex;
        SELECT t.displayName AS teamName, 
               t.divName, 
               COUNT(*) AS Games, 
               SUM(s.tODiff) AS tODiff, 
               SUM(s.tOGiveaways) AS tOGiveaways, 
               SUM(s.offIntsLost) AS offIntsLost, 
               SUM(s.offFumLost) AS offFumLost, 
               SUM(s.tOTakeaways) AS tOTakeaways, 
               SUM(s.defIntsRec) AS defIntsRec, 
               SUM(s.defFumRec) AS defFumRec
        FROM tblStatsTeam s
             LEFT JOIN tblTeamInfo t ON t.teamId = s.teamId
                                        AND t.leagueId = s.leagueId
        WHERE s.stageIndex = @stageIndex
              AND s.seasonIndex = @seasonIndex
              AND s.weekIndex BETWEEN @weekIndex AND @endWeekindex
              AND s.leagueId = @leagueId
        GROUP BY t.displayName, 
                 t.divName
        ORDER BY SUM(s.tODiff) DESC;
    END;