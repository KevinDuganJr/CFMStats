ALTER TABLE [dbo].[tblPlayerRatings]
ADD CONSTRAINT [FK_dbo.tblPlayerProfile_dbo.tblPlayerProfile] FOREIGN KEY([PlayerId]) REFERENCES [dbo].[tblPlayerProfile]([PlayerId]);

ALTER TABLE [dbo].[tblPlayerContract]
ADD CONSTRAINT [FK_dbo.tblPlayerContract_dbo.tblPlayerProfile] FOREIGN KEY([PlayerId]) REFERENCES [dbo].[tblPlayerProfile]([PlayerId]);

ALTER TABLE [dbo].[tblPlayerTraits]
ADD CONSTRAINT [FK_dbo.tblPlayerTraits_dbo.tblPlayerProfile] FOREIGN KEY([PlayerId]) REFERENCES [dbo].[tblPlayerProfile]([PlayerId]);

ALTER TABLE [dbo].[tblPlayerGrades]
ADD CONSTRAINT [FK_dbo.tblPlayerGrades_dbo.tblPlayerProfile] FOREIGN KEY([PlayerId]) REFERENCES [dbo].[tblPlayerProfile]([PlayerId]);

