USE [iServe];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

--ConfigSetting
SET IDENTITY_INSERT [dbo].[ConfigSetting] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[ConfigSetting]([ID], [Name], [Value], [Created], [CreatedBy], [Updated], [UpdatedBy])
SELECT 1, N'AppVersion', N'1.0', '20090522 11:40:30.933', 1, NULL, NULL UNION ALL
SELECT 2, N'DomainName', N'iwanttoserve.com', '20090522 11:40:30.960', 1, NULL, NULL UNION ALL
SELECT 3, N'DefaultChurchCode', N'www', '20090522 11:40:30.970', 1, NULL, NULL UNION ALL
SELECT 4, N'RecordsPerPage', N'10', '20090522 11:40:30.977', 1, NULL, NULL UNION ALL
SELECT 5, N'SMTPServer', N'mail.iwanttoserve.com', '20090522 11:40:30.987', 1, NULL, NULL UNION ALL
SELECT 6, N'SMTPUsername', N'support@iwanttoserve.com', '20090522 11:40:30.993', 1, NULL, NULL UNION ALL
SELECT 7, N'SMTPPassword', N'iserveemail', '20090522 11:40:31.003', 1, NULL, NULL UNION ALL
SELECT 8, N'FromEmail', N'noreply@iwanttoserve.com', '20090522 11:40:31.010', 1, NULL, NULL UNION ALL
SELECT 9, N'BaseAPIUrl', N'staging.fellowshiponeapi.com', '20090522 11:40:31.020', 1, NULL, NULL UNION ALL
SELECT 10, N'ApiVersion', N'v1', '20090522 11:40:31.027', 1, NULL, NULL UNION ALL
SELECT 11, N'ConsumerKey', N'6', '20090522 11:40:31.037', 1, NULL, NULL UNION ALL
SELECT 12, N'ConsumerSecret', N'6fd61bc9-2965-4a0d-bb48-1a134bec6875', '20090522 11:40:31.043', 1, NULL, NULL UNION ALL
SELECT 13, N'F1LoginMethod', N'WebLinkUser', '20090522 11:40:31.053', 1, NULL, NULL
COMMIT;
RAISERROR (N'[dbo].[ConfigSetting]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[ConfigSetting] OFF;

GO

--NeedStatus
SET IDENTITY_INSERT [dbo].[NeedStatus] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[NeedStatus]([ID], [Name])
SELECT 1, N'Pending' UNION ALL
SELECT 2, N'Open' UNION ALL
SELECT 3, N'Denied' UNION ALL
SELECT 4, N'Met' UNION ALL
SELECT 5, N'Cancelled'
COMMIT;
RAISERROR (N'[dbo].[NeedStatus]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[NeedStatus] OFF;
GO

--UserNeedStatus
SET IDENTITY_INSERT [dbo].[UserNeedStatus] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[UserNeedStatus]([ID], [Name])
SELECT 1, N'Interested' UNION ALL
SELECT 2, N'Accepted' UNION ALL
SELECT 3, N'Committed' UNION ALL
SELECT 4, N'SubmitterDeclined' UNION ALL
SELECT 5, N'HelperDeclined'
COMMIT;
RAISERROR (N'[dbo].[UserNeedStatus]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[UserNeedStatus] OFF;
GO

--UserRatingRole
SET IDENTITY_INSERT [dbo].[UserRatingRole] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[UserRatingRole]([ID], [Name])
SELECT 1, N'Submitter' UNION ALL
SELECT 2, N'Helper'
COMMIT;
RAISERROR (N'[dbo].[UserRatingRole]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[UserRatingRole] OFF;
GO

--UserRatingValue
SET IDENTITY_INSERT [dbo].[UserRatingValue] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[UserRatingValue]([ID], [Name], [Value])
SELECT 1, N'Positive', 1 UNION ALL
SELECT 2, N'Neutral', 0 UNION ALL
SELECT 3, N'Negative', -1
COMMIT;
RAISERROR (N'[dbo].[UserRatingValue]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[UserRatingValue] OFF;
GO

--UserStatus
SET IDENTITY_INSERT [dbo].[UserStatus] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[UserStatus]([ID], [Name])
SELECT 1, N'Active' UNION ALL
SELECT 2, N'Suspended' UNION ALL
SELECT 3, N'Banned'
COMMIT;
RAISERROR (N'[dbo].[UserStatus]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[UserStatus] OFF;
GO
