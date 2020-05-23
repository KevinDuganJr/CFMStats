-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2019 NOV 19
-- Description:	insert or update madden abilities
-- =============================================
CREATE PROCEDURE [dbo].[Abilities_update] @leagueId                         INT, 
                                          @signatureLogoId                  INT, 
                                          @signatureTitle                   NVARCHAR(50), 
                                          @signatureDescription             NVARCHAR(250), 
                                          @signatureActivationDescription   NVARCHAR(250), 
                                          @signatureDeactivationDescription NVARCHAR(250)
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        IF EXISTS ( SELECT * FROM tblAbilities WHERE leagueId = @leagueId AND signatureLogoId = @signatureLogoId )
		UPDATE tblAbilities
              SET 
                  ModifiedOn = GETUTCDATE(), 
                  signatureTitle = @signatureTitle, 
                  signatureDescription = @signatureDescription, 
                  signatureActivationDescription = @signatureActivationDescription, 
                  signatureDeactivationDescription = @signatureDeactivationDescription
            WHERE leagueId = @leagueId
                  AND signatureLogoId = @signatureLogoId;
            ELSE
            INSERT INTO tblAbilities
            (leagueId, 
             SignatureLogoId, 
             SignatureTitle, 
             SignatureDescription, 
             SignatureActivationDescription, 
             SignatureDeactivationDescription, 
             CreatedOn, 
             ModifiedOn
            )
            VALUES
            (@leagueId, 
             @SignatureLogoId, 
             @SignatureTitle, 
             @SignatureDescription, 
             @SignatureActivationDescription, 
             @SignatureDeactivationDescription, 
             GETUTCDATE(), 
             GETUTCDATE()
            );
    END;