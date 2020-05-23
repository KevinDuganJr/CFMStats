
MERGE INTO tblPlayerGrades AS TARGET
USING
(
    SELECT *
    FROM tblPlayer
) AS SOURCE
ON(SOURCE.playerid = TARGET.playerId)
    WHEN MATCHED
    THEN UPDATE SET 
                    ModifiedOn = GETUTCDATE(), 
                    durabilityGrade = SOURCE.durabilityGrade, 
                    intangibleGrade = SOURCE.intangibleGrade, 
                    physicalGrade = SOURCE.physicalGrade, 
                    productionGrade = SOURCE.productionGrade, 
                    sizeGrade = SOURCE.sizeGrade
    WHEN NOT MATCHED BY TARGET
    THEN
      INSERT(playerId, 
             leagueId, 
             CreatedOn, 
             ModifiedOn, 
             durabilityGrade, 
             intangibleGrade, 
             physicalGrade, 
             productionGrade, 
             sizeGrade)
      VALUES
			(SOURCE.playerId, 
			 SOURCE.leagueId, 
			 GETUTCDATE(), 
			 GETUTCDATE(), 
			 SOURCE.durabilityGrade, 
			 SOURCE.intangibleGrade, 
			 SOURCE.physicalGrade, 
			 SOURCE.productionGrade, 
			 SOURCE.sizeGrade
			);