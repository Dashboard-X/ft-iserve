
CREATE TRIGGER trg_DDL_Monitor_Change
ON DATABASE
FOR DDL_DATABASE_LEVEL_EVENTS
AS
SET NOCOUNT ON
SET ANSI_PADDING ON


declare @EventType varchar(100)
declare @SchemaName varchar(100)
declare @DatabaseName varchar(100)
declare @ObjectName varchar(100)
declare @ObjectType varchar(100)
DECLARE @EventDataText VARCHAR(MAX)

SELECT 
 @EventType = EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]','nvarchar(max)')  
,@DatabaseName = EVENTDATA().value('(/EVENT_INSTANCE/DatabaseName)[1]','nvarchar(max)')  
,@SchemaName = EVENTDATA().value('(/EVENT_INSTANCE/SchemaName)[1]','nvarchar(max)')  
,@ObjectName = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]','nvarchar(max)')
,@ObjectType = EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]','nvarchar(max)')   
,@EventDataText = EVENTDATA().value('(/EVENT_INSTANCE/TSQLCommand/CommandText)[1]','nvarchar(max)')

insert into Audit.dbo.DDL_Audit (Event_Type, Database_Name, SchemaName, ObjectName, ObjectType
	, EventDate, SystemUser, CurrentUser, OriginalUser, EventDataText)
	select @EventType, @DatabaseName, @SchemaName, @ObjectName, @ObjectType
	, getdate(), SUSER_SNAME(), CURRENT_USER, ORIGINAL_LOGIN()
	, @EventDataText

