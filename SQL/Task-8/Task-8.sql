	USE TASKS
	GO

	INSERT INTO
			Project (ProjectName, StartDate,  Budget, [Status])
	VALUES 
			('Weather App', '2024-01-01' , 15000.00, 'In Progress'),
			('Smart Home App', '2024-01-11' , 15000.00, 'In Progress');
    GO

-- Views
-- Query --> 1
	CREATE OR ALTER VIEW
			vw_ActiveProjects
	AS 
	SELECT 
			ProjectName AS Active_Projects
	FROM 
			Project
	WHERE
			EndDate IS NULL
    GO

--Fetch records from Views	
	SELECT
			* 
	FROM
			vw_ActiveProjects
	GO

-- Query --> 2
	CREATE OR ALTER VIEW
			vw_HighPriorityTasks
	AS 
	SELECT 
			TaskName,[priority]
	FROM 
			Task
	WHERE
			Priority = 'High'
    GO

--Fetch records from Views		
	SELECT
			* 
	FROM
			vw_HighPriorityTasks


--Cursors

-- Query --> 1
	DECLARE @TempProjectName VARCHAR(20)

	DECLARE ActiveProjects CURSOR 
	FOR
		SELECT
				* 
		FROM
				vw_ActiveProjects

	OPEN ActiveProjects

	FETCH NEXT FROM ActiveProjects INTO @TempProjectName
	 
	PRINT 'Active Projects:'
	
	WHILE @@FETCH_STATUS = 0
	BEGIN 
	       
			PRINT @TempProjectName	
	        FETCH NEXT FROM ActiveProjects INTO @TempProjectName
	END

	CLOSE ActiveProjects

	DEALLOCATE ActiveProjects

-- Query --> 2
	DECLARE @TempTaskID INT

	DECLARE UpdateTaskStatus CURSOR 
	FOR
		SELECT
			    TaskID
		FROM
				Task
		WHERE GETDATE() > Duedate AND [Status] <> 'Completed'
		
	OPEN UpdateTaskStatus

	FETCH NEXT FROM UpdateTaskStatus INTO @TempTaskID

	WHILE @@FETCH_STATUS = 0
	BEGIN 
				UPDATE Task
				SET [Status] = 'Overdue'
				WHERE TaskID = @TempTaskID
				FETCH NEXT FROM UpdateTaskStatus INTO @TempTaskID
	END

	CLOSE UpdateTaskStatus

	DEALLOCATE UpdateTaskStatus

--For Result Check
Select * From Task