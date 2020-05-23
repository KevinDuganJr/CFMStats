CREATE PROCEDURE [dbo].[GetLeagueIdByPlayerId] @playerId INT
AS
    BEGIN
	   SELECT 
			[leagueId]
			FROM 
				[dbo].[tblPlayerProfile]
			WHERE [playerId] = @playerId;
	    
    END;