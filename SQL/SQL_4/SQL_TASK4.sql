 USE TASKS
GO

--I inserted the following records to retrive data for some queries.

--INSERT INTO Project (ProjectName, StartDate, EndDate, Budget, [Status])
--VALUES 
--    ('Website 1', '2024-01-01', '2024-06-30', 15000.00, 'In Progress')

--INSERT INTO Task (TaskName, [Description], StartDate, DueDate, [Priority], [Status])
--VALUES 
--    ('Initial Design', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'High', 'Completed')

--1
SELECT TaskName,ProjectName,Project.StartDate,EndDate,Budget,Project.[Status]
FROM Task
JOIN Project ON Project.ProjectId=Task.ProjectId;
GO

select * from task

--2
SELECT ProjectName,STRING_AGG(TaskName,',') AS TaskName 
FROM Project
LEFT JOIN Task ON Project.ProjectId=Task.ProjectId
GROUP BY ProjectName;
GO

--3
SELECT STRING_AGG(TaskName,',') AS Tasks,Project.ProjectName 
FROM Task
LEFT JOIN Project ON Project.ProjectId=Task.ProjectId
GROUP BY ProjectName;
GO

--4
ALTER TABLE Project 
ADD ParentProjectId 
INT NULL;

INSERT INTO Project (ProjectName, StartDate, EndDate, Budget, [Status],ParentProjectId)
	values
	('React Native', '2024-01-01', '2024-06-30', 15000.00, 'In Progress',2);

SELECT P1.ProjectName AS ParentProjectName,
P2.ProjectName AS ChildProjectName 
FROM Project P1
JOIN Project P2  
ON P1.ProjectId=P2.ParentProjectId;

--5
SELECT GETDATE() AS CURRDATEANDTIME;
SELECT CURRENT_TIMESTAMP AS CURRDATEANDTIME;
GO

--6
--SELECT YEAR(StartDate) Y_startdate,MONTH(EndDate) M_enddate FROM Project
--wHERE ProjectId=1;

SELECT DATEPART(YEAR, StartDate) AS [DatePart_Y] ,DATEPART(MONTH, StartDate) AS [DatePart_M] 
FROM Project
WHERE ProjectId=1;
GO

--7
SELECT DATEDIFF(DAY,StartDate,EndDate) AS [DateDiff] 
FROM Project
WHERE ProjectId=1;
GO

--8 As per mentor's suggestion, i tried different date format as it already in a given format
SELECT FORMAT(StartDate,'dd-MM-yyyy') AS [DATEFORMAT] 
FROM Project
WHERE ProjectId=1;
GO

--9
SELECT ProjectName,STRING_AGG(TaskName,',') AS Tasks 
FROM Project
CROSS JOIN Task
WHERE Project.ProjectId=Task.ProjectId
GROUP BY ProjectName;

		--with cte_name as
		--(
		--select max(ProjectId)
		--from project)
		--select * from cte_name