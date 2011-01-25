ALTER TABLE [dbo].[UserNeed]
    ADD CONSTRAINT [DF_UserNeed_UserNeedStatusID] DEFAULT ((1)) FOR [UserNeedStatusID];

