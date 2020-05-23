
MERGE INTO tblPlayerContract AS TARGET
USING
(
    SELECT *
    FROM tblPlayer
) AS SOURCE
ON(SOURCE.playerid = TARGET.playerId)
    WHEN MATCHED
    THEN UPDATE SET 
                    ModifiedOn = GETUTCDATE(), 
                    capHit = SOURCE.capHit, 
                    capReleaseNetSavings = SOURCE.capReleaseNetSavings, 
                    capReleasePenalty = SOURCE.capReleasePenalty, 
                    contractBonus = SOURCE.contractBonus, 
                    contractLength = SOURCE.contractLength, 
                    contractSalary = SOURCE.contractSalary, 
                    contractYearsLeft = SOURCE.contractYearsLeft, 
                    desiredBonus = SOURCE.desiredBonus, 
                    desiredLength = SOURCE.desiredLength, 
                    desiredSalary = SOURCE.desiredSalary, 
                    reSignStatus = SOURCE.reSignStatus
    WHEN NOT MATCHED BY TARGET
    THEN
      INSERT(playerId, 
             leagueId, 
             CreatedOn, 
             ModifiedOn, 
             capHit, 
             capReleaseNetSavings, 
             capReleasePenalty, 
             contractBonus, 
             contractLength, 
             contractSalary, 
             contractYearsLeft, 
             desiredBonus, 
             desiredLength, 
             desiredSalary, 
             reSignStatus)
      VALUES
(SOURCE.playerId, 
 SOURCE.leagueId, 
 GETUTCDATE(), -- use lastUpdatedOn
 GETUTCDATE(), -- use lastUpdatedOn
 SOURCE.capHit, 
 SOURCE.capReleaseNetSavings, 
 SOURCE.capReleasePenalty, 
 SOURCE.contractBonus, 
 SOURCE.contractLength, 
 SOURCE.contractSalary, 
 SOURCE.contractYearsLeft, 
 SOURCE.desiredBonus, 
 SOURCE.desiredLength, 
 SOURCE.desiredSalary, 
 SOURCE.reSignStatus
);