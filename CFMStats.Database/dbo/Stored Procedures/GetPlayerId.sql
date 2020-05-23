CREATE PROCEDURE [dbo].[GetPlayerId] @rosterId INT, 
                                     @leagueId INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        SELECT playerId
        FROM tblPlayerProfile
        WHERE rosterId = @rosterId
              AND leagueId = @leagueId
              AND isRetired = 0;
    END;