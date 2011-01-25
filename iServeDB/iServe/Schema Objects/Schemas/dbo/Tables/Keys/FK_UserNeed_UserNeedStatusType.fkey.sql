ALTER TABLE [dbo].[UserNeed]
    ADD CONSTRAINT [FK_UserNeed_UserNeedStatusType] FOREIGN KEY ([UserNeedStatusID]) REFERENCES [dbo].[UserNeedStatus] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

