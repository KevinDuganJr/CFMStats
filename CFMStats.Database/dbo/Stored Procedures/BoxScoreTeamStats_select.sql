-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2019 JAN 23
-- Description:	Get Box Score Team Stats
-- =============================================

CREATE PROCEDURE [dbo].[BoxScoreTeamStats_select] @stageIndex  INT, 
										@seasonIndex INT, 
										@weekIndex   INT, 
										@scheduleId  INT, 
										@leagueId    INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
			teamid,
			offTotalYdsGained, 
			offRushYds, 
			offPassYds, 
			off1stDowns, 
			offTotalYds, 
			tODiff, 
			off3rdDownAtt, 
			off3rdDownConv, 
			off4thDownAtt, 
			off4thDownConv, 
			offRedZoneFGs, 
			offRedZoneTDs, 
			offRedZones, 
			penalties,
			penaltyYds, 
			defSacks,
			offRushTDs,
			offPassTDs,
			defIntsRec,
			tOGiveaways,
			tOTakeaways
			
	   FROM
		  tblStatsTeam
	   
	   WHERE scheduleid = @scheduleId
			 AND leagueid = @leagueId
			 AND seasonindex = @seasonIndex
			 AND stageindex = @stageIndex;
    END;