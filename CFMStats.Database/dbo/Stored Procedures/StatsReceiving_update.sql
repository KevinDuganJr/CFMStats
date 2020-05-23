-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 10
-- Description:	insert or update madden receiving stats
-- =============================================
CREATE PROCEDURE [dbo].[StatsReceiving_update] @rosterId         INT, 
                                               @seasonIndex      INT, 
                                               @weekIndex        INT, 
                                               @fullName         VARCHAR(50), 
                                               @recCatchPct      VARCHAR(10), 
                                               @recCatches       INT, 
                                               @recDrops         INT, 
                                               @recLongest       INT, 
                                               @recPts           INT, 
                                               @recTDs           INT, 
                                               @recToPct         INT, 
                                               @recYacPerCatch   VARCHAR(10), 
                                               @recYds           INT, 
                                               @recYdsAfterCatch INT, 
                                               @recYdsPerCatch   VARCHAR(10), 
                                               @recYdsPerGame    VARCHAR(10), 
                                               @teamId           INT, 
                                               @stageIndex       INT, 
                                               @statId           INT, 
                                               @scheduleId       INT, 
                                               @leagueId         INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @playerId INT= (dbo.fn_GetActivePlayerId(@rosterId, @leagueId));
        SET @seasonIndex =
        (
            SELECT TOP 1 calendarYear
            FROM tblTeamStandings
            WHERE leagueid = @leagueId
            ORDER BY calendarYear DESC
        );
        IF EXISTS
        (
            SELECT *
            FROM tblStatsReceiving
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblStatsReceiving
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  recCatchPct = @recCatchPct, 
                  recCatches = @recCatches, 
                  recDrops = @recDrops, 
                  recLongest = @recLongest, 
                  recPts = @recPts, 
                  recTDs = @recTDs, 
                  recToPct = @recToPct, 
                  recYacPerCatch = @recYacPerCatch, 
                  recYds = @recYds, 
                  recYdsAfterCatch = @recYdsAfterCatch, 
                  recYdsPerCatch = @recYdsPerCatch, 
                  recYdsPerGame = @recYdsPerGame, 
                  scheduleId = @scheduleId, 
                  stageIndex = @stageIndex, 
                  statId = @statId, 
                  teamId = @teamId, 
                  fullname = @fullName, 
                  playerId = @playerId
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId;
            ELSE
            INSERT INTO tblStatsReceiving
            (leagueId, 
             playerId, 
             fullName, 
             lastUpdatedOn, 
             recCatchPct, 
             recCatches, 
             recDrops, 
             recLongest, 
             recPts, 
             recTDs, 
             recToPct, 
             recYacPerCatch, 
             recYds, 
             recYdsAfterCatch, 
             recYdsPerCatch, 
             recYdsPerGame, 
             rosterId, 
             scheduleId, 
             seasonIndex, 
             stageIndex, 
             statId, 
             teamId, 
             weekIndex
            )
            VALUES
            (@leagueId, 
             @playerId, 
             @fullName, 
             GETUTCDATE(), 
             @recCatchPct, 
             @recCatches, 
             @recDrops, 
             @recLongest, 
             @recPts, 
             @recTDs, 
             @recToPct, 
             @recYacPerCatch, 
             @recYds, 
             @recYdsAfterCatch, 
             @recYdsPerCatch, 
             @recYdsPerGame, 
             @rosterId, 
             @scheduleId, 
             @seasonIndex, 
             @stageIndex, 
             @statId, 
             @teamId, 
             @weekIndex
            );
    END;