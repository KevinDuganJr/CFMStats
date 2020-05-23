-- =============================================
-- Author:		Kevin J. Dugan Jr
-- Create date: 2017 JUNE 04
-- Description:	Select position groups
-- =============================================
CREATE PROCEDURE [dbo].[PositionGroup_select]
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	/****** Script for SelectTopNRows command from SSMS  ******/
	SELECT DISTINCT(positionGroupID),GroupName
	FROM tblPositionGroup	
		   
END
