Print 'Data Load: Fix Season Index'

UPDATE tblTeamStandings   SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblSchedule        SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsDefense    SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsPassing    SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsKicking    SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsRushing    SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsReceiving  SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsPunting    SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017
UPDATE tblStatsTeam       SET seasonIndex = seasonIndex + 2017 where seasonIndex < 2017