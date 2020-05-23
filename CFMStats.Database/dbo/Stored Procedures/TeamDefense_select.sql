-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 11
-- Description:	Select team defense
-- =============================================

CREATE PROCEDURE [dbo].[TeamDefense_select] @stageIndex  INT, 
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
		  BEGIN
			 SET @endWeekindex = @weekIndex;
	   END;
	   SELECT 
			t.displayName AS teamName, 
			t.divName, 
			COUNT(s.teamid) AS Games, 
			SUM(defTotalYds) AS defTotalYds, 
			SUM(defPassYds) AS defPassYds, 
			SUM(defRushYds) AS defRushYds, 
			SUM(s.defSacks) AS defSacks, 
			SUM(defIntsRec) AS defIntsRec, 
			SUM(defPtsPerGame) AS defPoints, 
			(SELECT 
				   SUM(a.defForcedFum)
				   FROM 
					   tblStatsDefense AS a
				   WHERE a.teamId = t.teamid
					    AND a.leagueId = @leagueId
					    AND a.stageIndex = @stageIndex
					    AND a.seasonIndex = @seasonIndex
					    AND a.weekIndex BETWEEN @weekIndex AND @endWeekindex) AS defForcedFum, 
			SUM(s.defFumRec) AS defFumRec, 
			(SELECT 
				   SUM(a.defSafeties)
				   FROM 
					   tblStatsDefense AS a
				   WHERE a.teamId = t.teamid
					    AND a.leagueId = @leagueId
					    AND a.stageIndex = @stageIndex
					    AND a.seasonIndex = @seasonIndex
					    AND a.weekIndex BETWEEN @weekIndex AND @endWeekindex) AS defSafeties, 
			(SELECT 
				   SUM(a.defTDs)
				   FROM 
					   tblStatsDefense AS a
				   WHERE a.teamId = t.teamid
					    AND a.leagueId = @leagueId
					    AND a.stageIndex = @stageIndex
					    AND a.seasonIndex = @seasonIndex
					    AND a.weekIndex BETWEEN @weekIndex AND @endWeekindex) AS defTDs, 
			(SELECT 
				   SUM(a.defDeflections)
				   FROM 
					   tblStatsDefense AS a
				   WHERE a.teamId = t.teamid
					    AND a.leagueId = @leagueId
					    AND a.stageIndex = @stageIndex
					    AND a.seasonIndex = @seasonIndex
					    AND a.weekIndex BETWEEN @weekIndex AND @endWeekindex) AS defDeflections, 
			(SELECT 
				   SUM(a.defTotalTackles)
				   FROM 
					   tblStatsDefense AS a
				   WHERE a.teamId = t.teamid
					    AND a.leagueId = @leagueId
					    AND a.stageIndex = @stageIndex
					    AND a.seasonIndex = @seasonIndex
					    AND a.weekIndex BETWEEN @weekIndex AND @endWeekindex) AS defTotalTackles
			FROM 
				tblStatsTeam AS s
			LEFT JOIN tblTeamInfo AS t
				ON t.teamId = s.teamId
				   AND t.leagueId = s.leagueId
			WHERE s.stageIndex = @stageIndex
				 AND s.seasonIndex = @seasonIndex
				 AND s.weekIndex BETWEEN @weekIndex AND @endWeekindex
				 AND s.leagueId = @leagueId
			GROUP BY 
				    t.displayName, 
				    t.divName, 
				    t.teamId
			ORDER BY 
				    defTotalYds ASC;
    END;