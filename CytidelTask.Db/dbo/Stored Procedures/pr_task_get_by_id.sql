-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Get Task by task_id
-- =============================================
CREATE PROCEDURE [pr_task_get_by_id] 
	@TaskId int = 0
AS
BEGIN
	SELECT * FROM task 
	WHERE task_id = @TaskId AND task_voided_on IS NULL
END

