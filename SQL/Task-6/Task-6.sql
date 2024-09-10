USE TASKS;
GO
--Function DML
--In Function cannot perform DML(Insert,Update,Delete,) operation. 

--SELECT Function
	CREATE OR ALTER FUNCTION GET_ALLTask()
	RETURNS TABLE
	AS
	RETURN
	(SELECT * FROM TASK)
	GO

--Invoked Here
	SELECT * FROM GET_ALLTask()
	GO

--Store Procedure:

--Insert  SP
	CREATE OR ALTER PROCEDURE InsertInto_Task(
	@TaskName VARCHAR(255),@Description VARCHAR(255),@StartDate DATE,@DueDate DATE,@Priority VARCHAR(255),@Status VARCHAR(255),@ProjectID INT)
	AS
	BEGIN
	INSERT INTO TASK (TaskName,[Description],StartDate, DueDate,[Priority],[Status], ProjectID)
	VALUES (@TaskName,@Description,@StartDate,@DueDate,@Priority,@Status,@ProjectID)
	END
	GO

--Invoked Here
	EXEC InsertInto_Task    
	    @TaskName = 'Arm Interface',
		@Description = 'Design phase for the new website',
		@StartDate = '2024-01-02',
		@DueDate = '2024-09-28',
		@Priority = 'Medium',
		@Status = 'Completed',
		@ProjectID = 2;
	GO

--UPDATE  SP 
	CREATE OR ALTER PROCEDURE Update_TaskDetails ( @TaskID INT,@TaskName VARCHAR(255),@Description VARCHAR(255),
	@StartDate DATE,@DueDate DATE,@Priority VARCHAR(255),@Status VARCHAR(255),@ProjectID INT )
	AS
	BEGIN
	UPDATE Task
	SET 
		TaskName = @TaskName,
		[Description] = @Description,
		StartDate = @StartDate,
		DueDate = @DueDate,
		[Priority] = @Priority,
		[Status] = @Status,
		ProjectID = @ProjectID
	WHERE
		TaskID = @Taskid
	END 
	GO

--Invoked Here
	EXEC Update_TaskDetails
		@TaskID = 16,
	    @TaskName = 'Arm Interface',
		@Description = 'Making arm and chip interface',
		@StartDate = '2024-01-12',
		@DueDate = '2024-02-28',
		@Priority = 'Medium',
		@Status = 'Completed',
		@ProjectID = 3;
	GO

--DELETE SP  
	CREATE OR ALTER PROCEDURE Delete_TaskDetails(@id INT)
	AS
	BEGIN
	DELETE FROM Task 
	WHERE TaskId = @id
	END	
	GO

--Invoked Here
	EXEC Delete_TaskDetails 16
	GO

--SELECT SP 
	CREATE OR ALTER PROCEDURE SelectAllTask
	AS
	SELECT * FROM Task
	GO

--Invoked Here
	EXEC SelectAllTask