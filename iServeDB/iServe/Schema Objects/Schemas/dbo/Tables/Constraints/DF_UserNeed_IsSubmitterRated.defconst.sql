ALTER TABLE [dbo].[UserNeed]
    ADD CONSTRAINT [DF_UserNeed_IsSubmitterRated] DEFAULT ((0)) FOR [HasRatedSubmitter];

