-- Update teamName on Player Profile
UPDATE p
  SET 
      p.teamName = t.displayName
FROM tblPlayerProfile p
     INNER JOIN tblTeamInfo t ON t.teamId = p.teamId
WHERE t.leagueId = 1085;