-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 11
-- Description:	Select team red zone
-- =============================================
CREATE PROCEDURE [dbo].[TeamRedZone_select]
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
		SUM(s.offRedZones) as offRedZones,
		SUM(s.offRedZoneFGs) as offRedZoneFGs,
		SUM(s.offRedZoneTDs) as offRedZoneTDs,
		--(100.0 *(SUM(s.offRedZoneFGs) + SUM(s.offRedZoneTDs)) / (SUM(s.offRedZones))) as offRedZonePct,
		
		SUM(s.defRedZones) as defRedZones,
		SUM(s.defRedZoneFGs) as defRedZoneFGs,
		SUM(s.defRedZoneTDs) as defRedZoneTDs
		--(100.0 *(SUM(s.defRedZoneFGs) + SUM(s.defRedZoneTDs)) / (SUM(s.defRedZones))) as defRedZonePct



  FROM tblStatsTeam s
  	LEFT JOIN tblTeamInfo t
ON t.teamId = s.teamId
	AND t.leagueId = s.leagueId
	WHERE s.stageIndex = @stageIndex
	AND s.seasonIndex = @seasonIndex
	AND s.weekIndex BETWEEN @weekIndex AND @endWeekindex
	AND s.leagueId = @leagueId
	GROUP BY t.displayName, t.divName
	
	ORDER BY offRedZones DESC;
			   
END