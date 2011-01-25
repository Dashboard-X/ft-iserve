ALTER TABLE [dbo].[UserRating]
    ADD CONSTRAINT [FK_UserRating_UserRatingRoleType] FOREIGN KEY ([UserRatingRoleID]) REFERENCES [dbo].[UserRatingRole] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

