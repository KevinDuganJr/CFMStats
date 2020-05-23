Print 'Data Load: Adding Free Agent as Team'

-- Prepare the Default Data to Merge 
--------------------------------------------------

DECLARE @TeaminfoDataLoad TABLE
(	
    [teamId]         INT          NOT NULL,
    [leagueId]       INT          NOT NULL,
    [lastUpdatedOn]  DATETIME     NOT NULL,
    [abbrName]       VARCHAR (5)  NOT NULL,
    [cityName]       VARCHAR (50) NOT NULL,
    [displayName]    VARCHAR (50) NOT NULL,
    [divName]        VARCHAR (50) NOT NULL,
    [offScheme]      INT          NOT NULL,
    [defScheme]      INT          NOT NULL,
    [ovrRating]      INT          NOT NULL,
    [injuryCount]    INT          NOT NULL,
    [primaryColor]   INT          NOT NULL,
    [secondaryColor] INT          NOT NULL,
    [userName]       VARCHAR (50) NOT NULL
)
 
  -- Define values to be added
 INSERT INTO @TeaminfoDataLoad([teamId],[leagueId],[lastUpdatedOn],[abbrName],[cityName],[displayName],[divName],[offScheme],[defScheme],[ovrRating],[injuryCount],[primaryColor],[secondaryColor],[userName])
	   SELECT 0, 1, GETUTCDATE(), N'NFL', N'New york', N'Free Agent', N'NFL', 0, 0, 0, 0, 005599, 995500, N''



-- Merge Data Into Target Using Table Variable as Source
--------------------------------------------------

 MERGE INTO [dbo].[tblTeamInfo] AS TARGET

Using (
	SELECT				
	   x.[teamId] AS [leagueId],
	   x.[leagueId] AS [leagueId],
	   x.[lastUpdatedOn] AS [lastUpdatedOn],
	   x.[abbrName] AS [abbrName],
	   x.[cityName] AS [cityName],
	   x.[displayName] AS [displayName],
	   x.[divName] AS [divName],
	   x.[offScheme] AS [offScheme],
	   x.[defScheme] AS [defScheme],
	   x.[ovrRating] AS [ovrRating],
	   x.[injuryCount] AS [injuryCount],
	   x.[primaryColor] AS [primaryColor],
	   x.[secondaryColor] AS [secondaryColor],
	   x.[userName] AS [userName]

	
	FROM				@TeaminfoDataLoad x
)   AS SOURCE ([teamId],[leagueId],[lastUpdatedOn],[abbrName],[cityName],[displayName],[divName],[offScheme],[defScheme],[ovrRating],[injuryCount],[primaryColor],[secondaryColor],[userName])
	ON TARGET.[TeamId] = SOURCE.[TeamId]
WHEN MATCHED THEN
	UPDATE SET
	   [teamId] = SOURCE.[teamId],
	   [leagueId] = SOURCE.[leagueId],
	   [lastUpdatedOn] = SOURCE.[lastUpdatedOn],
	   [abbrName] = SOURCE.[abbrName],
	   [cityName] = SOURCE.[cityName],
	   [displayName] = SOURCE.[displayName],
	   [divName] = SOURCE.[divName],
	   [offScheme] = SOURCE.[offScheme],
	   [defScheme] = SOURCE.[defScheme],
	   [ovrRating] = SOURCE.[ovrRating],
	   [injuryCount] = SOURCE.[injuryCount],
	   [primaryColor] = SOURCE.[primaryColor],
	   [secondaryColor] = SOURCE.[secondaryColor],
	   [userName] = SOURCE.[userName]


WHEN NOT MATCHED BY TARGET THEN
	INSERT([teamId],[leagueId],[lastUpdatedOn],[abbrName],[cityName],[displayName],[divName],[offScheme],[defScheme],[ovrRating],[injuryCount],[primaryColor],[secondaryColor],[userName])
	VALUES([teamId],[leagueId],[lastUpdatedOn],[abbrName],[cityName],[displayName],[divName],[offScheme],[defScheme],[ovrRating],[injuryCount],[primaryColor],[secondaryColor],[userName])
;