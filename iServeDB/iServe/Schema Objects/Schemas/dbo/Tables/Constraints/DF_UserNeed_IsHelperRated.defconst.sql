ALTER TABLE [dbo].[UserNeed]
    ADD CONSTRAINT [DF_UserNeed_IsHelperRated] DEFAULT ((0)) FOR [HasBeenRated];

