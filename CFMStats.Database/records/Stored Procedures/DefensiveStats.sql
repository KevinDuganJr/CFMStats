-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2018 DEC 21
-- Description:	Defensive Stats (Game, Season, Career)
-- =============================================

CREATE PROCEDURE [records].[DefensiveStats] @leagueId   INT, 
								   @stageIndex INT, 
								   @orderBy    VARCHAR(20), 
								   @duration   VARCHAR(20)
AS
  BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   IF @duration = 'game'
		  BEGIN
			 SELECT TOP 10
			 -- Season and Week
				   s.seasonIndex AS Season, 
				   s.weekIndex + 1 AS Week,
				   -- Stats
				   defSacks AS Sacks, 
				   defInts AS interceptions, 
				   defForcedFum AS Fumbles, 
				   defTDs AS Touchdowns, 
				   defTotalTackles AS Tackles,
				   -- Player 
				   s.playerId, 
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position AS Position,
				   -- Team
				   t.teamId, 
				   t.displayName AS Team,
				   schedule.awayTeamID AS AwayTeamId, 
				   (SELECT TOP 1 displayName FROM tblTeamInfo WHERE teamId = schedule.awayTeamId AND leagueid = @leagueId) AS AwayTeamName, 
				   schedule.hometeamId AS HomeTeamId, 
				   (SELECT TOP 1 displayName	FROM tblTeamInfo WHERE teamId = schedule.homeTeamID AND leagueid = @leagueId) AS HomeTeamName

				   FROM 
					   tblStatsDefense AS s
				   LEFT JOIN tblTeamInfo AS t
					   ON t.teamId = s.teamId
						 AND t.leagueId = s.leagueId
				   LEFT JOIN tblSchedule AS schedule
					   ON schedule.scheduleId = s.scheduleId
						 AND schedule.leagueId = @leagueId
						 AND schedule.seasonIndex = s.seasonIndex
				   LEFT JOIN tblPlayerProfile AS p
					   ON p.playerId = s.playerId
						 AND p.leagueId = s.leagueId
				   WHERE s.stageIndex = @stageIndex
					    AND s.leagueId = @leagueId
					    AND s.weekIndex BETWEEN 0 AND 16
					    AND s.playerId > 0
				   ORDER BY 
						  CASE
							 WHEN @orderBy = 'sacks' THEN(CONVERT(DECIMAL(10, 2), defSacks))
							 WHEN @orderBy = 'interceptions' THEN defInts
							 WHEN @orderBy = 'fumbles' THEN defForcedFum
							 WHEN @orderBy = 'touchdowns' THEN defTDs
							 WHEN @orderBy = 'tackles' THEN defTotalTackles
						  END DESC;
	   END;
	   ELSE
		  BEGIN
			 SELECT TOP 10
			 -- Stats
				   SUM(CASE
						 WHEN @orderBy = 'sacks' THEN(CONVERT(DECIMAL(10, 2), defSacks))
					  END) AS Sacks, 
				   SUM(CASE
						 WHEN @orderBy = 'interceptions' THEN defInts
					  END) AS Interceptions, 
				   SUM(CASE
						 WHEN @orderBy = 'touchdowns' THEN defTDs
					  END) AS Touchdowns, 
				   SUM(CASE
						 WHEN @orderBy = 'fumbles' THEN defForcedFum
					  END) AS Fumbles, 
				   SUM(CASE
						 WHEN @orderBy = 'tackles' THEN defTotalTackles
					  END) AS Tackles,
				   CASE
					  WHEN @duration = 'season' THEN s.seasonIndex
				   END AS Season,
				   -- Player	 
				   s.playerId, 
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position AS Position,
				   -- Team	
				  -- t.teamId, 
				   p.teamName AS Team,
				   -- Games Played
				   COUNT(s.weekIndex) AS Games
				   FROM 
					   tblStatsDefense AS s
				   JOIN tblTeamInfo AS t
					   ON t.teamid = s.teamid
						 AND t.leagueId = s.leagueId
				   LEFT JOIN tblPlayerProfile AS p
					   ON p.playerId = s.playerId
						-- AND p.leagueId = s.leagueId
				   WHERE s.stageIndex = @stageIndex
					    AND s.leagueId = @leagueId
					    AND s.weekIndex BETWEEN 0 AND 16
					    AND s.playerId > 0
				   GROUP BY 
						  CASE
							 WHEN @duration = 'season' THEN s.seasonIndex
						  END, 
						  s.playerId, 
						  p.firstname, 
						  p.lastName, 
						  p.position,
						  p.teamName
				   ORDER BY 
						  CASE
							 WHEN @orderBy = 'sacks' THEN SUM(CONVERT(DECIMAL(10, 2), defSacks))
							 WHEN @orderBy = 'interceptions' THEN SUM(defInts)
							 WHEN @orderBy = 'fumbles' THEN SUM(defForcedFum)
							 WHEN @orderBy = 'touchdowns' THEN SUM(defTDs)
							 WHEN @orderBy = 'tackles' THEN SUM(defTotalTackles)
						  END DESC;
	   END;
    END;    