
-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2020 MAR 04
-- Description:	Select players by position group
-- =============================================
CREATE PROCEDURE [dbo].[PlayerRatings_select] @positionGroupID INT, 
                                              @teamID          INT, 
                                              @developmentId   INT, 
                                              @playerId        INT = NULL,                                               
                                              @playerAge       INT, 
                                              @onPracticeSquad BIT, 
                                              @yearsPro        INT,
                                              @leagueId        INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
        SET NOCOUNT ON;
        -- Set Position Group Id
        DECLARE @endpositionGroupID INT;
        IF @positionGroupID IS NULL
            BEGIN
                SET @positionGroupID = 0;
                SET @endpositionGroupID = 99;
        END;
            ELSE
            SET @endpositionGroupID = @positionGroupID;

        -- Set Team Id
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
            SET @endteamID = @teamID;

        -- Set Player Id
        DECLARE @endPlayerID INT;
        IF @playerId IS NULL
            BEGIN
                SET @playerId = 0;
                SET @endPlayerID =
                (
                    SELECT MAX(playerId)
                    FROM tblPlayerProfile
					WHERE leagueId = @leagueId
                );
        END;
            ELSE
            SET @endPlayerID = @playerId;

        /****** Script for SelectTopNRows command from SSMS  ******/

        SELECT r.playerId, 
               t.teamId, 
               t.abbrName, 
               t.displayName, 
               t.cityName, 
               
               r.position, 
               r.jerseyNum, 
               
               pt.devTrait, 
               r.injuryLength, 
               r.injuryType, 
               
               r.age, 
               r.height, 

               r.yearsPro,
               r.firstName, 
               r.lastName, 

               pr.playerBestOvr, 
               pr.playerSchemeOvr, 
               pr.teamSchemeOvr, 
               r.experiencePoints, 
               r.skillPoints,

               r.isActive, 
               r.isFreeAgent, 
               r.isOnIR, 
               r.isOnPracticeSquad, 
               r.isRetired, 
               
               pr.accelRating, 
               pr.agilityRating, 
               pr.awareRating, 
               pr.bCVRating, 
               pr.blockShedRating, 
               pr.carryRating, 
               pr.catchRating, 
               pr.cITRating, 
               pr.confRating, 
               pr.changeOfDirectionRating, 
               pr.finesseMovesRating, 
               pr.hitPowerRating, 
               pr.impactBlockRating, 
               pr.injuryRating, 
               pr.jukeMoveRating, 
               pr.jumpRating, 
               pr.kickAccRating, 
               pr.kickPowerRating, 
               pr.kickRetRating, 
               pr.manCoverRating, 
               pr.passBlockRating, 
               pr.playActionRating, 
               pr.playRecRating, 
               pr.powerMovesRating, 
               pr.pressRating, 
               pr.pursuitRating, 
               pr.releaseRating, 
               pr.routeRunRating, 
               pr.runBlockRating, 
               pr.specCatchRating, 
               pr.speedRating, 
               pr.spinMoveRating, 
               pr.staminaRating, 
               pr.stiffArmRating, 
               pr.strengthRating, 
               pr.tackleRating, 
               pr.throwAccDeepRating, 
               pr.throwAccMidRating, 
               pr.throwAccRating, 
               pr.throwAccShortRating, 
               pr.throwOnRunRating, 
               pr.throwPowerRating, 
               pr.toughRating, 
               pr.truckRating, 
               pr.zoneCoverRating, 
               pr.breakSackRating, 
               pr.breakTackleRating, 
               pr.leadBlockRating, 
               pr.passBlockFinesseRating, 
               pr.passBlockPowerRating, 
               pr.routeRunDeepRating, 
               pr.routeRunMedRating, 
               pr.routeRunShortRating, 
               pr.runBlockFinesseRating, 
               pr.runBlockPowerRating, 
               pr.throwUnderPressureRating
               
        FROM tblPlayerProfile r
             LEFT OUTER JOIN tblTeamInfo t 
				ON t.teamId = r.teamId
				AND t.leagueId = r.leagueId

             JOIN tblPlayerRatings pr 			 
				ON r.playerId = pr.playerId
           --     AND r.leagueId = pr.leagueId
             
			 JOIN tblPlayerTraits pt 
				ON r.playerId = pt.playerId
			--	AND r.leagueId = pt.leagueId

        WHERE positionGroupID BETWEEN @positionGroupID AND @endPositionGroupID
              AND isOnPracticeSquad BETWEEN @onPracticeSquad AND 1
			  AND pt.devTrait >= @developmentId
			  AND r.yearsPro <= @yearsPro
			  AND r.teamID BETWEEN @teamId AND @endTeamId
              AND r.playerId BETWEEN @playerId AND @endPlayerId
              AND r.isRetired = 0
              AND r.age BETWEEN 0 AND @playerAge
              AND r.leagueId = @leagueId
              
        ORDER BY pr.playerBestOvr DESC;
    END;