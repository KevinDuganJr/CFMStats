-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 17
-- Description:	Select player goals by position group
-- =============================================
CREATE PROCEDURE [dbo].[PlayerGoal_select]
	 @positionGroupID int = NULL,
	 @teamID int = NULL,
	 @leagueId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @endpositionGroupID int 
	IF @positionGroupID IS NULL
		BEGIN
			SET @positionGroupID = 0
			SET @endpositionGroupID = 99 			
		END
	ELSE
		SET @endpositionGroupID = @positionGroupID
	

	DECLARE @endteamID int 	 
	IF @teamID IS NULL
		BEGIN
			SET @teamID = 0
			SET @endteamID = (SELECT MAX(teamid) FROM tblTeamInfo)
		END
	ELSE
		SET @endteamID = @teamID
	

	 

	/****** Script for SelectTopNRows command from SSMS  ******/
	SELECT  
			g.rosterId,
			g.lastUpdatedOn,
			t.teamId,
			t.abbrName,
			t.displayName,
			t.cityName,
			r.firstName,
			r.lastName,
			r.age,
			r.playerBestOvr,
			r.playerSchemeOvr,
			r.teamSchemeOvr,
			r.position,
			r.yearsPro,
			goalId,
			title,
			currentLevel,
			completionValue1,
			completionValue2,
			completionValue3,
			completionValue4,
			experienceAward1,
			experienceAward2,
			experienceAward3,
			experienceAward4
		
	FROM tblPlayerGoals g
	LEFT JOIN tblTeamInfo t
	ON t.teamId = g.teamId
	AND t.leagueId = g.leagueId
	LEFT JOIN tblPlayer r
	ON r.rosterId = g.rosterid
	AND r.leagueId = g.leagueId
	WHERE positionGroupID BETWEEN @positionGroupID AND @endPositionGroupID
	AND g.teamID BETWEEN @teamId AND @endTeamId
	AND g.leagueId = @leagueId

	ORDER BY teamSchemeOvr DESC, playerBestOvr DESC  	
		   
END
