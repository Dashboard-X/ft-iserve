CREATE TABLE [dbo].[UserRating] (
    [ID]                INT      IDENTITY (1, 1) NOT NULL,
    [ChurchID]          INT      NOT NULL,
    [UserID]            INT      NOT NULL,
    [NeedID]            INT      NOT NULL,
    [RatedByID]         INT      NOT NULL,
    [UserRatingRoleID]  INT      NOT NULL,
    [UserRatingValueID] INT      NOT NULL,
    [Created]           DATETIME NOT NULL,
    [CreatedBy]         INT      NULL,
    [Updated]           DATETIME NULL,
    [UpdatedBy]         INT      NULL
);

