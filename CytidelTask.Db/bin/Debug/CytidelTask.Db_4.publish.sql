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
PRINT N'Creating Procedure [dbo].[pr_task_delete]...';


GO
-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Delete Existing Task
-- =============================================
CREATE PROCEDURE [pr_task_delete] 
	@TaskId int = 0
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			SET NOCOUNT ON;

			-- Delete Existing Task
			UPDATE dbo.[task] SET
				task_voided_on = GETDATE()
			WHERE task_id = @TaskId

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
PRINT N'Update complete.';


GO
