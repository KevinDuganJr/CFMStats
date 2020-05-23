Print 'Starting DataLoad for Database'

-- Populate Tables with Default Values
-- NOTE: ORDER MATTERS!


:r Injuries.sql
:r PositionGroups.sql
:r TeamInfo.sql

-- do this last!
--:r FixSeasonIndex.sql

PRINT 'Finished DataLoad for Database'