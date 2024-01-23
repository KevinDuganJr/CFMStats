-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2017 JUNE 15
-- Description:	Select punting stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsPunting_select] @stageIndex  INT, 
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
			 SET @top = 5000;
	   END;
	   SELECT TOP (@TOP) 
			t.displayName AS teamName, 
			s.playerid, 
			p.firstName, 
			p.lastName, 
			p.position, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(puntAtt) AS 'Punt', 
			SUM(puntYds) AS 'Yards', 
			SUM(puntNetYds) AS 'NetYards', 
			SUM(puntsBlocked) AS 'Blocked', 
			SUM(puntsIn20) AS 'In20', 
			MAX(puntLongest) AS 'Longest'
			FROM 
				tblStatsPunting AS s
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
				    Punt DESC;
    END;