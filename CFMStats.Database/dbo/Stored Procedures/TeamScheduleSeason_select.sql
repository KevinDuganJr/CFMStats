-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 NOV 07
-- Description:	Select schedule by team
-- =============================================

CREATE PROCEDURE [dbo].[TeamScheduleSeason_select] @stageIndex  INT, 
										@seasonIndex INT, 
										@leagueId    INT, 
										@teamId      INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
			s.scheduleId, 
			s.lastUpdatedOn, 
			s.leagueId, 
			s.stageIndex, 
			s.seasonIndex, 
			s.weekIndex, 
			ta.displayName AS AwayTeam, 
			s.awayScore, 
			s.awayTeamID, 
			th.displayName AS HomeTeam, 
			s.homeScore, 
			s.homeTeamId, 
			s.STATUS
			FROM 
				tblSchedule AS s
			JOIN tblTeamInfo AS ta
				ON ta.teamid = s.awayteamid
				   AND ta.leagueId = s.leagueId
			JOIN tblTeamInfo AS th
				ON th.teamid = s.hometeamid
				   AND th.leagueId = s.leagueId
			WHERE(s.homeTeamId = @teamId
				 OR s.awayTeamID = @teamId)
				AND s.leagueId = @leagueId
				AND s.seasonIndex = @seasonIndex
				AND s.stageIndex = @stageIndex
			--AND s.status > 1
			ORDER BY 
				    s.weekindex;
	   --status = 1 - game not played
	   --status = 2 - away team won
	   --status = 3 - home team won
	   --status = 4 - tie
    END;