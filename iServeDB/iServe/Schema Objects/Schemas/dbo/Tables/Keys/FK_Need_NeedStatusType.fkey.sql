ALTER TABLE [dbo].[Need]
    ADD CONSTRAINT [FK_Need_NeedStatusType] FOREIGN KEY ([NeedStatusID]) REFERENCES [dbo].[NeedStatus] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

