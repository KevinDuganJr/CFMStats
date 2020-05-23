-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 16
-- Description:	insert or update madden team information
-- =============================================
CREATE PROCEDURE [dbo].[TeamInfo_update]
		@teamId int,
		@abbrName varchar(50),
		@cityName varchar(50),
		@displayName varchar(50),
		@divName varchar(50),
		@offScheme int,
		@defScheme int,
		@ovrRating int,
		@injuryCount int,
		@primaryColor int,
		@secondaryColor int,
		@userName varchar(50),
		@leagueId int
     

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  IF EXISTS (SELECT * FROM tblTeamInfo WHERE teamID = @teamId  AND leagueId = @leagueId  )
      UPDATE tblTeamInfo
      SET 
		lastUpdatedOn = GETUTCDATE(),
		abbrName = @abbrName,
		cityName = @cityName,
		displayName = @displayName,
		divName = @divName,
		offScheme = @offScheme,
		defScheme = @defScheme,
		ovrRating = @ovrRating,
		injuryCount = @injuryCount,
		primaryColor = @primaryColor,
		secondaryColor = @secondaryColor,
		userName = @userName		 

	  WHERE teamID = @teamId
	  AND leagueId = @leagueId 

   ELSE
		INSERT INTO tblTeamInfo
				   (leagueId,
				   teamId,
				   lastUpdatedOn,
				   abbrName,
				   cityName,
				   displayName,
				   divName,
				   offScheme,
				   defScheme,
				   ovrRating,
				   injuryCount,
				   primaryColor,
				   secondaryColor,
				   userName)
			 VALUES
				   (@leagueId,
				   @teamId, 
				   GETUTCDATE(),
				   @abbrName,
				   @cityName,
				   @displayName, 
				   @divName, 
				   @offScheme,
				   @defScheme,
				   @ovrRating,
				   @injuryCount,
				   @primaryColor,
				   @secondaryColor,
				   @userName) 
		   
END
