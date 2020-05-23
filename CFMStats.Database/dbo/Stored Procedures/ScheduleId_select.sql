-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date:	2019 JAN 23
-- Description:	Get Schedule Id
-- =============================================

CREATE PROCEDURE [dbo].[ScheduleId_select] @stageIndex  INT, 
								   @seasonIndex INT, 
								   @weekIndex   INT, 
								   @teamId      INT, 
								   @leagueId    INT
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
			scheduleId, 
			awayTeamId, 
			homeTeamId
			
			FROM 
				tblschedule
			
			WHERE leagueid = @leagueId
				 AND seasonindex = @seasonIndex
				 AND weekindex = @weekIndex
				 AND stageIndex = @stageIndex
				 AND (awayteamid = @teamId
					 OR hometeamid = @teamId);
    END;