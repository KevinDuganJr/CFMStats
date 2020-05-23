-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 AUGUST 15
-- Description:	Select player profile
-- =============================================
CREATE PROCEDURE [dbo].[PlayerProfile_select] @playerId INT
AS
    BEGIN
        SET NOCOUNT ON;
        SELECT p.firstName, 
               p.lastName, 
               p.jerseyNum, 
               p.portraitId, 
               p.position, 
               p.age, 
               p.height, 
               p.[weight], 
               p.college, 
               p.isRetired, 
               p.isActive, 
               p.isOnIR, 
               p.isOnPracticeSquad, 
               pr.playerBestOVR, 
               pr.playerSchemeOvr, 
               pr.teamSchemeOvr, 
               p.yearsPro, 
               p.draftRound, 
               p.draftPick, 
               pc.contractSalary, 
               t.cityName, 
               t.displayName, 
               p.injuryType, 
               j.id, 
               j.injuryname
        FROM tblPlayerProfile P
             LEFT JOIN tblTeamInfo T ON T.TeamID = P.TeamID
                                        AND T.leagueId = P.leagueId
             LEFT JOIN tblInjury J ON J.ID = P.injuryType
             LEFT JOIN tblPlayerContract pc ON pc.playerId = p.playerId
                                               AND pc.leagueId = P.leagueId
             LEFT JOIN tblPlayerRatings pr ON p.playerId = pr.playerId
                                              AND pr.leagueId = P.leagueId
        WHERE P.playerId = @playerId;
    END;