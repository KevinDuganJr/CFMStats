-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 SEPT 22
-- Description:	Select players salary by position group
-- =============================================
CREATE PROCEDURE [dbo].[PlayerSalary_select] @positionGroupID INT = NULL, 
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
                    WHERE leagueId = @leagueId
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

        SELECT pp.playerId, 
               t.teamId, 
               t.abbrName, 
               t.displayName, 
               t.cityName, 
               Standings.capRoom, 
               Standings.capSpent, 
               pp.firstName, 
               pp.lastName, 
               pt.devTrait, 
               pp.age, 
               pr.playerBestOvr, 
               pr.playerSchemeOvr, 
               pr.teamSchemeOvr, 
               pp.position, 
               pp.yearsPro, 
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
               pp.skillPoints
        FROM tblPlayerProfile pp
             JOIN tblPlayerContract pc ON pc.playerId = pp.playerId
                                          AND pc.leagueId = pp.leagueId
             JOIN tblPlayerRatings pr ON pr.playerId = pp.playerId
                                         AND pr.leagueId = pp.leagueId
             JOIN tblPlayerTraits pt ON pt.playerId = pp.playerId
                                        AND pt.leagueId = pp.leagueId
             LEFT JOIN tblTeamInfo t ON t.teamId = pp.teamId
                                        AND t.leagueId = pp.leagueId
             CROSS APPLY
        (
            SELECT TOP 1 s.capRoom, 
                         s.capSpent
            FROM tblTeamStandings s
            WHERE s.teamID = pp.teamId
                  AND s.seasonIndex = @seasonIndex
                  AND s.leagueId = pp.leagueId
            ORDER BY s.lastUpdatedOn DESC
        ) AS Standings
        WHERE pp.positionGroupID BETWEEN @positionGroupID AND @endPositionGroupID
              AND pp.teamID BETWEEN @teamId AND @endTeamId
              AND pp.leagueId = @leagueId
        ORDER BY pr.playerBestOvr DESC;
    END;