-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JULY 19
-- Description:	Res
-- =============================================
CREATE PROCEDURE [dbo].[RosterRetireFix_Update]
					@leagueId int
 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	-- make all players retired and not on a team
	UPDATE tblPlayerProfile SET isRetired = 1, teamID = 0 WHERE leagueId = @leagueId;

	
	DECLARE @seasonIndex int 
	SET @seasonIndex = (SELECT MAX(seasonIndex) FROM tblSchedule WHERE leagueId = @leagueId);

	UPDATE S 
	SET S.rosterid = 0
	FROM tblPlayerProfile P
	JOIN tblStatsDefense S
	ON P.rosterId = S.rosterId
	AND P.yearsPro = 0
	AND S.seasonindex < @seasonIndex
	AND S.leagueId = P.leagueId
	WHERE S.leagueId = @leagueId;

	UPDATE  S 
	SET S.rosterid = 0
	FROM tblPlayerProfile P
	JOIN tblStatsKicking S
	ON P.rosterId = S.rosterId
	AND P.yearsPro = 0
	AND S.seasonindex < @seasonIndex
	AND S.leagueId = P.leagueId
	WHERE S.leagueId = @leagueId;

	UPDATE  S 
	SET S.rosterid = 0
	FROM tblPlayerProfile P
	JOIN tblStatsPassing S
	ON P.rosterId = S.rosterId
	AND P.yearsPro = 0
	AND S.seasonindex < @seasonIndex
	AND S.leagueId = P.leagueId
	WHERE S.leagueId = @leagueId;

	UPDATE  S 
	SET S.rosterid = 0
	FROM tblPlayerProfile P
	JOIN tblStatsPunting S
	ON P.rosterId = S.rosterId
	AND P.yearsPro = 0
	AND S.seasonindex < @seasonIndex
	AND S.leagueId = P.leagueId
	WHERE S.leagueId = @leagueId;

	UPDATE  S 
	SET S.rosterid = 0
	FROM tblPlayerProfile P
	JOIN tblStatsReceiving S
	ON P.rosterId = S.rosterId
	AND P.yearsPro = 0
	AND S.seasonindex < @seasonIndex
	AND S.leagueId = P.leagueId
	WHERE S.leagueId = @leagueId;
	
	UPDATE  S
	SET S.rosterid = 0
	FROM tblPlayerProfile P
	JOIN tblStatsRushing S
	ON P.rosterId = S.rosterId
	AND P.yearsPro = 0
	AND S.seasonindex < @seasonIndex
	AND S.leagueId = P.leagueId
	WHERE S.leagueId = @leagueId;
			   
END