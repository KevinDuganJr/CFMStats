-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2019 NOV 20
-- Description:	Select players traits
-- =============================================
CREATE PROCEDURE [dbo].[GetPlayerAbilities] @playerId INT
AS
    BEGIN

        /****** Script for SelectTopNRows command from SSMS  ******/

        SELECT a.signaturelogoid, 
               pa.islocked, 
               pa.isempty, 
               pa.OvrThreshold, 
               a.SignatureTitle, 
               a.SignatureDescription, 
               a.SignatureActivationDescription, 
               a.SignatureDeactivationDescription
        FROM tblPlayerAbilities pa
             JOIN tblAbilities a ON pa.AbilityId = a.SignatureLogoId
                                    AND pa.leagueid = a.leagueid
        WHERE playerid = @playerId
		ORDER BY pa.OvrThreshold ASC;
    END;