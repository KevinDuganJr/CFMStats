-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 AUGUST 15
-- Description:	Find blank injury name
-- =============================================

CREATE PROCEDURE [dbo].[FindInjury_Select]
AS
    BEGIN
	   -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	   SET NOCOUNT ON;
	   SELECT 
			p.injurytype, 
			p.firstname, 
			p.lastname, 
			t.displayname, 
			j.InjuryName
			FROM 
				[tblPlayerProfile] AS P
			JOIN tblTeamInfo AS T
				ON P.teamId = T.teamid
			LEFT JOIN tblInjury AS J
				ON p.injurytype = j.id
			WHERE P.injuryType <> 97
				 AND p.isactive = 0
				 AND LEN(J.InjuryName) = 2
			ORDER BY 
				    t.displayName;
    END;