-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 14
-- Description:	insert or update madden kicking stats
-- =============================================
CREATE PROCEDURE [dbo].[StatsKicking_update] @rosterId     INT, 
                                             @seasonIndex  INT, 
                                             @weekIndex    INT, 
                                             @statId       INT, 
                                             @teamId       INT, 
                                             @fG50PlusAtt  INT, 
                                             @fG50PlusMade INT, 
                                             @fGAtt        INT, 
                                             @fGLongest    INT, 
                                             @fGMade       INT, 
                                             @fullName     VARCHAR(50), 
                                             @kickPts      INT, 
                                             @kickoffAtt   INT, 
                                             @kickoffTBs   INT, 
                                             @scheduleId   INT, 
                                             @stageIndex   INT, 
                                             @xPAtt        INT, 
                                             @xPMade       INT, 
                                             @leagueId     INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        --DECLARE @positionGroupID int = 99999999

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
            FROM tblStatsKicking
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblStatsKicking
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  fG50PlusAtt = @fG50PlusAtt, 
                  fG50PlusMade = @fG50PlusMade, 
                  fGAtt = @fGAtt, 
                  fGLongest = @fGLongest, 
                  fGMade = @fGMade, 
                  fullName = @fullName, 
                  kickPts = @kickPts, 
                  kickoffAtt = @kickoffAtt, 
                  kickoffTBs = @kickoffTBs, 
                  scheduleId = @scheduleId, 
                  stageIndex = @stageIndex, 
                  statId = @statId, 
                  teamId = @teamId, 
                  xPAtt = @xPAtt, 
                  xPMade = @xPMade, 
                  playerId = @playerId
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId;
            ELSE
            INSERT INTO tblStatsKicking
            (leagueId, 
             playerId, 
             lastUpdatedOn, 
             fG50PlusAtt, 
             fG50PlusMade, 
             fGAtt, 
             fGLongest, 
             fGMade, 
             fullName, 
             kickPts, 
             kickoffAtt, 
             kickoffTBs, 
             rosterId, 
             scheduleId, 
             seasonIndex, 
             stageIndex, 
             statId, 
             teamId, 
             weekIndex, 
             xPAtt, 
             xPMade
            )
            VALUES
            (@leagueId, 
             @playerId, 
             GETUTCDATE(), 
             @fG50PlusAtt, 
             @fG50PlusMade, 
             @fGAtt, 
             @fGLongest, 
             @fGMade, 
             @fullName, 
             @kickPts, 
             @kickoffAtt, 
             @kickoffTBs, 
             @rosterId, 
             @scheduleId, 
             @seasonIndex, 
             @stageIndex, 
             @statId, 
             @teamId, 
             @weekIndex, 
             @xPAtt, 
             @xPMade
            );
    END;