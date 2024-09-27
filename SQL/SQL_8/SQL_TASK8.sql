USE TASKS 

--1
CREATE OR ALTER VIEW vw_ActiveProjects
AS
SELECT ProjectName
FROM Project
WHERE EndDate IS NULL
GO
SELECT * FROM vw_ActiveProjects
GO

--2
CREATE OR ALTER VIEW vw_HighPriorityTasks
AS
SELECT TaskName,[Priority]
FROM Task
WHERE [Priority]='High'
GO
SELECT * FROM vw_HighPriorityTasks

--1

DECLARE @Projectname VARCHAR(50)
DECLARE db_cursor CURSOR FOR
SELECT ProjectName
FROM Project
WHERE EndDate IS NULL

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @Projectname

WHILE @@FETCH_STATUS = 0  
BEGIN
	PRINT @Projectname
	FETCH NEXT FROM db_cursor INTO @Projectname
END

CLOSE db_cursor  
DEALLOCATE db_cursor 
 

--2
DECLARE @id INT
DECLARE db_cursor CURSOR FOR
SELECT TaskId FROM Task
WHERE DueDate<GETDATE()

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @id
WHILE @@FETCH_STATUS = 0  
BEGIN
	UPDATE Task
	SET [Status]='Overdue'
	WHERE TaskId=@id
	FETCH NEXT FROM db_cursor INTO @id
END

CLOSE db_cursor  
DEALLOCATE db_cursor 
GO
SELECT * FROM task