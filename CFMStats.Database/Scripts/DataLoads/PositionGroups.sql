Print 'Data Load: Adding Position Groups'

-- Prepare the Default Data to Merge 
--------------------------------------------------

DECLARE @PositionGroupsDataLoad TABLE
(	
	[ID] INT NOT NULL, 
	[positionGroupID] INT NOT NULL, 
	[GroupName] NVARCHAR(5) NOT NULL, 
	[Position] NVARCHAR(5) NOT NULL
)
 
  -- Define values to be added
 INSERT INTO @PositionGroupsDataLoad([ID], [positionGroupID], [GroupName], [Position])
	   SELECT 1, 1, N'QB', N'QB' UNION ALL
	   SELECT 2, 2, N'HB', N'HB' UNION ALL
	   SELECT 3, 3, N'FB', N'FB' UNION ALL
	   SELECT 4, 4, N'WR', N'WR' UNION ALL
	   SELECT 5, 5, N'TE', N'TE' UNION ALL
	   SELECT 6, 6, N'OL', N'LT' UNION ALL
	   SELECT 7, 6, N'OL', N'LG' UNION ALL
	   SELECT 8, 6, N'OL', N'C' UNION ALL
	   SELECT 9, 6, N'OL', N'RG' UNION ALL
	   SELECT 10, 6, N'OL', N'RT' UNION ALL
	   SELECT 11, 7, N'DL', N'RE' UNION ALL
	   SELECT 12, 7, N'DL', N'DT' UNION ALL
	   SELECT 13, 7, N'DL', N'LE' UNION ALL
	   SELECT 14, 8, N'LB', N'LOLB' UNION ALL
	   SELECT 15, 8, N'LB', N'MLB' UNION ALL
	   SELECT 16, 8, N'LB', N'ROLB' UNION ALL
	   SELECT 17, 9, N'CB', N'CB' UNION ALL
	   SELECT 18, 10, N'S', N'FS' UNION ALL
	   SELECT 19, 10, N'S', N'SS' UNION ALL
	   SELECT 20, 11, N'ST', N'K' UNION ALL
	   SELECT 21, 11, N'ST', N'P' 



-- Merge Data Into Target Using Table Variable as Source
--------------------------------------------------

 MERGE INTO [dbo].[tblPositionGroup] AS TARGET

Using (
	SELECT				
						x.[ID] AS [ID],
						x.[positionGroupID] AS [positionGroupID],
						x.[GroupName] AS [GroupName],
						x.[Position] AS [Position]
	
	FROM				@PositionGroupsDataLoad x
)   AS SOURCE ([ID], [positionGroupID], [GroupName], [Position])
	ON TARGET.[ID] = SOURCE.[ID]
WHEN MATCHED THEN
	UPDATE SET
	[positionGroupID] = SOURCE.[positionGroupID],
	[GroupName] = SOURCE.[GroupName],
	[Position] = SOURCE.[Position]

WHEN NOT MATCHED BY TARGET THEN
	INSERT([ID], [positionGroupID], [GroupName], [Position])
	VALUES([ID], [positionGroupID], [GroupName], [Position])
--WHEN NOT MATCHED BY SOURCE THEN
	--DELETE
;