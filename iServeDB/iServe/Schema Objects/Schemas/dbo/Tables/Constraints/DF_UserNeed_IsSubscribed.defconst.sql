ALTER TABLE [dbo].[UserNeed]
    ADD CONSTRAINT [DF_UserNeed_IsSubscribed] DEFAULT ((0)) FOR [IsSubscribed];

