-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 11
-- Description:	Select receiving stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsReceiving_select] @stageIndex  INT, 
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
	   DECLARE @endPlayerid INT;
	   DECLARE @endteamID INT;
	   -- set the WeekIndex
	   IF @weekIndex IS NULL
		  BEGIN
			 SET @weekIndex = 0;
			 SET @endWeekindex = 17;
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
			s.playerid, 
			p.firstName, 
			p.lastName, 
			p.position, 
			s.fullName, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(recCatches + recDrops) AS 'Targets', 
			SUM(recCatches) AS 'Receptions', 
			SUM(recDrops) AS 'Drops', 
			SUM(recTDs) AS 'Touchdowns', 
			SUM(recYds) AS 'Yards', 
			SUM(recYdsAfterCatch) AS 'YardsAfterCatch', 
			MAX(recLongest) AS 'Longest'
			FROM 
				tblStatsReceiving AS s
			JOIN tblTeamInfo AS t
				ON s.teamId = t.teamId
				AND s.leagueId = t.leagueId
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
				    s.playerid, 
				    p.firstName, 
				    p.lastName, 
				    p.position, 
				    s.fullName, 
				    t.displayName
			ORDER BY 
				    Yards DESC;
    END;