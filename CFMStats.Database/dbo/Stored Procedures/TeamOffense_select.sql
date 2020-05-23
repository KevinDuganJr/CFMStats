-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 11
-- Description:	Select team offense
-- =============================================

CREATE PROCEDURE [dbo].[TeamOffense_select] @stageIndex  INT, 
								   @seasonIndex INT, 
								   @weekIndex   INT = NULL, 
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
			COUNT(*) AS Games, 
			SUM(offTotalYds) AS offTotalYds, 
			SUM(offPassYds) AS offPassYds, 
			SUM(offRushYds) AS offRushYds, 
			ISNULL((SELECT 
				   SUM(passsacks)
				   FROM 
					   tblstatspassing
				   WHERE leagueid = @leagueId
					    AND seasonindex = @seasonIndex
					    AND teamid = t.teamId), 0 ) AS offSacks, 
			SUM(offRushTDs) AS offRushTDs, 
			SUM(offPassTDs) AS offPassTDs, 
			SUM(off1stDowns) AS off1stDowns, 
			SUM(offPtsPerGame) AS offPoints
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
				    offTotalYds DESC;
    END;