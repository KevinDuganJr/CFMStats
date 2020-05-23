CREATE TABLE [dbo].[tblLeague] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [lastUpdatedOn] DATETIME       NOT NULL,
    [Name]          VARCHAR (50)   NOT NULL,
    [exportID]      VARCHAR (5)    NOT NULL,
    [ownerUserID]   NVARCHAR (128) NOT NULL,
    [isActive]      BIT            NOT NULL,
    CONSTRAINT [PK_tblLeague] PRIMARY KEY CLUSTERED ([ID] ASC)
);

