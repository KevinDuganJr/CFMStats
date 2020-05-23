create procedure _ListIndexes(@tableName nvarchar(200))
as
begin
/*
exec _ListIndexes '<YOUR TABLE NAME>'
*/
SELECT DB_NAME(DB_ID()) as DBName,SCH.name + '.' + TBL.name AS TableName,IDX.name as IndexName, IDX.type_desc AS IndexType,COL.Name as ColumnName,IC.*
    FROM sys.tables AS TBL 
         INNER JOIN sys.schemas AS SCH ON TBL.schema_id = SCH.schema_id 
         INNER JOIN sys.indexes AS IDX ON TBL.object_id = IDX.object_id 
         INNER JOIN sys.index_columns IC ON  IDX.object_id = IC.object_id and IDX.index_id = IC.index_id 
         INNER JOIN sys.columns COL ON ic.object_id = COL.object_id and IC.column_id = COL.column_id 
        where TBL.name = @tableName
    ORDER BY TableName,IDX.name

end