--LOCAL TEMPORARY TABLE
USE TASKS

DROP TABLE IF EXISTS #LocalTempTable;
GO

CREATE TABLE #LocalTempTable(
	ID INT IDENTITY,
	TaskName NVARCHAR(100),
	StartDate DATE,
	[Priority] NVARCHAR(20)
);
GO

INSERT INTO #LocalTempTable
SELECT 
	TaskName,
	StartDate,
	[Priority]
FROM Task
WHERE [Priority]='Low';
GO

SELECT * 
FROM #LocalTempTable;
GO


--GLOBAL TEMPORARY TABLE
DROP TABLE IF EXISTS ##GlobalTempTable;
GO

CREATE TABLE ##GlobalTempTable(
	ID INT IDENTITY,
	ProjectName VARCHAR(100),
	Budget DECIMAL(18,2),
	[Priority] NVARCHAR(20)
);
GO

INSERT INTO ##GlobalTempTable
SELECT 
	Project.ProjectName,
	Project.Budget,
	[Priority]
FROM Task
JOIN Project ON Project.ProjectId=Task.ProjectId
WHERE [Priority]='Medium';
GO

SELECT * 
FROM ##GlobalTempTable;
GO


--TABLE VARIABLE
DECLARE @TableVariable TABLE(
	TaskID INT Identity, 
	TaskName VARCHAR(100), 
	DueDate DATE, 
	[Priority] Nvarchar(20)
)
INSERT INTO @TableVariable
SELECT 
	TaskName,
	DueDate,
	[Priority]
FROM Task
WHERE [Priority]='High';
SELECT * 
FROM @TableVariable;


