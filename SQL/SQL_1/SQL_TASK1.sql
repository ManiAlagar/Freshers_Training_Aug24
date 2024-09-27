CREATE DATABASE TASKS
GO
USE TASKS
GO
CREATE TABLE Project
(
	ProjectId INT IDENTITY PRIMARY KEY,
	ProjectName VARCHAR(25) UNIQUE,
	StartDate DATE,
	EndDate DATE,
	CONSTRAINT CheckEndLaterThanStart CHECK (EndDate >= StartDate),
	Budget DECIMAL(7, 2) NOT NULL,
	[Status] VARCHAR(25) CONSTRAINT DefaultPending DEFAULT 'Not Started'
);
GO
CREATE TABLE Task
(
	TaskId INT IDENTITY PRIMARY KEY,
	TaskName VARCHAR(30) NOT NULL, 
	[Description] VARCHAR(MAX),
	StartDate DATE,
	DueDate DATE,
	CONSTRAINT CheckDueLaterThanStart CHECK (DueDate >= StartDate),
	[Priority] VARCHAR(6) CONSTRAINT PriortityVal CHECK ([Priority] IN('Low', 'Medium','High')),
	[Status] VARCHAR(25) CONSTRAINT StatusPending  DEFAULT 'Pending',
	ProjectId INT,
	CONSTRAINT Fk_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId)
);
GO
INSERT INTO Project (ProjectName, StartDate, EndDate, Budget, [Status])
VALUES 
    ('Website Redesign', '2024-01-01', '2024-06-30', 15000.00, 'In Progress'),
    ('Mobile App Development', '2024-02-15', '2024-07-15', 25000.00, 'Not Started'),
    ('Market Research', '2024-03-01', '2024-05-31', 10000.00, 'Completed'),
    ('Annual Report Preparation', '2024-04-01', '2024-12-31', 12000.00, 'In Progress');
GO
INSERT INTO Task (TaskName, [Description], StartDate, DueDate, [Priority], [Status], ProjectID)
VALUES 
    ('Initial Design', 'Design phase for the new website', '2024-01-02', '2024-02-28', 'High', 'Completed', 1),
    ('UI Development', 'Development of user interface components', '2024-03-01', '2024-05-15', 'Medium', 'In Progress', 1),
    ('Quality Assurance', 'Testing and quality assurance', '2024-05-16', '2024-06-15', 'High', 'Pending', 1),
    ('API Development', 'Developing APIs for the mobile app', '2024-02-16', '2024-04-30', 'Medium', 'Completed', 2),
    ('Beta Testing', 'Conducting beta testing for the mobile app', '2024-05-01', '2024-06-30', 'High', 'In Progress', 2),
    ('Survey Analysis', 'Analyzing market research surveys', '2024-03-02', '2024-04-15', 'Low', 'Completed', 3),
    ('Report Drafting', 'Drafting the final report based on research', '2024-04-16', '2024-05-30', 'Medium', 'Pending', 3),
    ('Financial Statements', 'Preparing financial statements for the annual report', '2024-04-02', '2024-07-15', 'High', 'In Progress', 4),
    ('Final Review', 'Final review and submission of the annual report', '2024-07-16', '2024-12-15', 'High', 'Pending', 4),
    ('Client Feedback Incorporation', 'Incorporating feedback from the client into the project', '2024-02-01', '2024-03-15', 'Medium', 'In Progress', 1),
    ('Launch Preparation', 'Preparing for the official launch of the mobile app', '2024-06-01', '2024-07-01', 'High', 'Pending', 2);
GO
SELECT * FROM Task
SELECT * FROM Project

CREATE TABLE Proj
(
	ProjectId INT IDENTITY PRIMARY KEY,
	ProjectName VARCHAR(25) UNIQUE,
	StartDate DATE,
	Budget DECIMAL(7, 2) NOT NULL,
	[Status] VARCHAR(25) CONSTRAINT DefaultPendings DEFAULT 'Not Started'
);

INSERT INTO Proj (ProjectName, StartDate, Budget, [Status])
VALUES 
    ('Website Redesign', '2024-01-01',15000.00, 'In Progress'),
    ('Mobile App Development', '2024-02-15',25000.00, 'Not Started')

	SELECT * FROM Proj

ALTER TABLE Proj
ADD [Description] VARCHAR(255) NOT NULL DEFAULT 'proj';
GO
add constraint c_name not null 

add constraint f_key foreign key (id) ref pro(col);