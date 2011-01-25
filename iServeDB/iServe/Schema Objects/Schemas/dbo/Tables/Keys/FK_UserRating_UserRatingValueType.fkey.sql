ALTER TABLE [dbo].[UserRating]
    ADD CONSTRAINT [FK_UserRating_UserRatingValueType] FOREIGN KEY ([UserRatingValueID]) REFERENCES [dbo].[UserRatingValue] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

