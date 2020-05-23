-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2018 DEC 21
-- Description:	Receiving Stats (Game, Season, Career)
-- =============================================

CREATE PROCEDURE [records].[ReceivingStats] @leagueId   INT, 
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
				   recYds AS Yards, 
				   recCatches AS Receptions, 
				   recDrops AS Drops, 
				   recTDs AS Touchdowns, 
				   recLongest AS Longest,
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
					   tblStatsReceiving AS s
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
							 WHEN @orderBy = 'yards' THEN recYds
							 WHEN @orderBy = 'receptions' THEN recCatches
							 WHEN @orderBy = 'drops' THEN recDrops
							 WHEN @orderBy = 'touchdowns' THEN recTDs
							 WHEN @orderBy = 'longest' THEN recLongest
						  END DESC;
	   END;
	   ELSE
		  BEGIN
			 SELECT TOP 10
			 -- Stats
				   SUM(CASE
						 WHEN @orderBy = 'yards' THEN recYds
					  END) AS Yards, 
				   SUM(CASE
						 WHEN @orderBy = 'receptions' THEN recCatches
					  END) AS Receptions, 
				   SUM(CASE
						 WHEN @orderBy = 'touchdowns' THEN recTDs
					  END) AS Touchdowns, 
				   SUM(CASE
						 WHEN @orderBy = 'drops' THEN recDrops
					  END) AS Drops, 
				   MAX(CASE
						 WHEN @orderBy = 'longest' THEN recLongest
					  END) AS Longest,
				   CASE
					  WHEN @duration = 'season' THEN s.seasonIndex
				   END AS Season,
				   -- Player	 
				   s.playerId, 
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position AS Position,
				   -- Team	
				 --  t.teamId, 
				   p.teamName AS Team,
				   -- Games Played
				   COUNT(s.weekIndex) AS Games
				   FROM 
					   tblStatsReceiving AS s
				   LEFT JOIN tblTeamInfo AS t
					   ON t.teamId = s.teamId
						 AND t.leagueId = s.leagueId						 
				   LEFT JOIN tblPlayerProfile AS p
					   ON p.playerId = s.playerId
						 AND p.leagueId = s.leagueId
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
							 WHEN @orderBy = 'yards' THEN SUM(recYds)
							 WHEN @orderBy = 'receptions' THEN SUM(recCatches)
							 WHEN @orderBy = 'drops' THEN SUM(recDrops)
							 WHEN @orderBy = 'touchdowns' THEN SUM(recTDs)
							 WHEN @orderBy = 'longest' THEN MAX(recLongest)
						  END DESC;
	   END;
    END;