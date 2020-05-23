-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 03
-- Description:	Select players by position group
-- =============================================
CREATE PROCEDURE [dbo].[Player_select] @positionGroupID   INT, 
                                       @teamID            INT, 
                                       @developmentId     INT, 
                                       @playerId          INT = NULL, 
                                       @isRetired         BIT, 
                                       @playerAge		  INT, 
                                       @leagueId          INT, 
                                       @yearsLeft         INT
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
                SET @endteamID = (SELECT MAX(teamid) FROM tblTeamInfo);
        END;
            ELSE
            SET @endteamID = @teamID;

        -- Set Player Id
        DECLARE @endPlayerID INT;
        IF @playerId IS NULL
            BEGIN
                SET @playerId = 0;
                SET @endPlayerID = (SELECT MAX(playerId) FROM tblPlayer);
        END;
            ELSE
            SET @endPlayerID = @playerId;

        -- Set Years Left
        DECLARE @endYearsLeft INT;
        IF @yearsLeft IS NULL
            BEGIN
                SET @yearsLeft = 0;
                SET @endYearsLeft = 99;
        END;
            ELSE
            SET @endYearsLeft = @yearsLeft;

        /****** Script for SelectTopNRows command from SSMS  ******/

        SELECT playerId, 
               r.lastUpdatedOn, 
               t.teamId, 
               t.abbrName, 
               t.displayName, 
               t.cityName, 
               portraitId, 
               presentationId, 
               jerseyNum, 
               firstName, 
               lastName, 
               age, 
               injuryLength, 
               injuryType, 
               birthDay, 
               birthMonth, 
               birthYear, 
               college, 
               draftPick, 
               draftRound, 
               experiencePoints, 
               height, 
               isActive, 
               isFreeAgent, 
               isOnIR, 
               isOnPracticeSquad, 
               isRetired, 
               legacyScore, 
               playerBestOvr, 
               playerSchemeOvr, 
               position, 
               rookieYear, 
               teamSchemeOvr, 
               [weight], 
               yearsPro, 
               durabilityGrade, 
               intangibleGrade, 
               physicalGrade, 
               productionGrade, 
               sizeGrade, 
               accelRating, 
               agilityRating, 
               awareRating, 
               bCVRating, 
               blockShedRating, 
               carryRating, 
               catchRating, 
               cITRating, 
               confRating, 
               elusiveRating, 
               finesseMovesRating, 
               hitPowerRating, 
               impactBlockRating, 
               injuryRating, 
               jukeMoveRating, 
               jumpRating, 
               kickAccRating, 
               kickPowerRating, 
               kickRetRating, 
               manCoverRating, 
               passBlockRating, 
               playActionRating, 
               playRecRating, 
               powerMovesRating, 
               pressRating, 
               pursuitRating, 
               releaseRating, 
               routeRunRating, 
               runBlockRating, 
               specCatchRating, 
               speedRating, 
               spinMoveRating, 
               staminaRating, 
               stiffArmRating, 
               strengthRating, 
               tackleRating, 
               throwAccDeepRating, 
               throwAccMidRating, 
               throwAccRating, 
               throwAccShortRating, 
               throwOnRunRating, 
               throwPowerRating, 
               toughRating, 
               truckRating, 
               zoneCoverRating, 
               contractSalary, 
               desiredBonus, 
               desiredLength, 
               desiredSalary, 
               reSignStatus, 
               bigHitTrait, 
               clutchTrait, 
               coverBallTrait, 
               devTrait, 
               dLBullRushTrait, 
               dLSpinTrait, 
               dLSwimTrait, 
               dropOpenPassTrait, 
               feetInBoundsTrait, 
               fightForYardsTrait, 
               forcePassTrait, 
               highMotorTrait, 
               hPCatchTrait, 
               lBStyleTrait, 
               penaltyTrait, 
               playBallTrait, 
               posCatchTrait, 
               predictTrait, 
               qBStyleTrait, 
               sensePressureTrait, 
               stripBallTrait, 
               throwAwayTrait, 
               tightSpiralTrait, 
               yACCatchTrait, 
               runStyle, 
               scheme, 
               breakSackRating, 
               breakTackleRating, 
               leadBlockRating, 
               passBlockFinesseRating, 
               passBlockPowerRating, 
               routeRunDeepRating, 
               routeRunMedRating, 
               routeRunShortRating, 
               runBlockFinesseRating, 
               runBlockPowerRating, 
               skillPoints, 
               throwUnderPressureRating
        FROM tblPlayer r
             LEFT JOIN tblTeamInfo t 
				ON t.teamId = r.teamId
				AND t.leagueId = r.leagueId
        WHERE positionGroupID BETWEEN @positionGroupID AND @endPositionGroupID
              AND r.teamID BETWEEN @teamId AND @endTeamId
              AND r.playerId BETWEEN @playerId AND @endPlayerId
              AND r.isRetired = 0 --@isRetired 
              AND r.age BETWEEN 0 AND @playerAge
              AND r.devTrait >= @developmentId              
              AND r.leagueId = @leagueId
              AND r.contractYearsLeft BETWEEN @yearsLeft AND @endYearsLeft
        ORDER BY teamSchemeOvr DESC, 
                 playerBestOvr DESC;
    END;