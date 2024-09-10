--1
USE TASKS

CREATE OR ALTER TRIGGER trg_UpdateProjectStatus 
ON Project
AFTER UPDATE
AS
BEGIN
	UPDATE Project 
	SET [Status]='Completed'
	WHERE ProjectId = (SELECT ProjectId FROM inserted);
END

UPDATE Project
SET startdate='2024-01-08'
WHERE ProjectId=2;

SELECT * FROM Project

--2 AFTER UPDATE

--CREATE TABLE TaskAudit
--(
--	ID int IDENTITY PRIMARY KEY,
--	ModifiedDate varchar(30),
--	Operation varchar(20),
--	TaskId INT
--);
GO
CREATE OR ALTER TRIGGER trg_AuditTaskChanges
ON TASK
AFTER UPDATE
AS
BEGIN
	INSERT INTO TaskAudit(TaskId,ModifiedDate,Operation)
	SELECT inserted.TaskId,GETDATE(),'Update' FROM inserted
END

UPDATE Task
SET StartDate='2024-01-12'
WHERE TaskId=14;


SELECT * FROM TaskAudit
SELECT * FROM Task

GO
-------------AFTER INSERT
CREATE OR ALTER TRIGGER trg_AuditTaskInsert
ON TASK
AFTER INSERT
AS
BEGIN
	INSERT INTO TaskAudit(TaskId,ModifiedDate,Operation)
	SELECT inserted.TaskId,GETDATE(),'Insert' FROM inserted
END
GO

INSERT INTO Task (TaskName, [Description], StartDate, DueDate, [Priority], [Status], ProjectID)
VALUES 
 ('Initial Designs 2', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'High', 'Completed', 1)


SELECT * from TaskAudit

 ---------------AFTER DELETE
CREATE OR ALTER TRIGGER trg_AuditTaskDelete
ON TASK
AFTER DELETE
AS
BEGIN
	INSERT INTO TaskAudit(TaskId,ModifiedDate,Operation)
	SELECT deleted.TaskId,GETDATE(),'Delete' FROM deleted
END

DELETE FROM Task
WHERE TaskId=11;

SELECT * FROM TaskAudit

----------------INSTEAD OF
CREATE OR ALTER TRIGGER trg_AuditTaskSelect
ON TASK
INSTEAD OF DELETE
AS
BEGIN
	SELECT * FROM Task
END

DELETE FROM Task
WHERE TaskId=10;

SELECT * FROM TaskAudit