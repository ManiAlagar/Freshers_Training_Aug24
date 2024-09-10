    USE demo
	ALTER TABLE Project ADD  [Description] VARCHAR(255) DEFAULT '' NOT NULL;
	GO 
	EXEC SP_RENAME 'Project.[Description]', 'ProjectDescription','COLUMN';
	GO
	ALTER TABLE Project ALTER COLUMN ProjectDescription varchar(255) NULL;
	GO
	select * from project