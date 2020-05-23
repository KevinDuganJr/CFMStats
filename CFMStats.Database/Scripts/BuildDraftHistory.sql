CREATE PROCEDURE [dbo].[BuildDraftHistory]	@leagueId int
AS

DECLARE @SeasonCount INT = (SELECT COUNT(DISTINCT seasonindex) FROM   tblTeamStandings where leagueid = @leagueId) -1;

DECLARE @MaxSeasonIndex INT = (SELECT TOP 1 calendarYear FROM tblTeamStandings WHERE leagueid = @leagueId ORDER BY calendarYear DESC);

SELECT rosterId, 
       playerid, 
	   p.teamId,
	   teamName,
       position, 
       firstname, 
       lastName, 
       yearsPro, 
       isRetired, 
       rookieYear, 
       draftRound, 
       draftPick, 
       (rookieYear - @SeasonCount) AS Rookie,
       CASE
           WHEN draftRound > 8
           THEN 64
           ELSE draftRound - 1
       END AS [Round],
       CASE
           WHEN draftPick > 33
           THEN 64
           ELSE draftPick - 1
       END AS [Pick]
FROM [tblPlayerProfile] p
WHERE p.leagueid = @leagueId
      AND yearsPro = 4
      AND draftRound < 64
	  AND rookieYear > @MaxSeasonIndex
	  AND isRetired = 0
ORDER BY Rookie DESC, 
         draftRound, 
         draftPick;

		 