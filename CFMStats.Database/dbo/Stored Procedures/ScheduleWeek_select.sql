-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 22
-- Description:	Select schedule by week
-- =============================================

CREATE PROCEDURE [dbo].[ScheduleWeek_select] @stageIndex  INT, 
								    @seasonIndex INT, 
								    @weekIndex   INT = NULL, 
								    @leagueId    INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   --DECLARE @endWeekindex int 
	   --IF @weekIndex IS NULL
	   --	BEGIN
	   --		SET @weekIndex = 0
	   --		SET @endWeekindex = 17 			
	   --	END
	   --ELSE
	   --	SET @endWeekindex = @weekIndex
	   SELECT 
			s.scheduleId, 
			ta.userName AS awayUser, 
			ta.cityName AS awayCity, 
			ta.displayName AS awayTeam, 
			ta.ovrRating AS awayOvr, 
			ta.teamId AS awayTeamId, 
			ta.primaryColor AS awayPrimaryColor,
			ta.secondaryColor AS awaySecondaryColor,
			s.awayScore, 
			th.userName AS homeUser, 
			th.cityName AS homeCity, 
			th.displayName AS homeTeam, 
			th.ovrRating AS homeOvr, 
			th.teamId AS homeTeamId, 
			th.primaryColor AS homePrimaryColor,
			th.secondaryColor AS homeSecondaryColor,
			s.homeScore
			FROM 
				tblSchedule AS s
			LEFT JOIN tblTeamInfo AS ta
				ON ta.teamID = s.awayTeamId
				   AND ta.leagueId = s.leagueId
			LEFT JOIN tblTeamInfo AS th
				ON th.teamID = s.homeTeamId
				   AND th.leagueId = s.leagueId
			WHERE s.stageIndex = @stageIndex
				 AND s.seasonIndex = @seasonIndex
				 AND s.weekIndex = @weekIndex
				 AND s.leagueId = @leagueId
			ORDER BY 
				    ta.userName DESC, 
				    th.username DESC;
    END;