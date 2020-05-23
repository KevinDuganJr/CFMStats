-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 23
-- Description:	Get League ID
-- =============================================
create PROCEDURE [dbo].[LeagueID_select]
	 @exportID nvarchar(5) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		ID,
		lastUpdatedOn,
		Name,
		exportID,
		ownerUserID,
		isActive
	FROM tblLeague	
	WHERE exportID = @exportID; 
		   
END
