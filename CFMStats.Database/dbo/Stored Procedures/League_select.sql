-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 SEPT 06
-- Description:	Get League
-- =============================================

CREATE PROCEDURE [dbo].[League_select] @ownerUserID NVARCHAR(128) = NULL, 
							   @leagueID    INT           = NULL
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from
	   -- interfering with SELECT statements.
	   SET NOCOUNT ON;
	   IF @leagueID > 0
		  BEGIN
			 SELECT 
				   lastUpdatedOn, 
				   [Name], 
				   exportID, 
				   ownerUserID
				   FROM 
					   tblLeague
				   WHERE id = @leagueID;
	   END;
	   ELSE
		  BEGIN
			 SELECT 
				   Id, 
				   lastUpdatedOn = (SELECT TOP 1 
									  lastUpdatedOn
									  FROM 
										  tblTeamInfo
									  WHERE leagueId = l.id
									  ORDER BY teamId DESC), 
				   Name, 
				   exportID, 
				   ownerUserID, 
				   isActive, 
				   Users = STUFF(
			 (SELECT 
				    ', ' + userName
				    FROM 
					    tblTeamInfo
				    WHERE leagueId = l.id
						AND userName <> '' FOR
			  XML PATH('')), 1, 1, '')
				   FROM 
					   tblLeague AS l
				   WHERE isActive = 1
				   ORDER BY 
						  lastUpdatedOn DESC;
	   END;
    END;