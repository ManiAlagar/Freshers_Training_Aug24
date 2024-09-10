	USE TASKS

	SELECT * FROM Task
	SELECT * FROM Project

--Query--> 1
	BEGIN TRANSACTION  
	BEGIN TRY
		INSERT INTO
				Project (ProjectName, StartDate,EndDate, Budget, [Status])
		VALUES 
				('Monitoring App', '2024-01-31' ,'2024-06-15', 15000.00, 'In Progress')
	
		DECLARE @ProjectID INT
		SET @ProjectID = SCOPE_IDENTITY ()
	
		INSERT INTO 
				Task (TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID)
		VALUES 
				('UI Design', 'Design phase for the UI', '2024-01-12', '2024-02-28', 'High', 'Completed',@ProjectID),
				('Business Logic', 'Design phase for the Logic', '2024-03-02', '2024-04-28', 'High', 'Completed',@ProjectID),
				('Business Logic', 'Design phase for the Logic', '2024-03-02', '2024-04-28', 'High', 'Completed',@ProjectID)
		COMMIT
	END TRY
	BEGIN CATCH
	        SELECT ERROR_MESSAGE() AS ErrorMessage;  
			ROLLBACK;
	END CATCH
	GO

--Query--> 2
	BEGIN TRANSACTION 
	BEGIN TRY
	DECLARE @LastUpdateID TABLE(UpdateID INT null)
		UPDATE
				Project		
		SET
				Budget = '22800'
				OUTPUT INSERTED.ProjectID INTO @LastUpdateID
		WHERE
				ProjectID = 3
			
		IF NOT EXISTS(Select * from Project where ProjectID IN (SELECT UpdateID FROM @LastUpdateID))
				BEGIN 
					PRINT 'Project not Exists in Project table'
					RETURN;
				END  
		ELSE
		UPDATE 
				Task
		SET 
				Priority = 'LOW'
		WHERE 
				ProjectID = (SELECT UpdateID FROM @LastUpdateID)	
	COMMIT		
	END TRY	
	BEGIN CATCH
			SELECT ERROR_MESSAGE() AS Error
			ROLLBACK
	END CATCH
	GO
	
-- CRUD OPERATION Using SP and Transcation

-- For SP_INSERT 

	CREATE OR ALTER  PROC InsertIntoTask
			( @TaskName VARCHAR(255),@Description VARCHAR(255),@StartDate DATE,@DueDate DATE,@Priority VARCHAR(255),@Status VARCHAR(255),@ProjectID INT )
	AS	
			INSERT INTO
					TASK (TaskName,[Description],StartDate,DueDate,[Priority],[Status], ProjectID)
			VALUES 
					(@TaskName,@Description,@StartDate,@DueDate,@Priority,@Status,@ProjectID) 

-- Transaction	
	BEGIN TRANSACTION 
	BEGIN TRY
		EXEC InsertIntoTask    
			@TaskName = 'Arm Interface',
			@Description = 'Design phase for the new website',
			@StartDate = '2024-01-02',
			@DueDate = '2024-09-28',
			@Priority = 'Medium',
			@Status = 'Completed',
			@ProjectID = 3;
	COMMIT			
	END TRY			
	BEGIN CATCH 
		SELECT  ERROR_MESSAGE() AS Error
		ROLLBACK
	END CATCH


-- For SP_Update
	
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

-- Transaction	
	BEGIN TRANSACTION 
	BEGIN TRY
		EXEC Update_TaskDetails
			@TaskID = 6,
			@TaskName = 'Arm Interface',
			@Description = 'Making arm and chip interface',
			@StartDate = '2024-01-12',
			@DueDate = '2024-02-28',
			@Priority = 'Medium',
			@Status = 'Completed',
			@ProjectID = 4;
		COMMIT		
	END TRY			
	BEGIN CATCH 
		SELECT  ERROR_MESSAGE() AS Error
		ROLLBACK
	END CATCH


-- For SP_Delete

	CREATE OR ALTER PROCEDURE Delete_TaskDetails(@id INT)
	AS
	BEGIN
	DELETE FROM Task 
	WHERE TaskId = @id
	END	
	GO

	BEGIN TRANSACTION UpdateTran
	BEGIN TRY
		EXEC Delete_TaskDetails 11
		COMMIT		
	END TRY			
	BEGIN CATCH 
		SELECT  ERROR_MESSAGE() AS Error
		ROLLBACK
	END CATCH


-- For SP_Select

	CREATE OR ALTER PROCEDURE SelectAllTask
	AS
	SELECT * FROM Task

-- Transaction	
	BEGIN TRANSACTION 
	BEGIN TRY
		EXEC SelectAllTask
	END TRY			
	BEGIN CATCH 
		SELECT  ERROR_MESSAGE() AS Error
	END CATCH



	