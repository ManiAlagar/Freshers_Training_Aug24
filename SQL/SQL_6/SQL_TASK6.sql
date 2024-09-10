--SELECT 
USE TASKS
GO
CREATE OR ALTER FUNCTION GetTasks(@ID INT)
RETURNS TABLE
AS
RETURN
(
	SELECT * FROM Task
	WHERE TaskId=@ID
);
GO
SELECT * FROM GetTasks(1);

--we can't able to insert,update or delete the records in a table by using function.
--So, I implemented those by using stored procedures.

--INSERT
CREATE OR ALTER PROCEDURE InsertTask(
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
		INSERT INTO Task(Taskname,[Description],StartDate,DueDate,[Priority],[Status],ProjectId)
		VALUES(@TaskName,@Description,@StartDate,@DueDate,@Priority,@Status,@ProjectId)
	end
	GO
	EXEC InsertTask
		@TaskName='testName 3', 
		@Description='description',
		@StartDate='2024-01-01', 
		@DueDate='2024-01-08',
		@Priority='High', 
		@Status='Completed',
		@ProjectId=2;
	GO
	SELECT * FROM Task

--UPDATE
CREATE or ALTER PROCEDURE UpdateTask(
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
		UPDATE Task
		SET Taskname=@TaskName,
			[Description]=@Description,
			StartDate=@StartDate,
			DueDate=@DueDate,
			[Priority]=@Priority,
			[Status]=@Status,
			ProjectId=@ProjectId

		WHERE TaskId = @ID;
	END
	GO
	EXEC UpdateTask
		@ID=11,
		@TaskName='ReportS', 
		@Description='Final review and submission of the annual report',
		@StartDate='2024-01-01', 
		@Duedate='2024-08-09',
		@Priority='Medium', 
		@Status='Pending',
		@ProjectId=2
	GO
	SELECT * FROM Task

--DELETE.
CREATE OR ALTER PROCEDURE DeleteTask(
	@ID INT
	)
	AS
	BEGIN
		DELETE FROM Task
		WHERE TaskId = @ID;
	END
	GO
	EXEC DeleteTask
		@ID=10;
	GO
	SELECT * FROM Task

--SELECT
CREATE OR ALTER PROCEDURE SelectAllTasks
	AS
	BEGIN
		SELECT * FROM Task
	END
	GO
	EXEC SelectAllTasks;
