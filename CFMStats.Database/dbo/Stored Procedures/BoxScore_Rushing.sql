-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2019 JAN 20
-- Description:	Select rushing stats
-- =============================================

create PROCEDURE [dbo].[BoxScore_Rushing] @stageIndex  INT, 
								    @seasonIndex INT, 
								    @weekindex   INT = NULL, 
								    @teamId      INT = NULL, 
								    @leagueId    INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   DECLARE @scheduleId INT;
	   

	   SET @scheduleId = (SELECT 
					 scheduleId
					 FROM 
						 tblStatsTeam
					 WHERE leagueId = @leagueId
						  AND stageIndex = @stageIndex
						  AND seasonIndex = @seasonIndex
						  AND weekIndex = @weekindex
						  AND TeamID = @teamId);
	    
	   SELECT  
			t.displayName AS teamName, 
			s.playerid, 
			p.firstName, 
			p.lastName, 
			p.position, 
			s.fullName,			
			(rush20PlusYds) AS '20Plus', 
			(rushAtt) AS 'Attempt', 
			(rushBrokenTackles) AS 'BrokenTackle', 
			(rushFum) AS 'Fumble', 
			(rushYds) AS 'Yards', 
			(rushYdsAfterContact) AS 'YardsAfterContact', 
			(rushTDs) AS 'Touchdown', 
			(rushLongest) AS 'Longest'
			FROM 
				tblStatsRushing AS s
			LEFT JOIN tblTeamInfo AS t
				ON t.teamId = s.teamId
				   AND t.leagueId = s.leagueId

			LEFT JOIN tblPlayerProfile AS p
				ON p.playerId = s.playerId
				   AND p.leagueId = s.leagueId

			WHERE s.stageIndex = @stageIndex
				 AND s.seasonIndex = @seasonIndex
				 AND s.weekIndex = @weekIndex 
				 AND s.leagueId = @leagueId
				 AND s.scheduleId = @scheduleId
				-- AND s.playerId > 0

			ORDER BY 
				    Yards DESC;
    END;