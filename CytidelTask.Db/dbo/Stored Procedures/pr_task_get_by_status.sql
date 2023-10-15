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
	WHERE task_status = @Status AND task_voided_on IS NULL
END

