ALTER TABLE [dbo].[Need]
    ADD CONSTRAINT [DF_Need_HelpersNeeded] DEFAULT ((1)) FOR [HelpersNeeded];

