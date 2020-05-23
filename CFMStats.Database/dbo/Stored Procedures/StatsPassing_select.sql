-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 14
-- Description:	Select passing stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsPassing_select] @stageIndex  INT, 
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
			 SET @endWeekindex = 16;
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
			s.fullName, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(passAtt) AS 'Attempt', 
			SUM(passComp) AS 'Completion', 
			SUM(passInts) AS 'Interception', 
			SUM(passYds) AS 'Yards', 
			SUM(passSacks) AS 'Sack', 
			SUM(passTDs) AS 'Touchdown', 
			MAX(passLongest) AS 'Longest'
			FROM 
				tblStatsPassing AS s
			JOIN tblTeamInfo AS t
				ON s.teamId = t.teamId
			LEFT JOIN tblPlayerProfile AS p
				ON p.playerId = s.playerId
				   AND p.leagueId = s.leagueId
			WHERE s.stageIndex = @stageIndex
				 AND passAtt > 0
				 AND s.seasonIndex = @seasonIndex
				 AND s.weekIndex BETWEEN @weekindex AND @endWeekindex
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