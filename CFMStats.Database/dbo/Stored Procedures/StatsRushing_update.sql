-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 15
-- Description:	insert or update madden rushing stats
-- =============================================
CREATE PROCEDURE [dbo].[StatsRushing_update] @rosterId            INT, 
                                             @seasonIndex         INT, 
                                             @weekIndex           INT, 
                                             @fullName            VARCHAR(50), 
                                             @rush20PlusYds       INT, 
                                             @rushAtt             INT, 
                                             @rushBrokenTackles   INT, 
                                             @rushFum             INT, 
                                             @rushLongest         INT, 
                                             @rushPts             INT, 
                                             @rushTDs             INT, 
                                             @rushYds             INT, 
                                             @rushYdsAfterContact INT, 
                                             @scheduleId          INT, 
                                             @stageIndex          INT, 
                                             @statId              INT, 
                                             @teamId              INT, 
                                             @leagueId            INT
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
            FROM tblStatsRushing
            WHERE rosterid = @rosterId
                  AND seasonIndex = @seasonIndex
                  AND stageIndex = @stageIndex
                  AND weekIndex = @weekIndex
                  AND leagueId = @leagueId
        )
            UPDATE tblStatsRushing
              SET 
                  lastUpdatedOn = GETUTCDATE(), 
                  fullName = @fullName, 
                  rosterId = @rosterId, 
                  rush20PlusYds = @rush20PlusYds, 
                  rushAtt = @rushAtt, 
                  rushBrokenTackles = @rushBrokenTackles, 
                  rushFum = @rushFum, 
                  rushLongest = @rushLongest, 
                  rushPts = @rushPts, 
                  rushTDs = @rushTDs, 
                  rushYds = @rushYds, 
                  rushYdsAfterContact = @rushYdsAfterContact, 
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
            INSERT INTO tblStatsRushing
            (leagueId, 
             playerId, 
             lastUpdatedOn, 
             fullName, 
             rosterId, 
             rush20PlusYds, 
             rushAtt, 
             rushBrokenTackles, 
             rushFum, 
             rushLongest, 
             rushPts, 
             rushTDs, 
             rushYds, 
             rushYdsAfterContact, 
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
             @rosterId, 
             @rush20PlusYds, 
             @rushAtt, 
             @rushBrokenTackles, 
             @rushFum, 
             @rushLongest, 
             @rushPts, 
             @rushTDs, 
             @rushYds, 
             @rushYdsAfterContact, 
             @scheduleId, 
             @seasonIndex, 
             @stageIndex, 
             @statId, 
             @teamId, 
             @weekIndex
            );
    END;