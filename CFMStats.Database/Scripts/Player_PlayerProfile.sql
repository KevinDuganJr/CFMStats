SET IDENTITY_INSERT tblPlayerProfile ON;

MERGE INTO tblPlayerProfile AS TARGET 
USING (SELECT * FROM tblPlayer) AS SOURCE 
ON (SOURCE.playerid = TARGET.playerId) WHEN 

MATCHED THEN UPDATE 

              SET                   
                      TARGET.ModifiedOn = GETUTCDATE(),
                      TARGET.teamId = SOURCE.teamId, 
                      TARGET.positionGroupID = SOURCE.positionGroupID, 
                      TARGET.jerseyNum = SOURCE.jerseyNum, 
                      TARGET.firstName = SOURCE.firstName, 
                      TARGET.lastName = SOURCE.lastName, 
                      TARGET.age = SOURCE.age, 
                      TARGET.injuryLength = SOURCE.injuryLength, 
                      TARGET.injuryType = SOURCE.injuryType, 
                      TARGET.experiencePoints = SOURCE.experiencePoints, 
                      TARGET.height = SOURCE.height, 
                      TARGET.isActive = SOURCE.isActive, 
                      TARGET.isFreeAgent = SOURCE.isFreeAgent, 
                      TARGET.isOnIR = SOURCE.isOnIR, 
                      TARGET.isOnPracticeSquad = SOURCE.isOnPracticeSquad, 
                      TARGET.legacyScore = SOURCE.legacyScore, 
                      TARGET.position = SOURCE.position, 
                      TARGET.[weight] = SOURCE.weight, 
                      TARGET.yearsPro = SOURCE.yearsPro, 
                      TARGET.runStyle = SOURCE.runStyle, 
                      TARGET.scheme = SOURCE.scheme, 
                      TARGET.isRetired = SOURCE.isRetired,
                      TARGET.skillPoints = SOURCE.skillPoints

WHEN
  NOT MATCHED BY TARGET
  THEN 

   INSERT 
                (playerid,
				 leagueId, 
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
                (SOURCE.playerid,
				 SOURCE.leagueId, 
                 SOURCE.rosterId,                  
                 GETUTCDATE(),
                 GETUTCDATE(),
                 SOURCE.teamId, 
                 SOURCE.teamName, 
                 SOURCE.portraitId, 
                 SOURCE.presentationId, 
                 SOURCE.positionGroupID, 
                 SOURCE.jerseyNum, 
                 SOURCE.firstName, 
                 SOURCE.lastName, 
                 SOURCE.age, 
                 SOURCE.injuryLength, 
                 SOURCE.injuryType, 
                 SOURCE.birthDay, 
                 SOURCE.birthMonth, 
                 SOURCE.birthYear, 
                 SOURCE.college, 
                 SOURCE.draftPick, 
                 SOURCE.draftRound, 
                 SOURCE.rookieYear, 
                 SOURCE.playerBestOvr, 
                 SOURCE.experiencePoints, 
                 SOURCE.height, 
                 SOURCE.isActive, 
                 SOURCE.isFreeAgent, 
                 SOURCE.isOnIR, 
                 SOURCE.isOnPracticeSquad, 
                 SOURCE.legacyScore, 
                 SOURCE.position, 
                 SOURCE.weight, 
                 SOURCE.yearsPro, 
                 SOURCE.runStyle, 
                 SOURCE.scheme, 
                 SOURCE.isRetired, 
                 SOURCE.skillPoints
                );


SET IDENTITY_INSERT tblPlayerProfile OFF;