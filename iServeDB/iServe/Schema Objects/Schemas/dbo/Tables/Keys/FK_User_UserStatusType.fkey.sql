ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [FK_User_UserStatusType] FOREIGN KEY ([UserStatusID]) REFERENCES [dbo].[UserStatus] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

