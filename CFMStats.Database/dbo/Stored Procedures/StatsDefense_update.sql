-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 13
-- Description:	insert or update madden defensive stats
-- =============================================
CREATE PROCEDURE [dbo].[StatsDefense_update] @leagueId        INT, 
                                             @weekIndex       INT, 
                                             @rosterId        INT, 
                                             @scheduleId      INT, 
                                             @seasonIndex     INT, 
                                             @stageIndex      INT, 
                                             @statId          INT, 
                                             @defCatchAllowed INT, 
                                             @defDeflections  INT, 
                                             @defForcedFum    INT, 
                                             @defFumRec       INT, 
                                             @defIntReturnYds INT, 
                                             @defInts         INT, 
                                             @defPts          INT, 
                                             @defSacks        VARCHAR(10), 
                                             @defSafeties     INT, 
                                             @defTDs          INT, 
                                             @defTotalTackles INT, 
                                             @fullName        VARCHAR(50), 
                                             @teamId          INT
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
            FROM tblStatsDefense
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblStatsDefense
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  defCatchAllowed = @defCatchAllowed, 
                  defDeflections = @defDeflections, 
                  defForcedFum = @defForcedFum, 
                  defFumRec = @defFumRec, 
                  defIntReturnYds = @defIntReturnYds, 
                  defInts = @defInts, 
                  defPts = @defPts, 
                  defSacks = @defSacks, 
                  defSafeties = @defSafeties, 
                  defTDs = @defTDs, 
                  defTotalTackles = @defTotalTackles, 
                  fullName = @fullName, 
                  stageIndex = @stageIndex, 
                  statId = @statId, 
                  teamId = @teamId, 
                  scheduleId = @scheduleId, 
                  playerId = @playerId
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId;
            ELSE
            INSERT INTO tblStatsDefense
            (leagueId, 
             playerId, 
             lastUpdatedOn, 
             defCatchAllowed, 
             defDeflections, 
             defForcedFum, 
             defFumRec, 
             defIntReturnYds, 
             defInts, 
             defPts, 
             defSacks, 
             defSafeties, 
             defTDs, 
             defTotalTackles, 
             fullName, 
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
             GETUTCDATE(), 
             @defCatchAllowed, 
             @defDeflections, 
             @defForcedFum, 
             @defFumRec, 
             @defIntReturnYds, 
             @defInts, 
             @defPts, 
             @defSacks, 
             @defSafeties, 
             @defTDs, 
             @defTotalTackles, 
             @fullName, 
             @rosterId, 
             @scheduleId, 
             @seasonIndex, 
             @stageIndex, 
             @statId, 
             @teamId, 
             @weekIndex
            );
    END;