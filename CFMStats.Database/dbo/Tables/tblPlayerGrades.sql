CREATE TABLE [dbo].[tblPlayerGrades]
(
    [playerId]                  INT NOT NULL, 
    [CreatedOn]                 DATETIME2 NOT NULL,
    [ModifiedOn]                DATETIME2 NOT NULL,
    [leagueId]                  INT NOT NULL, 
    [durabilityGrade]           INT NOT NULL, 
    [intangibleGrade]           INT NOT NULL, 
    [physicalGrade]             INT NOT NULL, 
    [productionGrade]           INT NOT NULL, 
    [sizeGrade]                 INT NOT NULL, 
    PRIMARY KEY ([playerId])
 )
