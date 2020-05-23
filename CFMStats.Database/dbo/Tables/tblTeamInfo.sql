CREATE TABLE [dbo].[tblTeamInfo] (
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
    [userName]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblTeamInfo] PRIMARY KEY CLUSTERED ([teamId] ASC, [leagueId] ASC)
);

