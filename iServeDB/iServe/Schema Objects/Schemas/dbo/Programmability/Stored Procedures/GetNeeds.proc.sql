
/*
exec GetNeeds @ChurchID = 501, @UserID = 24643482, @OrderBy = 'CategoryID', @PageNumber = 1, @RecordsPerPage = 50
*/


CREATE PROCEDURE [dbo].[GetNeeds]
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

declare @start int, @end int, @sql as nvarchar(MAX)
select @start = (@pageNumber-1) * @recordsPerPage+1, @end = @pageNumber * @recordsPerPage;

CREATE TABLE #Needs (NeedID INT)
INSERT #Needs (NeedID)
SELECT n.ID FROM Need n WHERE SubmittedByID <> @userID
	EXCEPT
SELECT un.NeedID FROM dbo.UserNeed AS un WHERE UserID = @userID

CREATE TABLE #Results(ID int NOT NULL,
            ChurchID int NOT NULL,
            CategoryID int NOT NULL,
            SubmittedByID int NOT NULL,
            [Name] nvarchar(100) NOT NULL,
            [Description] nvarchar(3000),
            RequiredDate datetime NULL,
            IsRequiredOnDate bit NOT NULL,
            PostalCode varchar(11) NULL,
            ImageName varchar(80) NULL,
            HelpersNeeded int NOT NULL,
            NeedStatusID int NOT NULL,
            Created datetime NOT NULL,
            CreatedBy int NULL,
            Updated datetime NULL,
            UpdatedBy int NULL,
            Interested int NOT NULL,
            Accepted int NOT NULL,
            [Committed] int NOT NULL,
            SubmitterDeclined int NOT NULL,
            HelperDeclined int NOT NULL)
            
INSERT #Results (ID,ChurchID,CategoryID,SubmittedByID,[Name],[Description],
            RequiredDate,IsRequiredOnDate,PostalCode,ImageName,HelpersNeeded,NeedStatusID,Created,
            CreatedBy,Updated,UpdatedBy,Interested,Accepted,[Committed],SubmitterDeclined,HelperDeclined)
select 
            n.ID, n.ChurchID, n.CategoryID, n.SubmittedByID, n.[Name], n.[Description], n.RequiredDate, n.IsRequiredOnDate,
            n.PostalCode, n.ImageName, n.HelpersNeeded, n.NeedStatusID, n.Created, n.CreatedBy, n.Updated, n.UpdatedBy
            , NeedSummary.Interested
            , NeedSummary.Accepted
            , NeedSummary.[Committed]
            , NeedSummary.SubmitterDeclined
            , NeedSummary.HelperDeclined 
from 
      Need n
inner join
      #Needs AS n2
      ON n.ID = n2.NeedID
JOIN
(SELECT NeedID
      , SUM(CASE WHEN UserStatusName = 'Interested' THEN NumRecords ELSE 0 END) AS Interested
      , SUM(CASE WHEN UserStatusName = 'Accepted' THEN NumRecords ELSE 0 END) AS Accepted
      , SUM(CASE WHEN UserStatusName = 'Committed' THEN NumRecords ELSE 0 END) AS [Committed]
      , SUM(CASE WHEN UserStatusName = 'SubmitterDeclined' THEN NumRecords ELSE 0 END) AS SubmitterDeclined
      , SUM(CASE WHEN UserStatusName = 'HelperDeclined' THEN NumRecords ELSE 0 END) AS HelperDeclined
      FROM
      (
      Select n.ID AS NeedID, uns.ID AS UserNeedStatusID, uns.NAME AS UserStatusName, count(n.ID) as NumRecords
      FROM Need as n
      inner join #Needs as n2 on n2.NeedID = n.ID
      LEFT JOIN dbo.UserNeed AS un
              ON n.ID = un.NeedID
      LEFT JOIN UserNeedStatus as uns
              ON un.UserNeedStatusID = uns.ID
      GROUP BY n.ID, uns.ID, uns.NAME
      ) AS t1
      GROUP BY NeedID
) AS NeedSummary
      ON n.ID = NeedSummary.NeedID and NeedSummary.[Committed] < n.HelpersNeeded
where 
      n.ChurchID = @churchID
      and n.NeedStatusID <> 5
      and n.Created <= getdate()

select @rowCount = COUNT(*) FROM #Results AS r

select @sql = 'WITH t1 AS (SELECT ROW_NUMBER() OVER (ORDER BY ' + @orderBy + ' ' + case when @asc=1 then 'asc' else 'desc' end +  ') as RowNumberID, * FROM #Results)
      SELECT * from t1
      WHERE RowNumberID BETWEEN ' + CAST(@start as varchar(10)) + ' and ' + CAST(@end AS VARCHAR(10))

exec sp_ExecuteSQL @sql

drop table #Results
drop table #Needs


