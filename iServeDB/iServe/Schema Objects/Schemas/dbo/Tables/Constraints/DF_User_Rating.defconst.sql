ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_Rating] DEFAULT ((0)) FOR [Rating];

