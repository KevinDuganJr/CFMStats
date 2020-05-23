-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2020 MAR 04
-- Description:	insert or update madden player traits
-- =============================================

CREATE PROCEDURE [dbo].[PlayerTraits_update] @leagueId           INT, 
                                             @rosterId           INT, 
                                             @bigHitTrait        BIT, 
                                             @clutchTrait        INT, 
                                             @coverBallTrait     INT, 
                                             @devTrait           INT, 
                                             @dLBullRushTrait    INT, 
                                             @dLSpinTrait        INT, 
                                             @dLSwimTrait        INT, 
                                             @dropOpenPassTrait  INT, 
                                             @feetInBoundsTrait  INT, 
                                             @fightForYardsTrait INT, 
                                             @forcePassTrait     INT, 
                                             @highMotorTrait     INT, 
                                             @hPCatchTrait       INT, 
                                             @lBStyleTrait       INT, 
                                             @penaltyTrait       INT, 
                                             @playBallTrait      INT, 
                                             @posCatchTrait      INT, 
                                             @predictTrait       INT, 
                                             @qBStyleTrait       INT, 
                                             @sensePressureTrait INT, 
                                             @stripBallTrait     INT, 
                                             @throwAwayTrait     INT, 
                                             @tightSpiralTrait   INT, 
                                             @yACCatchTrait      INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        DECLARE @playerId INT= (dbo.fn_GetActivePlayerId(@rosterId, @leagueId));

        IF EXISTS (SELECT * FROM tblPlayerTraits WHERE leagueId = @leagueid AND playerId = @playerId AND playerId > 0)
            BEGIN
                UPDATE tblPlayerTraits
                  SET
                      ModifiedOn = GETUTCDATE(),
                      bigHitTrait = @bigHitTrait, 
                      clutchTrait = @clutchTrait, 
                      coverBallTrait = @coverBallTrait, 
                      devTrait = @devTrait, 
                      dLBullRushTrait = @dLBullRushTrait, 
                      dLSpinTrait = @dLSpinTrait, 
                      dLSwimTrait = @dLSwimTrait, 
                      dropOpenPassTrait = @dropOpenPassTrait, 
                      feetInBoundsTrait = @feetInBoundsTrait, 
                      fightForYardsTrait = @fightForYardsTrait, 
                      forcePassTrait = @forcePassTrait, 
                      highMotorTrait = @highMotorTrait, 
                      hPCatchTrait = @hPCatchTrait, 
                      lBStyleTrait = @lBStyleTrait, 
                      penaltyTrait = @penaltyTrait, 
                      playBallTrait = @playBallTrait, 
                      posCatchTrait = @posCatchTrait, 
                      predictTrait = @predictTrait, 
                      qBStyleTrait = @qBStyleTrait, 
                      sensePressureTrait = @sensePressureTrait, 
                      stripBallTrait = @stripBallTrait, 
                      throwAwayTrait = @throwAwayTrait, 
                      tightSpiralTrait = @tightSpiralTrait, 
                      yACCatchTrait = @yACCatchTrait
                WHERE playerId = @playerId
                      AND leagueId = @leagueId;
        END;
            ELSE
            BEGIN
                INSERT INTO tblPlayerTraits
                (playerId, 
                 leagueId,
                 CreatedOn,
                 ModifiedOn,
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
                 yACCatchTrait
                )
                VALUES
                (@playerId, 
                 @leagueId, 
                 GETUTCDATE(),
                 GETUTCDATE(),
                 @bigHitTrait, 
                 @clutchTrait, 
                 @coverBallTrait, 
                 @devTrait, 
                 @dLBullRushTrait, 
                 @dLSpinTrait, 
                 @dLSwimTrait, 
                 @dropOpenPassTrait, 
                 @feetInBoundsTrait, 
                 @fightForYardsTrait, 
                 @forcePassTrait, 
                 @highMotorTrait, 
                 @hPCatchTrait, 
                 @lBStyleTrait, 
                 @penaltyTrait, 
                 @playBallTrait, 
                 @posCatchTrait, 
                 @predictTrait, 
                 @qBStyleTrait, 
                 @sensePressureTrait, 
                 @stripBallTrait, 
                 @throwAwayTrait, 
                 @tightSpiralTrait, 
                 @yACCatchTrait
                );
        END;
    END;