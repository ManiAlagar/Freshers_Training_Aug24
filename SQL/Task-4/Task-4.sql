	USE TASKS
	
	INSERT INTO Project (ProjectName, StartDate, EndDate, Budget, [Status])
		VALUES 
			('Brain Computer Interface', '2024-02-01', '2024-07-30', 20000.00, 'In Progress')
	GO

	INSERT INTO Task (TaskName, [Description], StartDate, DueDate,[Priority],[Status])
		VALUES 
			('Task_Sample', 'Design phase for the component', '2024-01-02', '2024-03-28', 'High', 'Completed')
	GO

	--Query --> 1
	SELECT T.TaskName,P.ProjectName,P.StartDate,P.EndDate,P.Budget,P.[Status] FROM Project P 
	INNER JOIN Task T 		
	ON  P.ProjectID = T.ProjectID
	ORDER BY P.ProjectID
	GO

	--Query --> 2
	SELECT P.ProjectName,T.TaskName FROM Project P 
	LEFT JOIN Task T 		
	ON  P.ProjectID = T.ProjectID
	ORDER BY P.ProjectID
	GO

	--Query --> 3
	SELECT P.ProjectName,T.TaskName FROM Project P 
	RIGHT JOIN Task T 		
	ON  P.ProjectID = T.ProjectID
	ORDER BY P.ProjectID DESC
	GO

	--Query --> 4
	ALTER TABLE Project ADD ParentProjectId INT   Null;


	Update Project
	SET ParentProjectID =  1
	Where ProjectID = 5
	GO

	Update Project
	SET ParentProjectID =  3
	Where ProjectID = 2
	GO
	
	
	SELECT T1.ProjectName AS ParentProjectName,T2.ProjectName AS ChildProjectName,T2.StartDate,T2.EndDate
	FROM Project T1
	JOIN  Project T2
	ON T1.ProjectID=T2.ParentProjectID
	GO 

	Select * from project

	--Query --> 5
	SELECT FORMAT (getdate(), 'dd-MM-yyyy') AS DATE,FORMAT (GETDATE(),'hh:mm:ss') as TIME
	GO

	--Query --> 6
	SELECT  ProjectName,Month(StartDate) AS Start_Month,Month(EndDate) AS End_Month 
	FROM Project

	--Query --> 7
	SELECT ProjectName,DATEDIFF(DAY, StartDate, EndDate) AS Date_Difference FROM Project;

	--Query --> 8 As per mentor Suggestion changed the format to MM-DD-YYYY
	SELECT ProjectName, FORMAT(StartDate, 'MM-dd-yyyy') AS StartDate FROM Project

	
	--Query --> 9 

	SELECT P.ProjectName,STRING_AGG ( T.TaskName, ',' ) AS Tasks FROM Project P
	CROSS JOIN Task T
        WHERE P.ProjectID = T.ProjectID
	GROUP BY P.PROJECTNAME
