CREATE TABLE [dbo].[ConfigSetting] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [Value]     VARCHAR (500) NOT NULL,
    [Created]   DATETIME      NULL,
    [CreatedBy] INT           NULL,
    [Updated]   DATETIME      NULL,
    [UpdatedBy] INT           NULL
);

