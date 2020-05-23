-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 16
-- Description:	insert or update madden box score stats
-- =============================================

CREATE PROCEDURE [dbo].[StatsTeam_update] @weekIndex         INT, 
								 @stageIndex        INT, 
								 @statId            INT, 
								 @seasonIndex       INT, 
								 @seed              INT, 
								 @teamId            INT, 
								 @scheduleId        INT, 
								 @defForcedFum      INT, 
								 @defFumRec         INT, 
								 @defIntsRec        INT, 
								 @defPassYds        INT, 
								 @defPtsPerGame     INT, 
								 @defRedZoneFGs     INT, 
								 @defRedZonePct     DECIMAL(18, 6), 
								 @defRedZoneTDs     INT, 
								 @defRedZones       INT, 
								 @defRushYds        INT, 
								 @defSacks          INT, 
								 @defTotalYds       INT, 
								 @off1stDowns       INT, 
								 @off2PtAtt         INT, 
								 @off2PtConv        INT, 
								 @off2PtConvPct     DECIMAL(18, 6), 
								 @off3rdDownAtt     INT, 
								 @off3rdDownConv    INT, 
								 @off3rdDownConvPct DECIMAL(18, 6), 
								 @off4thDownAtt     INT, 
								 @off4thDownConv    INT, 
								 @off4thDownConvPct DECIMAL(18, 6), 
								 @offFumLost        INT, 
								 @offIntsLost       INT, 
								 @offPassTDs        INT, 
								 @offPassYds        INT, 
								 @offPtsPerGame     DECIMAL(18, 6), 
								 @offRedZoneFGs     INT, 
								 @offRedZonePct     DECIMAL(18, 6), 
								 @offRedZoneTDs     INT, 
								 @offRedZones       INT, 
								 @offRushTDs        INT, 
								 @offRushYds        INT, 
								 @offSacks          INT, 
								 @offTotalYds       INT, 
								 @offTotalYdsGained INT, 
								 @penalties         INT, 
								 @penaltyYds        INT, 
								 @tODiff            INT, 
								 @tOGiveaways       INT, 
								 @tOTakeaways       INT, 
								 @totalLosses       INT, 
								 @totalTies         INT, 
								 @totalWins         INT, 
								 @leagueId          INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from
	   -- interfering with SELECT statements.
	   SET NOCOUNT ON;
	   DECLARE @pointsFor INT;
	   DECLARE @pointsAgainst INT;

	   SET @seasonIndex = (SELECT TOP 1 calendarYear FROM tblTeamStandings WHERE leagueid = @leagueId ORDER BY calendarYear DESC);

	   SELECT 
			@pointsFor = IIF(@teamId = s.hometeamid, s.homeScore, s.awayScore), 
			@pointsAgainst = IIF(@teamId = s.homeTEamId, s.awayScore, s.homeScore)
			FROM 
				tblSchedule AS s
			WHERE s.leagueId = @leagueid
				 AND s.seasonIndex = @seasonIndex
				 AND s.scheduleId = @scheduleId
				 AND s.stageIndex = @stageIndex
				 AND (s.awayTeamID = @teamid
					 OR s.homeTeamId = @teamid);
	   IF EXISTS(SELECT 
					*
					FROM 
						tblStatsTeam
					WHERE teamId = @teamId
						 AND seasonIndex = @seasonIndex
						 AND stageIndex = @stageIndex
						 AND weekIndex = @weekIndex
						 AND leagueId = @leagueId)
		  BEGIN
			 UPDATE tblStatsTeam
				   SET 
					  lastUpdatedOn = GETUTCDATE(), 
					  statId = @statId, 
					  seed = @seed, 
					  scheduleId = @scheduleId, 
					  defForcedFum = @defForcedFum, 
					  defFumRec = @defFumRec, 
					  defIntsRec = @defIntsRec, 
					  defPassYds = @defPassYds, 
					  defPtsPerGame = @pointsAgainst, 
					  defRedZoneFGs = @defRedZoneFGs, 
					  defRedZonePct = @defRedZonePct, 
					  defRedZoneTDs = @defRedZoneTDs, 
					  defRedZones = @defRedZones, 
					  defRushYds = @defRushYds, 
					  defSacks = @defSacks, 
					  defTotalYds = @defTotalYds, 
					  off1stDowns = @off1stDowns, 
					  off2PtAtt = @off2PtAtt, 
					  off2PtConv = @off2PtConv, 
					  off2PtConvPct = @off2PtConvPct, 
					  off3rdDownAtt = @off3rdDownAtt, 
					  off3rdDownConv = @off3rdDownConv, 
					  off3rdDownConvPct = @off3rdDownConvPct, 
					  off4thDownAtt = @off4thDownAtt, 
					  off4thDownConv = @off4thDownConv, 
					  off4thDownConvPct = @off4thDownConvPct, 
					  offFumLost = @offFumLost, 
					  offIntsLost = @offIntsLost, 
					  offPassTDs = @offPassTDs, 
					  offPassYds = @offPassYds, 
					  offPtsPerGame = @pointsFor, 
					  offRedZoneFGs = @offRedZoneFGs, 
					  offRedZonePct = @offRedZonePct, 
					  offRedZoneTDs = @offRedZoneTDs, 
					  offRedZones = @offRedZones, 
					  offRushTDs = @offRushTDs, 
					  offRushYds = @offRushYds, 
					  offSacks = @offSacks, 
					  offTotalYds = @offTotalYds, 
					  offTotalYdsGained = @offTotalYdsGained, 
					  penalties = @penalties, 
					  penaltyYds = @penaltyYds, 
					  tODiff = @tODiff, 
					  tOGiveaways = @tOGiveaways, 
					  tOTakeaways = @tOTakeaways, 
					  totalLosses = @totalLosses, 
					  totalTies = @totalTies, 
					  totalWins = @totalWins
				   WHERE 
					    teamId = @teamId
					    AND seasonIndex = @seasonIndex
					    AND stageIndex = @stageIndex
					    AND weekIndex = @weekIndex
					    AND leagueId = @leagueId;
	   END;
	   ELSE
		  BEGIN
			 INSERT INTO tblStatsTeam
			 (
				   leagueId, 
				   lastUpdatedOn, 
				   weekIndex, 
				   stageIndex, 
				   statId, 
				   seasonIndex, 
				   seed, 
				   teamId, 
				   scheduleId, 
				   defForcedFum, 
				   defFumRec, 
				   defIntsRec, 
				   defPassYds, 
				   defPtsPerGame, 
				   defRedZoneFGs, 
				   defRedZonePct, 
				   defRedZoneTDs, 
				   defRedZones, 
				   defRushYds, 
				   defSacks, 
				   defTotalYds, 
				   off1stDowns, 
				   off2PtAtt, 
				   off2PtConv, 
				   off2PtConvPct, 
				   off3rdDownAtt, 
				   off3rdDownConv, 
				   off3rdDownConvPct, 
				   off4thDownAtt, 
				   off4thDownConv, 
				   off4thDownConvPct, 
				   offFumLost, 
				   offIntsLost, 
				   offPassTDs, 
				   offPassYds, 
				   offPtsPerGame, 
				   offRedZoneFGs, 
				   offRedZonePct, 
				   offRedZoneTDs, 
				   offRedZones, 
				   offRushTDs, 
				   offRushYds, 
				   offSacks, 
				   offTotalYds, 
				   offTotalYdsGained, 
				   penalties, 
				   penaltyYds, 
				   tODiff, 
				   tOGiveaways, 
				   tOTakeaways, 
				   totalLosses, 
				   totalTies, 
				   totalWins
			 )
			 VALUES
			 (
				   @leagueId, 
				   GETUTCDATE(), 
				   @weekIndex, 
				   @stageIndex, 
				   @statId, 
				   @seasonIndex, 
				   @seed, 
				   @teamId, 
				   @scheduleId, 
				   @defForcedFum, 
				   @defFumRec, 
				   @defIntsRec, 
				   @defPassYds, 
				   @pointsAgainst, 
				   @defRedZoneFGs, 
				   @defRedZonePct, 
				   @defRedZoneTDs, 
				   @defRedZones, 
				   @defRushYds, 
				   @defSacks, 
				   @defTotalYds, 
				   @off1stDowns, 
				   @off2PtAtt, 
				   @off2PtConv, 
				   @off2PtConvPct, 
				   @off3rdDownAtt, 
				   @off3rdDownConv, 
				   @off3rdDownConvPct, 
				   @off4thDownAtt, 
				   @off4thDownConv, 
				   @off4thDownConvPct, 
				   @offFumLost, 
				   @offIntsLost, 
				   @offPassTDs, 
				   @offPassYds, 
				   @pointsFor, 
				   @offRedZoneFGs, 
				   @offRedZonePct, 
				   @offRedZoneTDs, 
				   @offRedZones, 
				   @offRushTDs, 
				   @offRushYds, 
				   @offSacks, 
				   @offTotalYds, 
				   @offTotalYdsGained, 
				   @penalties, 
				   @penaltyYds, 
				   @tODiff, 
				   @tOGiveaways, 
				   @tOTakeaways, 
				   @totalLosses, 
				   @totalTies, 
				   @totalWins
			 );
	   END;
    END;