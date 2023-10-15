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

