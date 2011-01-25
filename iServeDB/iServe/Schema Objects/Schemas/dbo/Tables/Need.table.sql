CREATE TABLE [dbo].[Need] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [ChurchID]         INT             NOT NULL,
    [CategoryID]       INT             NOT NULL,
    [SubmittedByID]    INT             NOT NULL,
    [Name]             NVARCHAR (100)  NOT NULL,
    [Description]      NVARCHAR (3000) NULL,
    [RequiredDate]     DATETIME        NULL,
    [IsRequiredOnDate] BIT             NOT NULL,
    [PostalCode]       VARCHAR (11)    NULL,
    [ImageName]        VARCHAR (80)    NULL,
    [HelpersNeeded]    INT             NOT NULL,
    [NeedStatusID]     INT             NOT NULL,
    [Created]          DATETIME        NOT NULL,
    [CreatedBy]        INT             NULL,
    [Updated]          DATETIME        NULL,
    [UpdatedBy]        INT             NULL
);

