-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JULY 15
-- Description:	Select defense stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsDefenseBySeason_select] @stageIndex INT, 
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
			t.displayName AS 'displayName', 
			t.teamId, 
			COUNT(s.weekIndex) AS 'Games', 
			SUM(defCatchAllowed) AS 'CatchesAllowed', 
			SUM(defDeflections) AS 'Deflections', 
			SUM(defForcedFum) AS 'ForcedFumble', 
			SUM(defFumRec) AS 'FumbleRecovery', 
			SUM(defIntReturnYds) AS 'INTReturnYards', 
			SUM(defInts) AS 'Interceptions',
			--SUM(defSacks) as 'Sacks',
			SUM(CONVERT(DECIMAL(10, 2), defSacks)) AS 'Sacks', 
			SUM(defSafeties) AS 'Safety', 
			SUM(defTDs) AS 'Touchdowns', 
			SUM(defTotalTackles) AS 'Tackles'
			FROM 
				tblStatsDefense AS s
			JOIN tblTeamInfo AS t
				ON t.teamid = s.teamid
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