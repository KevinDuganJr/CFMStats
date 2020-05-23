-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 24
-- Description:	Add/Remove user favorite league
-- =============================================
CREATE PROCEDURE [dbo].[LeagueUserFavorite_update]
	 @ownerUserID nvarchar(128) = NULL,
	 @leagueID int = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	IF EXISTS(SELECT ID_New FROM tblLeagueUserFavorite WHERE ownerUserID = @ownerUserID AND leagueID = @leagueID)
		BEGIN
			DELETE tblLeagueUserFavorite WHERE ownerUserID = @ownerUserID AND leagueID = @leagueID;
		END
	ELSE
		BEGIN
			INSERT INTO tblLeagueUserFavorite (ownerUserID, leagueID)
			VALUES (@ownerUserID, @leagueID)
		END
	 
		   
END
