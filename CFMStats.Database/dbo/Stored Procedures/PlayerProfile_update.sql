-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2020 MAR 04
-- Description:	insert or update madden player profile
-- =============================================

CREATE PROCEDURE [dbo].[PlayerProfile_update] @leagueId          INT, 
                                              @rosterId          INT, 
                                              @teamId            INT, 
                                              @portraitId        INT, 
                                              @presentationId    INT, 
                                              @injuryLength      INT, 
                                              @injuryType        INT, 
                                              @age               INT, 
                                              @birthDay          INT, 
                                              @birthMonth        INT, 
                                              @birthYear         INT, 
                                              @college           VARCHAR(50), 
                                              @draftPick         INT, 
                                              @draftRound        INT, 
                                              @rookieYear        INT, 
                                              @experiencePoints  INT, 
                                              @firstName         VARCHAR(50), 
                                              @height            INT, 
                                              @isActive          BIT, 
                                              @isFreeAgent       BIT, 
                                              @isOnIR            BIT, 
                                              @isOnPracticeSquad BIT, 
                                              @jerseyNum         INT, 
                                              @lastName          VARCHAR(50), 
                                              @legacyScore       INT, 
                                              @playerBestOvr     INT, 
                                              @position          VARCHAR(10), 
                                              @weight            INT, 
                                              @yearsPro          INT, 
                                              @runStyle          INT, 
                                              @scheme            INT, 
                                              @skillPoints       INT
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @positionGroupID INT;
        SET @positionGroupID =
        (
            SELECT positionGroupID
            FROM tblPositionGroup
            WHERE Position = @position
        );
        DECLARE @playerId INT= 0;
        SET @playerId =
        (
            SELECT TOP 1 playerId
            FROM tblPlayerProfile
            WHERE leagueId = @leagueId
                  AND rosterId = @rosterId
                  AND birthYear = @birthYear
                  AND birthMonth = @birthMonth
                  AND birthDay = @birthDay
            ORDER BY playerId ASC
        );
        DECLARE @teamName VARCHAR(50);
        IF(@isFreeAgent = 1)
            BEGIN
                SET @teamName = 'Free Agent';
        END;
            ELSE
            BEGIN
                SET @teamName =
                (
                    SELECT displayName
                    FROM tblTeamInfo
                    WHERE teamId = @teamId
                          AND leagueId = @leagueId
                );
        END;
        IF(@playerId > 0)
            BEGIN
                UPDATE tblPlayerProfile
                  SET                   
                      ModifiedOn = GETUTCDATE(),
                      teamId = @teamId, 
                      positionGroupID = @positionGroupID, 
                      jerseyNum = @jerseyNum, 
                      firstName = @firstName, 
                      lastName = @lastName, 
                      age = @age, 
                      injuryLength = @injuryLength, 
                      injuryType = @injuryType, 
                      experiencePoints = @experiencePoints, 
                      height = @height, 
                      isActive = @isActive, 
                      isFreeAgent = @isFreeAgent, 
                      isOnIR = @isOnIR, 
                      isOnPracticeSquad = @isOnPracticeSquad, 
                      legacyScore = @legacyScore, 
                      position = @position, 
                      [weight] = @weight, 
                      yearsPro = @yearsPro, 
                      runStyle = @runStyle, 
                      scheme = @scheme, 
                      isRetired = 0, 
                      skillPoints = @skillPoints
                WHERE playerId = @playerId
                      AND leagueId = @leagueId;
        END;
            ELSE
            BEGIN
                DECLARE @SeasonCount INT = (SELECT COUNT(DISTINCT seasonindex) FROM tblTeamStandings WHERE leagueid = @leagueId) - 1;
                
                SET @rookieYear = (CASE WHEN @yearsPro = 0 THEN (@rookieYear - @SeasonCount) ELSE @rookieYear END);
                SET @draftRound = (CASE WHEN @draftRound > 8 THEN 64 ELSE @draftRound - 1 END);
                SET @draftPick = (CASE WHEN @draftRound = 64 THEN @draftPick ELSE @draftPick - 1 END);

                INSERT INTO tblPlayerProfile
                (leagueId, 
                 rosterId,                  
                 CreatedOn,
                 ModifiedOn,
                 teamId, 
                 teamName, 
                 portraitId, 
                 presentationId, 
                 positionGroupID, 
                 jerseyNum, 
                 firstName, 
                 lastName, 
                 age, 
                 injuryLength, 
                 injuryType, 
                 birthDay, 
                 birthMonth, 
                 birthYear, 
                 college, 
                 draftPick, 
                 draftRound, 
                 rookieYear, 
                 rookieRating, 
                 experiencePoints, 
                 height, 
                 isActive, 
                 isFreeAgent, 
                 isOnIR, 
                 isOnPracticeSquad, 
                 legacyScore, 
                 position, 
                 [weight], 
                 yearsPro, 
                 runStyle, 
                 scheme, 
                 isRetired, 
                 skillPoints
                )
                VALUES
                (@leagueId, 
                 @rosterId,                  
                 GETUTCDATE(),
                 GETUTCDATE(),
                 @teamId, 
                 @teamName, 
                 @portraitId, 
                 @presentationId, 
                 @positionGroupID, 
                 @jerseyNum, 
                 @firstName, 
                 @lastName, 
                 @age, 
                 @injuryLength, 
                 @injuryType, 
                 @birthDay, 
                 @birthMonth, 
                 @birthYear, 
                 @college, 
                 @draftPick, 
                 @draftRound, 
                 @rookieYear, 
                 @playerBestOvr, 
                 @experiencePoints, 
                 @height, 
                 @isActive, 
                 @isFreeAgent, 
                 @isOnIR, 
                 @isOnPracticeSquad, 
                 @legacyScore, 
                 @position, 
                 @weight, 
                 @yearsPro, 
                 @runStyle, 
                 @scheme, 
                 0, 
                 @skillPoints
                );
        END;
    END;