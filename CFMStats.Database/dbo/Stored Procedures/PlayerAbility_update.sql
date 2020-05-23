-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2019 NOV 19
-- Description:	insert or update madden player abilities
-- =============================================
CREATE PROCEDURE [dbo].[PlayerAbility_Update] @leagueId        INT, 
                                              @rosterId        INT, 
                                              @signatureLogoId INT, 
                                              @isEmpty         BIT, 
                                              @isLocked        BIT, 
                                              @ovrThreshold    INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @playerId INT= (dbo.fn_GetActivePlayerId (@rosterId, @leagueId));
        
		IF EXISTS (SELECT * FROM tblPlayerAbilities WHERE leagueId = @leagueId AND AbilityId = @signatureLogoId AND PlayerId = @playerId)
            UPDATE tblPlayerAbilities
              SET 
                  isEmpty = @isEmpty, 
                  islocked = @isLocked, 
                  ovrThreshold = @ovrThreshold, 
                  ModifiedOn = GETUTCDATE()
            WHERE leagueId = @leagueId
                  AND AbilityId = @signatureLogoId
                  AND PlayerId = @playerId;
            ELSE
            INSERT INTO tblPlayerAbilities
            (leagueId, 
             PlayerId, 
             AbilityId, 
             IsEmpty, 
             IsLocked, 
             OvrThreshold, 
             CreatedOn, 
             ModifiedOn
            )
            VALUES
            (@leagueId, 
             @PlayerId, 
             @signatureLogoId, 
             @IsEmpty, 
             @IsLocked, 
             @OvrThreshold, 
             GETUTCDATE(), 
             GETUTCDATE()
            );
    END;