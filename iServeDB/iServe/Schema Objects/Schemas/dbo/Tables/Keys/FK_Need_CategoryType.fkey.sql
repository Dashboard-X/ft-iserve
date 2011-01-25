ALTER TABLE [dbo].[Need]
    ADD CONSTRAINT [FK_Need_CategoryType] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Category] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

