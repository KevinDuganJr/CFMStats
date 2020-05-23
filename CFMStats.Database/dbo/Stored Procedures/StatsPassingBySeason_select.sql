-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JULY 14
-- Description:	Select passing stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsPassingBySeason_select] @stageIndex INT, 
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
			SUM(passAtt) AS 'Attempt', 
			SUM(passComp) AS 'Completion', 
			SUM(passInts) AS 'Interception', 
			SUM(passYds) AS 'Yards', 
			SUM(passSacks) AS 'Sack', 
			SUM(passTDs) AS 'Touchdown', 
			MAX(passLongest) AS 'Longest'
			FROM 
				tblStatsPassing AS s
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