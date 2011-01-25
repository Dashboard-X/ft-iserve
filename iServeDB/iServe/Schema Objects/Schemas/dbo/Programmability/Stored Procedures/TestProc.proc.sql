CREATE PROCEDURE dbo.TestProc
	(
		@churchID int,
		@userID int,
		@pageNumber int = 1,
		@recordsPerPage int = 20,
		@orderBy varchar(300),
		@asc bit = 1,
		@rowCount int = 0 output
	)
AS

select 
		n.ID, n.ChurchID, n.CategoryID, n.SubmittedByID, n.[Name], n.[Description], n.RequiredDate, n.IsRequiredOnDate,
		n.PostalCode, n.ImageName, n.HelpersNeeded, n.NeedStatusID, n.Created, n.CreatedBy, n.Updated, n.UpdatedBy
		, 1 AS Interested
		, 1 AS Accepted
		, 1 AS [Committed]
		, 1 AS SubmitterDeclined
		, 1 AS HelperDeclined 
from 
	Need n
inner join
	UserNeed un on un.NeedID = n.ID
where 
	n.ChurchID = @churchID
      and n.NeedStatusID <> 5
      and n.Created <= getdate()
      and n.SubmittedByID <> @userID
      and un.UserID = @userID
      
 set @rowCount=27
      
 select @rowCount as [RowCount]
