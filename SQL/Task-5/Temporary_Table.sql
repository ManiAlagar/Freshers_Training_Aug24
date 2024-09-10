	Use TASKS;
	GO
-- 1.Local Temporary Table:
-- Query--> 1
	DROP TABLE IF EXISTS #LocalTempTable
-- Query--> 2
	CREATE TABLE #LocalTempTable
	(
	 ID INT IDENTITY,
	 [Name] NVARCHAR(100), 
	 StartDate DATE,
	 [Priority] NVARCHAR(20)
	);
	GO
-- Query--> 3
	INSERT INTO #LocalTempTable([Name], StartDate, [Priority]) 
	SELECT TaskName,StartDate,[Priority] FROM Task
	WHERE [Priority] = 'Low';
	GO
-- Query--> 4
	Select * from #LocalTempTable;
	GO

-- 2.Global Temporary Table
-- Query--> 1
	DROP TABLE IF EXISTS ##GlobalTempTable
-- Query--> 2
	CREATE TABLE ##GlobalTempTable
	(
	 ID INT IDENTITY,
	 ProjectName VARCHAR(100), 
	 Budget DECIMAL(18, 2),
	 [Priority] NVARCHAR(20)
	);
	GO
-- Query--> 3
	INSERT INTO ##GlobalTempTable(ProjectName, Budget, [Priority]) 
	SELECT P.ProjectName,P.Budget,T.[Priority] FROM Project P
	INNER JOIN Task T
	ON P.ProjectID = T.ProjectID
	WHERE T.[Priority] = 'Medium';
	GO
-- Query--> 4
	Select * from ##GlobalTempTable
	GO

-- 3. Declare a Table Variable:
-- Query--> 1
	DECLARE @TableVariable TABLE 
	(
	TaskID INT Identity,
	TaskName VARCHAR(100),
	DueDate DATE,
	[Priority] Nvarchar(20)
	);
-- Query--> 2
	INSERT INTO @TableVariable(TaskName,DueDate, [Priority]) 
	SELECT TaskName,DueDate,[Priority] FROM Task
	WHERE [Priority] = 'High';
-- Query--> 3
	Select * from @TableVariable;
	GO

