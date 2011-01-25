CREATE TABLE [dbo].[User] (
    [ID]                INT           NOT NULL,
    [ChurchID]          INT           NOT NULL,
    [Name]              NVARCHAR (30) NOT NULL,
    [Rating]            INT           NOT NULL,
    [Email]             VARCHAR (80)  NULL,
    [IsModerator]       BIT           NOT NULL,
    [UserStatusID]      INT           NOT NULL,
    [AccessToken]       VARCHAR (50)  NULL,
    [AccessTokenSecret] VARCHAR (50)  NULL,
    [Created]           DATETIME      NOT NULL,
    [CreatedBy]         INT           NULL,
    [Updated]           DATETIME      NULL,
    [UpdatedBy]         INT           NULL
);

