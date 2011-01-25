CREATE PROCEDURE [dbo].[GetNeedHelpersByStatus]
	(
		@needID int,
		@userNeedStatusID int,
		@churchID int
	)
	
AS
	select u.ID as UserID, u.Name as UserName, u.ChurchID, u.Rating, u.Email, u.UserStatusID,
			un.UserNeedStatusID, un.IsSubscribed, un.HasBeenRated, un.HasRatedSubmitter, un.NeedID
	from [User] u
	inner join [UserNeed] un on un.UserID = u.ID
	where un.NeedID = @needID and un.UserNeedStatusID = @userNeedStatusID
	and u.ChurchID = @churchID
