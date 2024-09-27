USE TASKS
--1
BEGIN TRY
	BEGIN TRANSACTION  
	INSERT INTO Project (ProjectName, StartDate, EndDate, Budget, [Status])
	VALUES 
		('Project-7', '2024-01-01', '2024-11-30', 15000.00, 'In Progress')
		declare @ProjectId int
		SET @ProjectId =SCOPE_IDENTITY()
	INSERT INTO Task (TaskName, [Description], StartDate, DueDate, [Priority], [Status], ProjectID)
	VALUES 
		('TASK-5', 'Design phase for the new website', '2024-01-02', '2024-10-28', 'High', 'Completed',@ProjectId),
		('TASK-6', 'Design phase for the new website', '2024-01-09', '2024-10-22', 'Low', 'Completed',@ProjectId)
	COMMIT
END TRY
BEGIN CATCH
   SELECT ERROR_MESSAGE() AS Error__message
   ROLLBACK
END CATCH

--2
BEGIN TRY
	DECLARE @ids TABLE (id int);
	BEGIN TRANSACTION 
	UPDATE Project
	SET Project.Budget=20000.00
	OUTPUT INSERTED.ProjectId INTO @ids
	WHERE ProjectId=1;

	UPDATE Task
	SET [Priority]='Low'
	FROM Task
	JOIN @ids i on i.id = Task.ProjectId;
	COMMIT
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() AS Error__message
    ROLLBACK
END CATCH

--3
CREATE OR ALTER PROCEDURE SelectTask
AS
BEGIN
  BEGIN TRY
	BEGIN TRANSACTION;
		SELECT * FROM Task
		COMMIT TRANSACTION;  
  END TRY
  BEGIN CATCH
		SELECT ERROR_MESSAGE() AS Error__message
		ROLLBACK TRANSACTION;
  END CATCH
END
GO
EXEC SelectTask;



CREATE OR ALTER PROCEDURE DeleteTask(
	@ID INT
	)
	AS
	BEGIN 
	  BEGIN TRY
		BEGIN TRANSACTION;
			DELETE FROM Task
			WHERE TaskId = @ID;
		COMMIT TRANSACTION;  
	  END TRY
	  BEGIN CATCH
			SELECT ERROR_MESSAGE() AS Error__message
			ROLLBACK TRANSACTION;
	  END CATCH
	END
	GO
	EXEC DeleteTask @ID=57;


CREATE OR ALTER PROCEDURE InsertOneTask(
		@TaskName VARCHAR(10),
		@Description VARCHAR(100),
		@StartDate DATE,
		@DueDate DATE,
		@Priority VARCHAR(30),
		@Status VARCHAR(30),
		@ProjectId INT
		)
		AS
		BEGIN 
		  BEGIN TRY
			BEGIN TRANSACTION;
				INSERT INTO Task(Taskname,[Description],StartDate,DueDate,[Priority],[Status],ProjectId)
				VALUES
					(@TaskName,@Description,@StartDate,@DueDate,@Priority,@Status,@ProjectId)
			 COMMIT TRANSACTION;  
		  END TRY
		  BEGIN CATCH
				SELECT ERROR_MESSAGE() AS Error__message
				ROLLBACK TRANSACTION;
		  END CATCH
		END
		GO
		EXEC InsertOneTask
			@TaskName='testName 6', 
			@Description='description',
			@StartDate='2024-01-01', 
			@DueDate='2024-01-08',
			@Priority='High', 
			@Status='Completed',
			@ProjectId=2;
		GO
		SELECT * FROM Task


CREATE or ALTER PROCEDURE UpdateOneTask(
	@id INT,
	@TaskName VARCHAR(30),
	@Description VARCHAR(100),
	@StartDate DATE,
	@DueDate DATE,
	@Priority VARCHAR(30),
	@Status VARCHAR(30),
	@ProjectId INT)
	AS
		BEGIN 
		  BEGIN TRY
			BEGIN TRANSACTION;
				UPDATE Task
				SET Taskname=@TaskName,
					[Description]=@Description,
					StartDate=@StartDate,
					DueDate=@DueDate,
					[Priority]=@Priority,
					[Status]=@Status,
					ProjectId=@ProjectId
				WHERE TaskId = @ID;
			 COMMIT TRANSACTION;  
		  END TRY
		  BEGIN CATCH
				SELECT ERROR_MESSAGE() AS Error__message
				ROLLBACK TRANSACTION;
		  END CATCH
		END
		GO
		EXEC UpdateOneTask
				@ID=11,
				@TaskName='ReportESS', 
				@Description='Final review and submission of the annual report',
				@StartDate='2024-01-01', 
				@Duedate='2024-08-09',
				@Priority='Medium', 
				@Status='Pending',
				@ProjectId=2;
		GO
		SELECT * FROM Task







		with cte_name as
		(
		select max(salary)
		from project)