-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 24
-- Description:	Select user league default
-- =============================================
create PROCEDURE [dbo].[LeagueDefault_select]
	 @ownerUserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
		BEGIN
			SELECT
				 l.ID,
				 l.Name

			FROM tblLeagueDefault d
			JOIN tblLeague l
			ON l.ID = d.leagueID
			WHERE userID = @ownerUserID;
	 END
		   
END
