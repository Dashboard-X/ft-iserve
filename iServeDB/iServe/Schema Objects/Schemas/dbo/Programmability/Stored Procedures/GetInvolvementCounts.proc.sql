CREATE PROCEDURE dbo.GetInvolvementCounts
	(
		@needIDs varchar(8000)
	)
AS
	
	SELECT NeedID
	, SUM(CASE WHEN UserStatusName = 'Interested' THEN NumRecords ELSE 0 END) AS Interested
	, SUM(CASE WHEN UserStatusName = 'Accepted' THEN NumRecords ELSE 0 END) AS Accepted
	, SUM(CASE WHEN UserStatusName = 'Committed' THEN NumRecords ELSE 0 END) AS [Committed]
	, SUM(CASE WHEN UserStatusName = 'SubmitterDeclined' THEN NumRecords ELSE 0 END) AS SubmitterDeclined
	, SUM(CASE WHEN UserStatusName = 'HelperDeclined' THEN NumRecords ELSE 0 END) AS HelperDeclined
	FROM
	(
	Select n.ID AS NeedID, uns.ID AS UserNeedStatusID, uns.NAME AS UserStatusName, count(n.ID) as NumRecords
	FROM Need as n
	inner join dbo.strSplit(@needIDs, ',') as n2 on n2.val = n.ID
	LEFT JOIN dbo.UserNeed AS un
		  ON n.ID = un.NeedID
	LEFT JOIN UserNeedStatus as uns
		  ON un.UserNeedStatusID = uns.ID
	WHERE n.ID IS NOT NULL AND uns.ID IS NOT NULL
	GROUP BY n.ID, uns.ID, uns.NAME
	) AS t1
	GROUP BY NeedID

