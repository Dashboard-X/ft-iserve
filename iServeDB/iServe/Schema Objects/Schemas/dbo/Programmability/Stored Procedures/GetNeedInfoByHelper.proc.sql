CREATE PROCEDURE dbo.GetNeedInfoByHelper
	(
		@userID int,
		@churchID int
	)
	
AS
	select n.ID as NeedID, n.CategoryID, n.Name as NeedName, n.Description, n.SubmittedByID, n.ChurchID, n.IsRequiredOnDate, n.RequiredDate,
		 n.NeedStatusID, n.HelpersNeeded, n.ImageName,
		 u.Name as UserName, u.Rating, 
		 un.UserNeedStatusID, un.IsSubscribed, un.HasBeenRated, un.HasRatedSubmitter, case when n.NeedStatusID in (4,5) and un.UserNeedStatusID = 3 and un.HasRatedSubmitter = 0 then cast(1 as bit) else cast(0 as bit) end as CanRate
	from [Need] n
	inner join [UserNeed] un on un.NeedID = n.ID and un.UserID = @userID 
	inner join [User] u on n.SubmittedByID = u.ID
	where n.churchID = @churchID and un.HasRatedSubmitter = 0
