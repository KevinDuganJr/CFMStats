CREATE TABLE [dbo].[tblPositionGroup] (
    [ID]              INT          NOT NULL,
    [positionGroupID] INT         NOT NULL,
    [GroupName]       VARCHAR (5) CONSTRAINT [DF_tblPositionGroup_Group] DEFAULT ('null') NOT NULL,
    [Position]        VARCHAR (5) NOT NULL,
    CONSTRAINT [PK_tblPositionGroup] PRIMARY KEY CLUSTERED ([ID] ASC)
);

