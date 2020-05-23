-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2018 DEC 21
-- Description:	Rushing Stats (Game, Season, Career)
-- =============================================

CREATE PROCEDURE [records].[RushingStats] @leagueId   INT, 
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
				   rushYds AS Yards, 
				   rushBrokenTackles AS BrokenTackles, 
				   rushTDs AS Touchdowns, 
				   rushLongest AS Longest,
				   -- Player 
				   s.playerId, 
				   --IIF (p.firstname IS NULL, s.fullname, p.firstname + ' ' + p.lastName ) AS Player,  
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position as Position,
				   -- Team
				   t.teamId, 
				   p.teamName AS Team, 
				   schedule.awayTeamID AS AwayTeamId, 
				   (SELECT TOP 1 displayName FROM tblTeamInfo WHERE teamId = schedule.awayTeamId AND leagueid = @leagueId) AS AwayTeamName, 
				   schedule.hometeamId AS HomeTeamId, 
				   (SELECT TOP 1 displayName	FROM tblTeamInfo WHERE teamId = schedule.homeTeamID AND leagueid = @leagueId) AS HomeTeamName
				   FROM 
					   tblStatsRushing AS s
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
							 WHEN @orderBy = 'yards' THEN rushYds
							 WHEN @orderBy = 'brokentackles' THEN rushBrokenTackles
							 WHEN @orderBy = 'touchdowns' THEN rushTDs
							 WHEN @orderBy = 'longest' THEN rushLongest
						  END DESC;
	   END;
	   ELSE
		  BEGIN
			 SELECT TOP 10
			 -- Stats
				   SUM(CASE
						 WHEN @orderBy = 'yards' THEN rushYds
					  END) AS Yards, 
				   SUM(CASE
						 WHEN @orderBy = 'touchdowns' THEN rushTDs
					  END) AS Touchdowns, 
				   SUM(CASE
						 WHEN @orderBy = 'brokentackles' THEN rushBrokenTackles
					  END) AS BrokenTackles, 
				   MAX(CASE
						 WHEN @orderBy = 'longest' THEN rushLongest
					  END) AS Longest,
				   CASE
					  WHEN @duration = 'season' THEN s.seasonIndex
				   END AS Season,
				   -- Player	 
				   s.playerId, 
				   --IIF (p.firstname IS NULL, s.fullname, p.firstname + ' ' + p.lastName ) AS Player,  
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position as Position,
				   -- Team	
				 --  t.teamId, 
				   p.teamName AS Team,
				   -- Games Played
				   COUNT(s.weekIndex) AS Games
				   FROM 
					   tblStatsRushing AS s
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
						  p.teamName, 
						  p.firstname, 
						  p.lastName, 
						  p.position
						 
				   ORDER BY 
						  CASE
							 WHEN @orderBy = 'yards' THEN SUM(rushYds)
							 WHEN @orderBy = 'brokentackles' THEN SUM(rushBrokenTackles)
							 WHEN @orderBy = 'touchdowns' THEN SUM(rushTDs)
							 WHEN @orderBy = 'longest' THEN MAX(rushLongest)
						  END DESC;
	   END;
    END;  