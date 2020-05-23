-- Database Deployment Variables
------------------------------------------------------
------------------------------------------------------
DECLARE @DataLoadUser NVARCHAR(255) = 'Automated Data Load'
DECLARE @DataLoadDateTime DATETIME2 = GETUTCDATE()
DECLARE @Environment NVARCHAR(32) = '$(Environment)'
DECLARE @OverwriteDataLoads INT = (SELECT COALESCE($(OverwriteDataLoads), 0))

IF(@Environment != 'local')
BEGIN
	-- If we aren't local, we don't even want to risk an overwrite scenario
	SET @OverwriteDataLoads = 0
END

-- Database Version Variables
------------------------------------------------------
------------------------------------------------------

-- Setting default version number for when there isn't a table to pull it from
DECLARE @CurrentMajorVersion INT = 1
DECLARE @CurrentMinorVersion INT = 0
DECLARE @CurrentPatchVersion INT = 0

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo'
                 AND  TABLE_NAME = '__DatabaseVersions'))
BEGIN
	-- TODO: remove this before going like with 1.5
	-- THIS IS A HACK TO CLEANUP LOCAL AND DEV
	IF(@Environment = 'local' OR @Environment = 'dev')
	BEGIN
		DELETE FROM dbo.__DatabaseVersions WHERE Major = 1 AND Minor = 5 AND Patch = 0
	END

	SELECT TOP 1		@CurrentMajorVersion = Major,
						@CurrentMinorVersion = Minor,
						@CurrentPatchVersion = Patch
	FROM				[dbo].[__DatabaseVersions]
	ORDER BY			Major DESC, Minor DESC, Patch DESC, LastDeployedDateTime DESC
END
ELSE
	PRINT 'Could not find __DatabaseVersions table!'

PRINT 'Found database version ' + CAST(@CurrentMajorVersion AS NVARCHAR(10)) + '.' + CAST(@CurrentMinorVersion AS NVARCHAR(10)) + '.' + CAST(@CurrentPatchVersion AS NVARCHAR(10))

DECLARE @VersionCheck BIT = 0