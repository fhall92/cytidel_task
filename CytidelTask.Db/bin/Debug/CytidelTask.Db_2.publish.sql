﻿/*
Deployment script for cytidel_task

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "cytidel_task"
:setvar DefaultFilePrefix "cytidel_task"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering Procedure [dbo].[pr_task_create]...';


GO
-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Create New Task
-- =============================================
ALTER PROCEDURE [pr_task_create] 
	@TaskTitle varchar(MAX) = '',
	@TaskDescription varchar(MAX) = '',
	@TaskPriority int = 0,
	@TaskStatus int = 0,
	@TaskDueDate datetime
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			SET NOCOUNT ON;

			-- Insert new Task
			INSERT INTO dbo.[task](
				task_title,
				task_description,
				task_priority,
				task_status,
				task_due_date,
				task_created_on)
			VALUES(
				@TaskTitle,
				@TaskDescription,
				@TaskPriority,
				@TaskStatus,
				@TaskDueDate,
				GETDATE())
		END TRY

		BEGIN CATCH
			ROLLBACK TRANSACTION
		END CATCH

		IF @@TRANCOUNT > 0
		BEGIN
			COMMIT TRANSACTION;
		END
END
GO
PRINT N'Altering Procedure [dbo].[pr_task_get_by_id]...';


GO
-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Get Task by task_id
-- =============================================
ALTER PROCEDURE [pr_task_get_by_id] 
	@TaskId int = 0
AS
BEGIN
	SELECT * FROM task 
	WHERE task_id = @TaskId AND task_voided_on = NULL
END
GO
PRINT N'Altering Procedure [dbo].[pr_task_update]...';


GO
-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Update Existing Task
-- =============================================
ALTER PROCEDURE [pr_task_update] 
	@TaskId int = 0,
	@TaskTitle varchar(MAX) = '',
	@TaskDescription varchar(MAX) = '',
	@TaskPriority int = 0,
	@TaskStatus int = 0,
	@TaskDueDate datetime
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			SET NOCOUNT ON;

			-- Insert new Task
			INSERT INTO dbo.[task](
				task_title,
				task_description,
				task_priority,
				task_status,
				task_due_date,
				task_created_on)
			VALUES(
				@TaskTitle,
				@TaskDescription,
				@TaskPriority,
				@TaskStatus,
				@TaskDueDate,
				GETDATE())
		END TRY

		BEGIN CATCH
			ROLLBACK TRANSACTION
		END CATCH

		IF @@TRANCOUNT > 0
		BEGIN
			COMMIT TRANSACTION;
		END
END
GO
PRINT N'Creating Procedure [dbo].[pr_task_get_by_status]...';


GO
-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Get Tasks by Status
-- =============================================
CREATE PROCEDURE [pr_task_get_by_status] 
	@Status int = 0
AS
BEGIN
	SELECT * FROM task 
	WHERE task_status = @Status AND task_voided_on = NULL
END
GO
PRINT N'Update complete.';


GO