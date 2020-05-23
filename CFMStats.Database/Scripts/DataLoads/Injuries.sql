Print 'Data Load: Adding Injury Titles'

-- Prepare the Default Data to Merge 
--------------------------------------------------

DECLARE @InjuriesDataLoad TABLE
(	
	[ID] INT NOT NULL, 
	[InjuryName] NVARCHAR(25) NOT NULL
)
 
  -- Define Injury Titles to be added
 INSERT INTO @InjuriesDataLoad([Id], [InjuryName])
    SELECT 1, N'1' UNION ALL
    SELECT 2, N'2' UNION ALL
    SELECT 3, N'3' UNION ALL
    SELECT 4, N'4' UNION ALL
    SELECT 8, N'8' UNION ALL
    SELECT 9, N'9' UNION ALL
    SELECT 10, N'10' UNION ALL
    SELECT 11, N'11' UNION ALL
    SELECT 15, N'15' UNION ALL
    SELECT 17, N'17' UNION ALL
    SELECT 20, N'20' UNION ALL
    SELECT 21, N'21' UNION ALL
    SELECT 22, N'22' UNION ALL
    SELECT 23, N'Dislocated Elbow' UNION ALL
    SELECT 25, N'25' UNION ALL
    SELECT 26, N'26' UNION ALL
    SELECT 27, N'27' UNION ALL
    SELECT 28, N'28' UNION ALL
    SELECT 32, N'32' UNION ALL
    SELECT 33, N'33' UNION ALL
    SELECT 34, N'34' UNION ALL
    SELECT 35, N'35' UNION ALL
    SELECT 36, N'36' UNION ALL
    SELECT 37, N'37' UNION ALL
    SELECT 40, N'40' UNION ALL
    SELECT 41, N'41' UNION ALL
    SELECT 43, N'43' UNION ALL
    SELECT 44, N'44' UNION ALL
    SELECT 45, N'45' UNION ALL
    SELECT 46, N'46' UNION ALL
    SELECT 47, N'47' UNION ALL
    SELECT 5, N'Achilles Tear' UNION ALL
    SELECT 6, N'Dislocated Ankle' UNION ALL
    SELECT 7, N'Dislocated Ankle' UNION ALL
    SELECT 12, N'Forearm Fracture' UNION ALL
    SELECT 13, N'Torn Bicep' UNION ALL
    SELECT 16, N'Upper Arm Fracture' UNION ALL
    SELECT 19, N'Ruptured Disk' UNION ALL
    SELECT 24, N'Dislocated Elbow' UNION ALL
    SELECT 29, N'Broken Toe' UNION ALL
    SELECT 31, N'Foot Fracture' UNION ALL
    SELECT 39, N'Broken Hand' UNION ALL
    SELECT 49, N'Dislocated Hip' UNION ALL
    SELECT 57, N'Partial ACL Tear' UNION ALL
    SELECT 58, N'Knee Cartilage Tear' UNION ALL
    SELECT 59, N'Dislocated Knee' UNION ALL
    SELECT 64, N'Fractured Knee Cap' UNION ALL
    SELECT 72, N'Pulled Groin' UNION ALL
    SELECT 75, N'Hamstring Tear' UNION ALL
    SELECT 77, N'Quad Tear' UNION ALL
    SELECT 84, N'Abdominal Tear' UNION ALL
    SELECT 85, N'Broken Ribs' UNION ALL
    SELECT 86, N'Broken Collarbone' UNION ALL
    SELECT 87, N'Torn Pectoral' UNION ALL
    SELECT 92, N'Shoulder Tear' UNION ALL
    SELECT 93, N'Fractured Shoulder Blade' UNION ALL
    SELECT 94, N'Torn Rotator Cuff' UNION ALL
    SELECT 95, N'Shoulder Tear' UNION ALL
    SELECT 14, N'Torn Tricep' UNION ALL
    SELECT 18, N'Ruptured Disk' UNION ALL
    SELECT 73, N'Broken Fibula' UNION ALL
    SELECT 38, N'Broken Thumb' UNION ALL
    SELECT 30, N'Foot Fracture' UNION ALL
    SELECT 42, N'Broken Wrist' UNION ALL
    SELECT 48, N'Dislocated Hip' UNION ALL
    SELECT 50, N'50' UNION ALL
    SELECT 51, N'Fractured Hip' UNION ALL
    SELECT 52, N'52' UNION ALL
    SELECT 53, N'53' UNION ALL
    SELECT 54, N'54' UNION ALL
    SELECT 55, N'55' UNION ALL
    SELECT 56, N'56' UNION ALL
    SELECT 60, N'Partial MCL Tear' UNION ALL
    SELECT 61, N'Complete ACL Tear' UNION ALL
    SELECT 62, N'Partial PCL Tear' UNION ALL
    SELECT 66, N'Complete PCL Tear' UNION ALL
    SELECT 63, N'63' UNION ALL
    SELECT 65, N'65' UNION ALL
    SELECT 67, N'67' UNION ALL
    SELECT 68, N'68' UNION ALL
    SELECT 69, N'69' UNION ALL
    SELECT 70, N'70' UNION ALL
    SELECT 71, N'71' UNION ALL
    SELECT 74, N'74' UNION ALL
    SELECT 76, N'76' UNION ALL
    SELECT 78, N'78' UNION ALL
    SELECT 79, N'79' UNION ALL
    SELECT 80, N'80' UNION ALL
    SELECT 81, N'81' UNION ALL
    SELECT 82, N'82' UNION ALL
    SELECT 83, N'83' UNION ALL
    SELECT 88, N'88' UNION ALL
    SELECT 89, N'89' UNION ALL
    SELECT 90, N'90' UNION ALL
    SELECT 91, N'91' UNION ALL
    SELECT 96, N'96' UNION ALL
    SELECT 97, N'97' UNION ALL
    SELECT 98, N'98' UNION ALL
    SELECT 99, N'99'


-- Merge Data Into Target Using Table Variable as Source
--------------------------------------------------

 MERGE INTO [dbo].[tblInjury] AS TARGET

Using (
	SELECT				
						x.[ID] AS [ID],
						x.[InjuryName] AS [InjuryName]
	
	FROM				@InjuriesDataLoad x
)   AS SOURCE ([ID], [InjuryName])
	ON TARGET.[ID] = SOURCE.[ID]
WHEN MATCHED THEN
	UPDATE SET
	[InjuryName] = SOURCE.[InjuryName]
WHEN NOT MATCHED BY TARGET THEN
	INSERT([ID], [InjuryName])
	VALUES([ID], [InjuryName])
WHEN NOT MATCHED BY SOURCE THEN
	DELETE
;