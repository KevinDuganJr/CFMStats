-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 14
-- Description:	Select kicking stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsKicking_select] @stageIndex  INT, 
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
	   DECLARE @theTop INT;
	   DECLARE @endteamID INT;
	   IF @weekIndex IS NULL
		  BEGIN
			 SET @weekIndex = 0;
			 SET @endWeekindex = 17;
	   END;
	   ELSE
		  BEGIN
			 SET @endWeekindex = @weekIndex;
	   END;
	   IF @teamID IS NULL
		  BEGIN
			 SET @teamID = 0;
			 SET @endteamID = (SELECT MAX(teamid) FROM tblTeamInfo);
	   END;
	   ELSE
		  BEGIN
			 SET @endteamID = @teamID;
	   END;
	   IF @top IS NULL
		  BEGIN
			 SET @top = 500;
	   END;
	   SELECT TOP (@TOP) 
			t.displayName AS teamName, 
			s.playerid, 
			p.firstName, 
			p.lastName, 
			p.position, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(xPAtt) AS 'XPAttempt', 
			SUM(xPMade) AS 'XPMade', 
			SUM(fGAtt) AS 'FGAtt', 
			SUM(fGMade) AS 'FGMade', 
			MAX(fGLongest) AS 'Longest', 
			SUM(fG50PlusAtt) AS 'FG50PlusAttempt', 
			SUM(fG50PlusMade) AS 'FG50PlusMade', 
			SUM(kickoffAtt) AS 'Kickoff', 
			SUM(kickoffTBs) AS 'Touchback'
			FROM 
				tblStatsKicking AS s
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
				    t.displayName
			ORDER BY 
				    XPMade DESC;
    END;