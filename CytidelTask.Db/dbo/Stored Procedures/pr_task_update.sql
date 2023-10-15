-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Update Existing Task
-- =============================================
CREATE PROCEDURE [pr_task_update] 
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

			-- Update Existing Task
			UPDATE dbo.[task] SET
				task_title = @TaskTitle,
				task_description = @TaskDescription,
				task_priority = @TaskPriority,
				task_status = @TaskStatus,
				task_due_date = @TaskDueDate
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

