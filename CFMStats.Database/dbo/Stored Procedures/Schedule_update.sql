-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 16
-- Description:	insert or update madden schedule
-- =============================================
CREATE PROCEDURE [dbo].[Schedule_update] @scheduleId      INT, 
                                         @seasonIndex     INT, 
                                         @weekIndex       INT, 
                                         @awayScore       INT, 
                                         @awayTeamID      INT, 
                                         @homeScore       INT, 
                                         @homeTeamId      INT, 
                                         @isGameOfTheWeek BIT, 
                                         @status          INT, 
                                         @stageIndex      INT, 
                                         @leagueId        INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
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
            FROM tblSchedule
            WHERE scheduleId = @scheduleId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblSchedule
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  awayScore = @awayScore, 
                  awayTeamID = @awayTeamID, 
                  homeScore = @homeScore, 
                  homeTeamId = @homeTeamId, 
                  isGameOfTheWeek = @isGameOfTheWeek, 
                  [status] = @status, 
                  stageIndex = @stageIndex
            WHERE scheduleId = @scheduleId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId;
            ELSE
            INSERT INTO tblSchedule
            (leagueId, 
             scheduleId, 
             lastUpdatedOn, 
             seasonIndex, 
             weekIndex, 
             awayScore, 
             awayTeamID, 
             homeScore, 
             homeTeamId, 
             isGameOfTheWeek, 
             [status], 
             stageIndex
            )
            VALUES
            (@leagueId, 
             @scheduleId, 
             GETUTCDATE(), 
             @seasonIndex, 
             @weekIndex, 
             @awayScore, 
             @awayTeamID, 
             @homeScore, 
             @homeTeamId, 
             @isGameOfTheWeek, 
             @status, 
             @stageIndex
            );
    END;