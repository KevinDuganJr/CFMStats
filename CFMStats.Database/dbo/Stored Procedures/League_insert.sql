-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 OCT 06
-- Description:	Create League
-- =============================================
create PROCEDURE [dbo].[League_insert]
	 @ownerUserID nvarchar(128),
	 @leagueName nvarchar(50),
	 @exportID varchar(5)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO tblLeague
           (lastUpdatedOn,             
		    Name,            
		    exportID,            
		    ownerUserID,            
		    isActive)
     VALUES 
           (GETUTCDATE(),            
		    @leagueName,
			@exportID,		    
		    @ownerUserID, 
		    1);
		   
END
