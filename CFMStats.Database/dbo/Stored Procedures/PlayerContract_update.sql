-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2020 MAR 04
-- Description:	insert or update madden player contract
-- =============================================

CREATE PROCEDURE [dbo].[PlayerContract_update] @leagueId             INT, 
                                               @rosterId             INT, 
                                               @capHit               INT, 
                                               @capReleaseNetSavings INT, 
                                               @capReleasePenalty    INT, 
                                               @contractBonus        INT, 
                                               @contractLength       INT, 
                                               @contractSalary       INT, 
                                               @contractYearsLeft    INT, 
                                               @desiredBonus         INT, 
                                               @desiredLength        INT, 
                                               @desiredSalary        INT, 
                                               @reSignStatus         INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @playerId INT= (dbo.fn_GetActivePlayerId (@rosterId, @leagueId));


        IF EXISTS (SELECT * FROM tblPlayerContract WHERE leagueId = @leagueid AND playerId = @playerId AND playerId > 0)
            BEGIN
                UPDATE tblPlayerContract
                  SET 
                      ModifiedOn = GETUTCDATE(),
                      capHit = @capHit, 
                      capReleaseNetSavings = @capReleaseNetSavings, 
                      capReleasePenalty = @capReleasePenalty, 
                      contractBonus = @contractBonus, 
                      contractLength = @contractLength, 
                      contractSalary = @contractSalary, 
                      contractYearsLeft = @contractYearsLeft, 
                      desiredBonus = @desiredBonus, 
                      desiredLength = @desiredLength, 
                      desiredSalary = @desiredSalary, 
                      reSignStatus = @reSignStatus
                WHERE playerId = @playerId
                      AND leagueId = @leagueId;
        END;
            ELSE
            BEGIN
                INSERT INTO tblPlayerContract
                (playerId,
                 leagueId, 
                 CreatedOn,
                 ModifiedOn,
                 capHit, 
                 capReleaseNetSavings, 
                 capReleasePenalty, 
                 contractBonus, 
                 contractLength, 
                 contractSalary, 
                 contractYearsLeft, 
                 desiredBonus, 
                 desiredLength, 
                 desiredSalary, 
                 reSignStatus
                )
                VALUES
                (@playerId, 
                 @leagueId, 
                 GETUTCDATE(),
                 GETUTCDATE(),
                 @capHit, 
                 @capReleaseNetSavings, 
                 @capReleasePenalty, 
                 @contractBonus, 
                 @contractLength, 
                 @contractSalary, 
                 @contractYearsLeft, 
                 @desiredBonus, 
                 @desiredLength, 
                 @desiredSalary, 
                 @reSignStatus
                );
        END;
    END;