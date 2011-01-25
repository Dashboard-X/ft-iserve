CREATE PROCEDURE dbo.CompleteNeed
	(
		@needID int
	)
	
AS
	update Need set NeedStatusID = 4
	where ID = @needID
	
	delete from UserNeed
	where NeedID = @neediD
	and UserNeedStatusID IN (1,2)
