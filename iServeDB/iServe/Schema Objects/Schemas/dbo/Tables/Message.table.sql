CREATE TABLE [dbo].[Message] (
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    [ChurchID]   INT             NOT NULL,
    [NeedID]     INT             NOT NULL,
    [Body]       NVARCHAR (3900) NOT NULL,
    [FromUserID] INT             NOT NULL,
    [ToUserID]   INT             NULL,
    [Created]    DATETIME        NOT NULL,
    [CreatedBy]  INT             NULL,
    [Updated]    DATETIME        NULL,
    [UpdatedBy]  INT             NULL
);

