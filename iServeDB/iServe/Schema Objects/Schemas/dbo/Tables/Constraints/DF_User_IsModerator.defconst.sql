ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_IsModerator] DEFAULT ((0)) FOR [IsModerator];

