-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 24
-- Description:	Select current week
-- =============================================
CREATE PROCEDURE [dbo].[ScheduleCurrentWeek_select]
				@leagueId int
 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;



	IF EXISTS (SELECT TOP 1 seasonIndex, weekIndex, stageIndex, [status]
				FROM tblSchedule 
				WHERE  leagueId = @leagueId
				AND [status] = 1)
	BEGIN
		SELECT TOP 1 seasonIndex, weekIndex, stageIndex, [status]
		FROM tblSchedule 
		WHERE  leagueId = @leagueId
		AND [status] = 1
		ORDER BY seasonIndex DESC, stageIndex ASC, [status] ASC, weekIndex ASC;
	END
	ELSE
	BEGIN
		SELECT TOP 1 seasonIndex, weekIndex, stageIndex, [status]
		FROM tblSchedule 
		WHERE  leagueId = @leagueId
		ORDER BY seasonIndex DESC, weekIndex DESC;
	END;
 
			   
END;
 