-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 24
-- Description:	Get user favorite league
-- =============================================
create PROCEDURE [dbo].[LeagueUserFavorite_select]
	 @ownerUserID nvarchar(128) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	 
	SELECT 
		F.Id_new as ID,
		lastUpdatedOn,
		Name,
		exportID		
		
	FROM tblLeague L
	JOIN tblLeagueUserFavorite F
	ON L.ID = F.leagueID
	WHERE  F.ownerUserID = @ownerUserID;

		   
END
