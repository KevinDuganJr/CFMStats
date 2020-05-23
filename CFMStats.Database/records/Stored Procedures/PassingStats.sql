-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2018 SEPT 01
-- Description:	Passing Stats (Game, Season, Career)
-- =============================================

CREATE PROCEDURE [records].[PassingStats] @leagueId   INT, 
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
				   passYds AS Yards, 
				   passTDs AS Touchdowns, 
				   passInts AS Interceptions, 
				   passLongest AS Longest,
				   -- Player 
				   s.playerId, 
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position as Position,
				   -- Team
				   t.teamId, 
				   t.displayName AS Team, 
				   schedule.awayTeamID AS AwayTeamId, 
				   (SELECT TOP 1 displayName FROM tblTeamInfo WHERE teamId = schedule.awayTeamId AND leagueid = @leagueId) AS AwayTeamName, 
				   schedule.hometeamId AS HomeTeamId, 
				   (SELECT TOP 1 displayName	FROM tblTeamInfo WHERE teamId = schedule.homeTeamID AND leagueid = @leagueId) AS HomeTeamName
				   
				   FROM 
					   tblStatsPassing AS s
				   JOIN tblTeamInfo AS t
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
							 WHEN @orderBy = 'yards' THEN passYds
							 WHEN @orderBy = 'interceptions' THEN passInts
							 WHEN @orderBy = 'touchdowns' THEN passTDs
							 WHEN @orderBy = 'longest' THEN passLongest
						  END DESC;
	   END;
	   ELSE
		  BEGIN
			 SELECT TOP 10
			 -- Stats
				   SUM(CASE
						 WHEN @orderBy = 'yards' THEN passYds
					  END) AS Yards, 
				   SUM(CASE
						 WHEN @orderBy = 'touchdowns' THEN passTDs
					  END) AS Touchdowns, 
				   SUM(CASE
						 WHEN @orderBy = 'interceptions' THEN passInts
					  END) AS Interceptions, 
				   MAX(CASE
						 WHEN @orderBy = 'longest' THEN passLongest
					  END) AS Longest,
				   CASE
					  WHEN @duration = 'season' THEN s.seasonIndex
				   END AS Season,
				   -- Player	 
				   s.playerId, 
				   p.firstname + ' ' + p.lastName AS Player,
				   p.position as Position,
				   -- Team	
				 --  t.teamId, 
				   p.teamName AS Team,
				   -- Games Played
				   COUNT(s.weekIndex) AS Games
				   FROM 
					   tblStatsPassing AS s
				  LEFT JOIN tblTeamInfo AS t
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
						  p.teamName, 
						  p.firstname, 
						  p.lastName, 
						  p.position
				   ORDER BY 
						  CASE
							 WHEN @orderBy = 'yards' THEN SUM(passYds)
							 WHEN @orderBy = 'interceptions' THEN SUM(passInts)
							 WHEN @orderBy = 'touchdowns' THEN SUM(passTDs)
							 WHEN @orderBy = 'longest' THEN MAX(passLongest)
						  END DESC;
	   END;
    END;