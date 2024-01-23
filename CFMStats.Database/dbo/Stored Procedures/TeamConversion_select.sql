-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 11
-- Description:	Select team conversions
-- =============================================
CREATE PROCEDURE [dbo].[TeamConversion_select]
	 @stageIndex int,
	 @seasonIndex int,
	 @weekindex int = NULL,
	 @leagueId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @endWeekindex int 

	-- set the WeekIndex
	IF @weekIndex IS NULL
		BEGIN
			SET @weekIndex = 0
			SET @endWeekindex = 17 			
		END
	ELSE
		SET @endWeekindex = @weekIndex


	SELECT 
		t.displayName as teamName,
		t.divName,
		
		COUNT(*) as Games,
		SUM(off3rdDownAtt) as off3rdDownAtt,
		SUM(off3rdDownConv) as off3rdDownConv,
		--(100.0 *(SUM(off3rdDownAtt) / SUM(off3rdDownConv))) as off3rdDownPctNew,

		SUM(off4thDownAtt) as off4thDownAtt,
		SUM(off4thDownConv) as off4thDownConv,
		
		SUM(off2PtAtt) as off2PtAtt,
		SUM(off2PtConv) as off2PtConv
		

  FROM tblStatsTeam s
  	LEFT JOIN tblTeamInfo t
	ON t.teamId = s.teamId
	AND t.leagueId = s.leagueId
	WHERE s.stageIndex = @stageIndex
	AND s.seasonIndex = @seasonIndex
	AND s.weekIndex BETWEEN @weekIndex AND @endWeekindex
	AND s.leagueId = @leagueId
	GROUP BY t.displayName, t.divName
	
	ORDER BY SUM(off3rdDownConv) DESC;
			   
END