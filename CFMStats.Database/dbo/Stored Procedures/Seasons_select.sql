-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JULY 20
-- Description:	Select current week
-- =============================================
CREATE PROCEDURE [dbo].[Seasons_select]
			@leagueId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT(seasonIndex)
	FROM tblSchedule 
	WHERE leagueId = @leagueId
	ORDER BY seasonIndex DESC;
			   
END
