-- Procedure for Comparing Version Numbers
CREATE PROCEDURE #VersionRequiresUpgrade
	@ExpectedMajor INT,
	@ExpectedMinor INT,
	@ExpectedPatch INT,
	-- Can these be accessed by the parent scope? Should we just call the SQL to get them each time?
	@CurrentMajor INT,
	@CurrentMinor INT,
	@CurrentPatch INT,
	@Result BIT OUTPUT
AS
BEGIN
	IF(@CurrentMajor < @ExpectedMajor)
	BEGIN
		SET @Result = 1;
		RETURN 0;
	END
	IF(@CurrentMajor > @ExpectedMajor)
		SET @Result = 0;

	IF(@CurrentMinor < @ExpectedMinor)
	BEGIN
		SET @Result = 1;
		RETURN 0;
	END
	IF(@CurrentMinor > @ExpectedMinor)
		SET @Result = 0;

	IF(@CurrentPatch < @ExpectedPatch)
	BEGIN
		SET @Result = 1;
		RETURN 0;
	END
	IF(@CurrentPatch > @ExpectedPatch)
		SET @Result = 0;

	-- This should happen only if the version number matches. If so, we'll allow it to upgrade again (assuming a redeploy)
	-- If not, something went wrong
	IF(@CurrentMajor = @ExpectedMajor AND @CurrentMinor = @ExpectedMinor AND @CurrentPatch = @ExpectedPatch)
	BEGIN
		SET @Result = 1;
		RETURN 0;
	END

	SET @Result = 0;
	RETURN 0;
END

GO