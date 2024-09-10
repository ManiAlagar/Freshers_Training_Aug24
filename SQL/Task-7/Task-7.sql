	USE TASKS
	GO
	
--Create ProjectStatus Trigger
	CREATE OR ALTER TRIGGER trg_UpdateProjectStatus
	ON Project 
	AFTER UPDATE 
	AS
	IF UPDATE(EndDate)
	BEGIN
	IF ( (SELECT [Status] FROM INSERTED) <> 'Completed' )
		BEGIN
			UPDATE Project
			SET [Status] = 'Completed'
			WHERE ProjectID = (SELECT ProjectID FROM INSERTED) AND GETDATE() >= (SELECT EndDate FROM INSERTED) 
		END
	END
	GO

--Trigger Event Here...
	UPDATE Project  
	SET EndDate = '2024-07-05'
	WHERE ProjectID = 1
	GO
	SELECT * FROM Project



--Create AuditTaskChanges Trigger

	CREATE TABLE TaskAudit
	( 
	     AuditID INT IDENTITY,
		 TaskID int,
		 TaskName VARCHAR(255),
		 [Description] VARCHAR(255),
		 StartDate DATE,
		 DueDate DATE,
		 [Priority] varchar(255) not null,
		 [STATUS] varchar(255),
		 ProjectID INT,
		 Operation_Type VARCHAR(6),
		 Date_Time  datetime	
		 CONSTRAINT PK_AuditID PRIMARY KEY(AuditID)
	);
	GO

	CREATE OR ALTER TRIGGER trg_UpdateChanges
	ON Task 
	AFTER UPDATE
	AS
		BEGIN
				INSERT INTO TaskAudit(TaskID,TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID,Operation_Type,Date_Time) 
				SELECT TaskID,TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID,'Update',GETDATE() FROM INSERTED			
		END
	GO

--FOR INSERT 
	CREATE OR ALTER TRIGGER trg_InsertChanges
	ON Task 
	AFTER INSERT
	AS
		BEGIN
		INSERT INTO TaskAudit(TaskID,TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID,Operation_Type,Date_Time) 
				SELECT TaskID,TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID,'Insert',GETDATE() FROM INSERTED
		END
	GO

	CREATE OR ALTER TRIGGER trg_DeleteChanges
	ON Task 
	AFTER DELETE
	AS
		BEGIN
			INSERT INTO TaskAudit(TaskID,TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID,Operation_Type,Date_Time)
			SELECT TaskID,TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID,'Delete',GETDATE() FROM DELETED;
		END
	GO


--Trigger Event Here...
	INSERT INTO Task (TaskName, [Description], StartDate, DueDate,[Priority],[Status], ProjectID)
	VALUES 
		('Chip Design', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'High', 'Completed', 2)
	GO

	UPDATE Task 
	SET DueDate = '2024-07-11'
	WHERE TaskID = 2
	GO

	DELETE FROM Task WHERE TaskID =4
	GO

--Here can see the TaskAudit table details:
    SELECT * FROM TaskAudit
	SELECT * FROM Task
	GO

