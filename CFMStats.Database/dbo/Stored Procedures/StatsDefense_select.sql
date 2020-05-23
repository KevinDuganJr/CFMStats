-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 13
-- Description:	Select defense stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsDefense_select] @stageIndex  INT, 
									@seasonIndex INT, 
									@weekindex   INT = NULL, 
									@teamId      INT = NULL, 
									@top         INT = NULL, 
									@leagueId    INT
AS
       BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   DECLARE @endWeekindex INT;
	   DECLARE @endteamID INT;
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
	   -- set the TeamID
	   IF @teamID IS NULL
		  BEGIN
			 SET @teamID = 0;
			 SET @endteamID = (SELECT MAX(teamid) FROM tblTeamInfo);
	   END;
	   ELSE
		  BEGIN
			 SET @endteamID = @teamID;
	   END;
	   -- set the Top
	   IF @top IS NULL
		  BEGIN
			 SET @top = 5000;
	   END;
	   SELECT TOP (@TOP) 
			t.displayName AS teamName, 
			s.playerId, 
			p.firstName, 
			p.lastName, 
			p.position, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(defCatchAllowed) AS 'CatchesAllowed', 
			SUM(defDeflections) AS 'Deflections', 
			SUM(defForcedFum) AS 'ForcedFumble', 
			SUM(defFumRec) AS 'FumbleRecovery', 
			SUM(defIntReturnYds) AS 'INTReturnYards', 
			SUM(defInts) AS 'Interceptions',
			--SUM(defSacks) as 'Sacks',
			SUM(CONVERT(DECIMAL(10, 2), defSacks)) AS 'Sacks', 
			SUM(defSafeties) AS 'Safety', 
			SUM(defTDs) AS 'Touchdowns', 
			SUM(defTotalTackles) AS 'Tackles'
			FROM 
				tblStatsDefense AS s
			JOIN tblTeamInfo AS t
				ON s.teamId = t.teamId
			LEFT JOIN tblPlayerProfile AS p
				ON p.playerId = s.playerId
				   AND p.leagueId = s.leagueId
			WHERE s.stageIndex = @stageIndex
				 AND s.seasonIndex = @seasonIndex
				 AND s.weekIndex BETWEEN @weekIndex AND @endWeekindex
				 AND s.teamId BETWEEN @teamId AND @endTeamId
				 AND s.leagueId = @leagueId
				 AND s.playerId > 0
			GROUP BY 
				    s.playerId, 
				    p.firstName, 
				    p.lastName, 
				    p.position, 
				    t.displayName
			ORDER BY 
				    Sacks DESC;
    END;