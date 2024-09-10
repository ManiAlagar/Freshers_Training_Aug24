USE TASKS
GO


--1

ALTER TABLE Project
ADD [Description] VARCHAR(255) NOT NULL DEFAULT '';
GO

--2

EXEC sp_RENAME 'Project.Description', 'ProjectDescription', 'COLUMN'
GO

--3

ALTER TABLE Project ALTER COLUMN  ProjectDescription  VARCHAR(255) NULL
GO
UPDATE Project set ProjectDescription = NULL;

GO
SELECT * FROM Project 

