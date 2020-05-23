-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 SEPT 22
-- Description:	Select team salary by position group
-- =============================================
CREATE PROCEDURE [dbo].[TeamSalary_select] @positionGroupID INT = NULL, 
                                           @teamID          INT = NULL, 
                                           @leagueId        INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @endpositionGroupID INT;
        IF @positionGroupID IS NULL
            BEGIN
                SET @positionGroupID = 0;
                SET @endpositionGroupID = 99;  
        END;
            ELSE
            BEGIN
                SET @endpositionGroupID = @positionGroupID;
        END;
        DECLARE @endteamID INT;
        IF @teamID IS NULL
            BEGIN
                SET @teamID = 0;
                SET @endteamID =
                (
                    SELECT MAX(teamid)
                    FROM tblTeamInfo
                );
        END;
            ELSE
            BEGIN
                SET @endteamID = @teamID;
        END;
        DECLARE @seasonIndex INT;
        SET @seasonIndex =
        (
            SELECT MAX(tblTeamStandings.Seasonindex)
            FROM dbo.tblTeamStandings
            WHERE tblTeamStandings.leagueID = @leagueID
        );

        /****** Script for SelectTopNRows command from SSMS  ******/

        SELECT r.playerId, 
               r.ModifiedOn, 
               t.teamId, 
               t.abbrName, 
               t.displayName, 
               t.cityName, 
               Standings.capRoom, 
               Standings.capSpent, 
               r.firstName, 
               r.lastName, 
               pt.devTrait, 
               r.age, 
               pr.playerBestOvr, 
               pr.playerSchemeOvr, 
               pr.teamSchemeOvr, 
               r.position, 
               r.yearsPro, 
               pc.capHit, 
               pc.capReleaseNetSavings, 
               pc.capReleasePenalty, 
               pc.contractBonus, 
               pc.contractLength, 
               pc.contractSalary, 
               pc.contractYearsLeft, 
               pc.desiredBonus, 
               pc.desiredLength, 
               pc.desiredSalary, 
               pc.reSignStatus, 
               r.skillPoints
        FROM tblPlayerProfile r
             LEFT JOIN tblTeamInfo t ON t.teamId = r.teamId
                                        AND t.leagueId = r.leagueId
             LEFT JOIN tblPlayerContract pc ON r.playerId = pc.playerId
                                               AND r.leagueId = pc.leagueId
             LEFT JOIN tblPlayerTraits pt ON r.playerId = pt.playerId
                                             AND r.leagueId = pt.leagueId
             LEFT JOIN tblPlayerRatings pr ON r.playerId = pr.playerId
                                             AND r.leagueId = pr.leagueId
             CROSS APPLY
        (
            SELECT TOP 1 s.capRoom, 
                         s.capSpent
            FROM tblTeamStandings s
            WHERE s.teamID = r.teamId
                  AND s.seasonIndex = @seasonIndex
                  AND s.leagueId = r.leagueId
            ORDER BY s.lastUpdatedOn DESC
        ) AS Standings
        WHERE r.positionGroupID BETWEEN @positionGroupID AND @endPositionGroupID
              AND r.teamID BETWEEN @teamId AND @endTeamId
              AND r.leagueId = @leagueId
        ORDER BY pr.teamSchemeOvr DESC, 
                 pr.playerBestOvr DESC;
    END;