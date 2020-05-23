-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2017 JULY 20
-- Description:	Select single week box score
-- =============================================

CREATE PROCEDURE [dbo].[BoxScore_select] @stageIndex  INT, 
								@seasonIndex INT, 
								@weekindex   INT, 
								@teamId      INT, 
								@leagueId    INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
			S.awayTeamID AS 'AwayID', 
			TA.displayName AS 'AwayTeam', 
			S.awayScore AS 'AwayScore', 
			S.homeTeamID AS 'HomeID', 
			TH.displayName AS 'HomeTeam', 
			S.homeScore AS 'HomeScore'
			FROM 
				tblSchedule AS S
			JOIN tblTeamInfo AS TH
				ON TH.teamID = S.homeTeamId
				   AND TH.leagueId = S.leagueId
			JOIN tblTeamInfo AS TA
				ON TA.teamID = S.awayTeamID
				   AND TA.leagueId = S.leagueId
			WHERE s.stageIndex = @stageIndex
				 AND S.seasonIndex = @seasonIndex
				 AND S.weekIndex = @weekIndex
				 AND (S.awayteamid = @teamId
					 OR S.hometeamid = @teamId)
				 AND S.leagueId = @leagueId;
    END;