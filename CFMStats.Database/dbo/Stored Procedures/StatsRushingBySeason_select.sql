-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2017 JULY 15
-- Description:	Select rushing stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsRushingBySeason_select] @stageIndex INT, 
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
			s.seasonindex, 
			t.displayName AS displayName,
			t.teamId, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(rush20PlusYds) AS '20Plus', 
			SUM(rushAtt) AS 'Attempt', 
			SUM(rushBrokenTackles) AS 'BrokenTackle', 
			SUM(rushFum) AS 'Fumble', 
			SUM(rushYds) AS 'Yards', 
			SUM(rushYdsAfterContact) AS 'YardsAfterContact', 
			SUM(rushTDs) AS 'Touchdown', 
			MAX(rushLongest) AS 'Longest'
			FROM 
				tblStatsRushing AS s
			
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
			GROUP BY s.seasonIndex,
				    s.playerid, 
				    s.teamId, 
				    t.teamId,
				    t.displayName, 
				    p.firstName, 
				    p.lastName 
				    
			ORDER BY 
				    s.seasonIndex ASC;
    END;