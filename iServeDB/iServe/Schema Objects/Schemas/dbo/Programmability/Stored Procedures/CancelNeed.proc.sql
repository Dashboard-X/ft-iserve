CREATE PROCEDURE dbo.CancelNeed
		(
		@needID int
	)
AS
	
	delete from UserNeed 
	where NeedID = @needID
	and UserNeedStatusID <> 3
	
	update Need set NeedStatusID = 5 
	where ID = @needID
	
	