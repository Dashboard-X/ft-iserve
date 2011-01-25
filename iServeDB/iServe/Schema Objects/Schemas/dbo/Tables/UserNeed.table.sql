CREATE TABLE [dbo].[UserNeed] (
    [UserID]            INT      NOT NULL,
    [NeedID]            INT      NOT NULL,
    [UserNeedStatusID]  INT      NULL,
    [IsSubscribed]      BIT      NOT NULL,
    [HasBeenRated]      BIT      NOT NULL,
    [HasRatedSubmitter] BIT      NOT NULL,
    [Created]           DATETIME NOT NULL,
    [CreatedBy]         INT      NULL,
    [Updated]           DATETIME NULL,
    [UpdatedBy]         INT      NULL
);

