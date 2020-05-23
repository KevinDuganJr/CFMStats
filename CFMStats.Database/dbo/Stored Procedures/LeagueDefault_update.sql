-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 24
-- Description:	Set user league default
-- =============================================
create PROCEDURE [dbo].[LeagueDefault_update]
	 @ownerUserID nvarchar(128),
	 @leagueID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	IF EXISTS(SELECT ID FROM tblLeagueDefault WHERE userID = @ownerUserID)
		BEGIN
			UPDATE tblLeagueDefault 
			SET  leagueID = @leagueID
			WHERE userID = @ownerUserID;
		END
	ELSE
		BEGIN
			INSERT INTO tblLeagueDefault (userID, leagueID)
			VALUES (@ownerUserID, @leagueID)
		END
	 
		   
END
