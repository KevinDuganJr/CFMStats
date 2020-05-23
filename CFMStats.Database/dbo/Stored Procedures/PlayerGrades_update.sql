-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2020 MAR 04
-- Description:	insert or update madden player grades
-- =============================================

CREATE PROCEDURE [dbo].[PlayerGrades_update] @leagueId        INT, 
                                             @rosterId        INT,                                              
                                             @durabilityGrade INT, 
                                             @intangibleGrade INT, 
                                             @physicalGrade   INT, 
                                             @productionGrade INT, 
                                             @sizeGrade       INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
 
        DECLARE @playerId INT= (dbo.fn_GetActivePlayerId (@rosterId, @leagueId));


    IF EXISTS (SELECT * FROM tblPlayerGrades WHERE leagueId = @leagueid AND playerId = @playerId AND playerId > 0)
            BEGIN
                UPDATE tblPlayerGrades
                  SET 
                      ModifiedOn = GETUTCDATE(),
                      durabilityGrade = @durabilityGrade, 
                      intangibleGrade = @intangibleGrade, 
                      physicalGrade = @physicalGrade, 
                      productionGrade = @productionGrade, 
                      sizeGrade = @sizeGrade
                WHERE playerId = @playerId
                      AND leagueId = @leagueId;
        END;
            ELSE
            BEGIN
                INSERT INTO tblPlayerGrades
                (playerId,
                 leagueId, 
                 CreatedOn, 
                 ModifiedOn,
                 durabilityGrade, 
                 intangibleGrade, 
                 physicalGrade, 
                 productionGrade, 
                 sizeGrade
                )
                VALUES
                (@playerId, 
                 @leagueId, 
                 GETUTCDATE(),
                 GETUTCDATE(),
                 @durabilityGrade, 
                 @intangibleGrade, 
                 @physicalGrade, 
                 @productionGrade, 
                 @sizeGrade
                );
        END;
    END;