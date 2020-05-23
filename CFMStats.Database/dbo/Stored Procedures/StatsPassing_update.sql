-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 14
-- Description:	insert or update madden passing stats
-- =============================================
CREATE PROCEDURE [dbo].[StatsPassing_update] @rosterId    INT, 
                                             @seasonIndex INT, 
                                             @weekIndex   INT, 
                                             @fullName    VARCHAR(50), 
                                             @passAtt     INT, 
                                             @passComp    INT, 
                                             @passInts    INT, 
                                             @passLongest INT, 
                                             @passPts     INT, 
                                             @passSacks   INT, 
                                             @passTDs     INT, 
                                             @passYds     INT, 
                                             @scheduleId  INT, 
                                             @stageIndex  INT, 
                                             @statId      INT, 
                                             @teamId      INT, 
                                             @leagueId    INT
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
            FROM tblStatsPassing
            WHERE rosterId = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblStatsPassing
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  fullName = @fullName, 
                  passAtt = @passAtt, 
                  passComp = @passComp, 
                  passInts = @passInts, 
                  passLongest = @passLongest, 
                  passPts = @passPts, 
                  passSacks = @passSacks, 
                  passTDs = @passTDs, 
                  passYds = @passYds, 
                  rosterId = @rosterId, 
                  scheduleId = @scheduleId, 
                  stageIndex = @stageIndex, 
                  statId = @statId, 
                  teamId = @teamId, 
                  playerId = @playerId
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId;
            ELSE
            INSERT INTO tblStatsPassing
            (leagueId, 
             playerId, 
             lastUpdatedOn, 
             fullName, 
             passAtt, 
             passComp, 
             passInts, 
             passLongest, 
             passPts, 
             passSacks, 
             passTDs, 
             passYds, 
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
             @passAtt, 
             @passComp, 
             @passInts, 
             @passLongest, 
             @passPts, 
             @passSacks, 
             @passTDs, 
             @passYds, 
             @rosterId, 
             @scheduleId, 
             @seasonIndex, 
             @stageIndex, 
             @statId, 
             @teamId, 
             @weekIndex
            );
    END;