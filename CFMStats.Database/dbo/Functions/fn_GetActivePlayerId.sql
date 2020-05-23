-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2018 SEPT 03
-- Description:	Return Player ID who is not retired
-- =============================================
CREATE FUNCTION [dbo].[fn_GetActivePlayerId]
(@rosterId INT, 
 @leagueId INT
)
RETURNS INT
AS
     BEGIN
         DECLARE @playerId INT;
         RETURN
         (
             SELECT playerId
             FROM dbo.tblPlayerProfile
             WHERE rosterId = @rosterId
                   AND leagueId = @leagueId
                   AND isRetired = 0
         );
     END;