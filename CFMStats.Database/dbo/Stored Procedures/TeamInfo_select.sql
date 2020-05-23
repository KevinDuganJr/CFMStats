-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 28
-- Description:	select league teams
-- =============================================
CREATE PROCEDURE [dbo].[TeamInfo_select]
				@leagueId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT  teamId,
		lastUpdatedOn,
		abbrName,
		cityName,
		displayName,
		divName,
		offScheme,
		defScheme,
		ovrRating,
		injuryCount,
		primaryColor,
		secondaryColor,
		userName
FROM tblTeamInfo
WHERE leagueId = @leagueId
ORDER BY displayName ASC;
		
		
		
		 
		   
END
