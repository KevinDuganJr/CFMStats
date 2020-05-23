-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2019 JAN 24
-- Description:	Get Box Score Stats - Defense
-- =============================================

CREATE PROCEDURE [dbo].[BoxScoreStatsDefense_select] @stageIndex  INT, 
										  @seasonIndex INT, 
										  @scheduleId  INT, 
										  @leagueId    INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
	   t.displayName,
			s.playerId, 
			s.leagueId, 
			s.statId, 
			s.rosterId, 
			s.teamId, 
			s.scheduleId, 
			s.seasonIndex, 
			s.weekIndex, 
			s.stageIndex, 
			s.lastUpdatedOn, 
			s.fullName, 
			s.defCatchAllowed, 
			s.defDeflections, 
			s.defForcedFum, 
			s.defFumRec, 
			s.defIntReturnYds, 
			s.defInts, 
			s.defPts, 
			s.defSacks, 
			s.defSafeties, 
			s.defTDs, 
			s.defTotalTackles
			FROM 
				tblStatsDefense AS s
			JOIN tblTeamInfo AS t
				ON s.teamId = t.teamId
				AND s.leagueId = t.leagueId

			WHERE s.scheduleid = @scheduleId
				 AND s.leagueid = @leagueId
				 AND s.seasonindex = @seasonIndex
				 AND s.stageindex = @stageIndex;
    END;