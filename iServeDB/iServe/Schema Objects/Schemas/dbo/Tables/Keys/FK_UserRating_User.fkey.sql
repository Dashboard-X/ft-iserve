﻿ALTER TABLE [dbo].[UserRating]
    ADD CONSTRAINT [FK_UserRating_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
