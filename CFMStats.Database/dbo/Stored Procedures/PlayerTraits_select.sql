-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2018 NOV 25
-- Description:	Select players traits
-- =============================================
CREATE PROCEDURE [dbo].[PlayerTraits_select] @playerId INT
AS
    BEGIN

        /****** Script for SelectTopNRows command from SSMS  ******/

        SELECT [playerId], 
               [bigHitTrait], 
               [clutchTrait], 
               [coverBallTrait], 
               [devTrait], 
               [dLBullRushTrait], 
               [dLSpinTrait], 
               [dLSwimTrait], 
               [dropOpenPassTrait], 
               [feetInBoundsTrait], 
               [fightForYardsTrait], 
               [forcePassTrait], 
               [highMotorTrait], 
               [hPCatchTrait], 
               [lBStyleTrait], 
               [penaltyTrait], 
               [playBallTrait], 
               [posCatchTrait], 
               [predictTrait], 
               [qBStyleTrait], 
               [sensePressureTrait], 
               [stripBallTrait], 
               [throwAwayTrait], 
               [tightSpiralTrait], 
               [yACCatchTrait]
        FROM [tblPlayerTraits]
        WHERE [playerId] = @playerId;
    END;