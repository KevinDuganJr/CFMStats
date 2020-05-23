-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2018 SEPT 02
-- Description:	Retire Players
-- =============================================
CREATE PROCEDURE [dbo].[SetPlayersAsRetiredAndFreeAgent] @leagueId INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
        SET NOCOUNT ON;

        -- make all players retired and not on a team
        UPDATE [tblPlayerProfile]
          SET 
              [isRetired] = 1, 
              [teamID] = 0
        WHERE [leagueId] = @leagueId;
    END;