-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Create New Task
-- =============================================
CREATE PROCEDURE [pr_task_create] 
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

