-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JULY 15
-- Description:	Select receiving stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsReceivingBySeason_select] @stageIndex INT, 
										    @playerId   INT, 
										    @leagueId   INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
			s.playerid, 
			p.firstName, 
			p.lastName, 
			s.seasonIndex, 
			t.displayName AS displayName, 
			t.teamId, 
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
			LEFT JOIN tblTeamInfo AS t
				ON t.teamId = s.teamId
				   AND t.leagueId = s.leagueId 
			LEFT JOIN tblPlayerProfile AS p
				ON p.playerId = s.playerId
				   AND p.leagueId = s.leagueId
			WHERE s.stageIndex = @stageIndex
				 AND s.playerId = @playerId
				 AND s.leagueId = @leagueId
				 AND s.weekIndex BETWEEN 0 AND 16
			GROUP BY 
				    s.playerid, 
				    t.teamId, 
				    t.displayName, 
				    p.firstName, 
				    p.lastName, 
				    s.seasonIndex
			ORDER BY 
				    s.seasonIndex ASC;
    END;