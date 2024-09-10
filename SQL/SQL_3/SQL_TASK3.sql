USE TASKS
GO

--1
SELECT * 
FROM Task
	ORDER BY StartDate;
GO

--2
SELECT Project.ProjectName,COUNT(TaskId) AS total_count_of_tasks 
FROM Project
JOIN Task ON Project.ProjectId=Task.ProjectId
	GROUP BY ProjectName
	ORDER BY total_count_of_tasks DESC;
GO

--3
SELECT Project.ProjectName,COUNT(TaskId) AS total_count_of_tasks,SUM(Budget) AS Total_Budget 
FROM Project
JOIN Task on Project.ProjectId=Task.ProjectId
	GROUP BY ProjectName
	ORDER BY Total_Budget;
GO

--4
SELECT * 
FROM Project
	WHERE [Status]='In Progress' 
	AND Budget BETWEEN 10000 AND 50000;
GO

--5
SELECT * 
FROM Task
	WHERE YEAR(StartDate)=2024 
	AND [Status]='Completed';
GO

--6
SELECT * 
FROM Task
	WHERE MONTH(DueDate) = MONTH(StartDate)+1 
	AND [Status]='Pending';
GO

--7
SELECT TaskName,[Priority] 
FROM Task
JOIN Project ON Project.ProjectId=Task.ProjectId
	WHERE ProjectName='Website Redesign' 
	AND [Priority]='High';
GO

--8
SELECT STRING_AGG(ProjectName,',') AS ProjectNames 
FROM Project
	WHERE ProjectId IN
	(
	SELECT ProjectId 
	FROM Task
	WHERE Task.DueDate < CAST(GETDATE() AS DATE)
	AND  Task.[Status]<>'Completed'
	);
GO

--9
SELECT TaskName,Project.ProjectName,Project.StartDate 
FROM Task
JOIN Project ON Project.ProjectId=Task.ProjectId
	WHERE ProjectName=
	(
	SELECT TOP 1 ProjectName 
	FROM Project
	ORDER BY StartDate DESC
	);

GO

--INSERT INTO Task (TaskName, [Description], StartDate, DueDate, [Priority], [Status], ProjectID)
--VALUES 
--    ('Designs', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'High', 'Completed', 1),
--	('UI', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'Low', 'Completed', 1),
--	('API', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'Low', 'Completed', 2)

--10
SELECT ProjectName,ProjectId FROM project
WHERE ProjectId IN (SELECT ProjectId FROM task WHERE [Priority]='High' )
AND
ProjectId IN (SELECT ProjectId FROM task WHERE [Priority]='Low' )
GO 

--11
SELECT * 
FROM Task
	WHERE TaskName 
	LIKE 'Design%';
GO

--12
SELECT * 
FROM Task
	WHERE TaskName 
	LIKE '%Review%' 
    AND TaskName NOT LIKE 'Pre%';
GO

--13
SELECT * 
FROM Task
	WHERE TaskName 
	LIKE '%[A-M]___';

SELECT * 
FROM Task
	WHERE TaskName 
	LIKE '%[A-M][A-M][A-M]%';