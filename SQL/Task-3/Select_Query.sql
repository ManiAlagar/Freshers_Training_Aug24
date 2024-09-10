	USE TASKS
	INSERT INTO Task (TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID)
	VALUES 
		('Design Armtech', 'Design phase for the new website', '2024-01-02', '2024-09-28', 'Low', 'Completed', 4)

	UPDATE Task
	SET DueDate = '2024-09-28',[Status]='Pending'
	WHERE TaskName = 'Financial Statements'

	/* 1 */
	SELECT  TaskID ,TaskName,StartDate FROM Task
	ORDER BY StartDate ;
	GO

	/* 2 */
	SELECT  P.ProjectName,COUNT(T.TaskName) AS Number_of_Task FROM  Project P
	INNER JOIN Task T ON P.ProjectID = T.ProjectID 
	GROUP BY P.ProjectName
	ORDER BY  Number_of_Task DESC
	GO

	/* 3 */
	SELECT P.ProjectName,COUNT(T.TaskName) AS Number_of_Task, P.Budget FROM  Project P
	INNER JOIN Task T
	ON P.ProjectID = T.ProjectID 
	GROUP BY  P.ProjectName,P.Budget
	ORDER BY P.Budget
	GO

	/* 4 */
	SELECT ProjectName,[Status],Budget FROM Project
	WHERE [Status] ='In Progress' AND (Budget BETWEEN 10000 AND 50000);
	GO

	/* 5 */
	SELECT TaskName,StartDate,[Status] FROM Task
	WHERE [Status] = 'Completed' AND YEAR(StartDate)=2024;
	GO

	/* 6 */
	SELECT TaskName,StartDate,DueDate,[Status] FROM Task
	WHERE [Status] = 'Pending' AND  ( Month(DueDate) = MONTH (GETDATE())+1 )
	GO

	/* 7 */
	SELECT T.TaskName,T.[Priority] FROM Task T 
	INNER JOIN Project P ON P.ProjectID = T.ProjectID 
	WHERE P.ProjectName ='Website Redesign' AND T.[Priority]='High'
	GO

	/* 8 */
	SELECT ProjectName FROM Project 
	WHERE
	ProjectID IN (
	SELECT ProjectID FROM Task
    WHERE [Status] != 'Completed' AND GETDATE() > DueDate)
	GO

	/* 9 */
	SELECT TaskName FROM Task
	WHERE ProjectID = ( SELECT ProjectID FROM Project
	WHERE StartDate = (SELECT max(StartDate) FROM Project) )
	GO

	/* 10 */
	SELECT ProjectName,ProjectID From Project
	WHERE ProjectID IN ((Select ProjectID From Task where [Priority] = 'High'))
	AND
	ProjectID IN ((Select ProjectID From Task where [Priority] = 'Low'))
	GO

	/* 11 */
	SELECT TaskName FROM Task
	WHERE TaskName LIKE 'Design%' 
	GO

	/* 12 */
	SELECT TaskName FROM Task
	WHERE TaskName LIKE '%Review%' AND TaskName NOT LIKE 'Pre%'
    GO

	/* 13 */
	SELECT TaskName FROM Task
	WHERE TaskName LIKE '%[A-M]___'
    GO

	SELECT TaskName FROM Task
	WHERE TaskName LIKE '%[A-M][A-M][A-M]%'
   
   select * from task
   select * from Project