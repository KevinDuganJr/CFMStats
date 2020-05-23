-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 15
-- Description:	insert or update madden punting stats
-- =============================================
CREATE PROCEDURE [dbo].[StatsPunting_update] @rosterId     INT, 
                                             @seasonIndex  INT, 
                                             @weekIndex    INT, 
                                             @statId       INT, 
                                             @teamId       INT, 
                                             @fullName     VARCHAR(50), 
                                             @puntAtt      INT, 
                                             @puntLongest  INT, 
                                             @puntNetYds   INT, 
                                             @puntTBs      INT, 
                                             @puntYds      INT, 
                                             @puntsBlocked INT, 
                                             @puntsIn20    INT, 
                                             @scheduleId   INT, 
                                             @stageIndex   INT, 
                                             @leagueId     INT
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
            FROM tblStatsPunting
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblStatsPunting
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  puntAtt = @puntAtt, 
                  puntLongest = @puntLongest, 
                  puntNetYds = @puntNetYds, 
                  puntTBs = @puntTBs, 
                  puntYds = @puntYds, 
                  puntsBlocked = @puntsBlocked, 
                  puntsIn20 = @puntsIn20, 
                  rosterId = @rosterId, 
                  scheduleId = @scheduleId, 
                  seasonIndex = @seasonIndex, 
                  stageIndex = @stageIndex, 
                  statId = @statId, 
                  teamId = @teamId, 
                  weekIndex = @weekIndex, 
                  playerId = @playerId
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId;
            ELSE
            INSERT INTO tblStatsPunting
            (leagueId, 
             playerId, 
             lastUpdatedOn, 
             fullName, 
             puntAtt, 
             puntLongest, 
             puntNetYds, 
             puntTBs, 
             puntYds, 
             puntsBlocked, 
             puntsIn20, 
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
             @fullName, 
             @puntAtt, 
             @puntLongest, 
             @puntNetYds, 
             @puntTBs, 
             @puntYds, 
             @puntsBlocked, 
             @puntsIn20, 
             @rosterId, 
             @scheduleId, 
             @seasonIndex, 
             @stageIndex, 
             @statId, 
             @teamId, 
             @weekIndex
            );
    END;