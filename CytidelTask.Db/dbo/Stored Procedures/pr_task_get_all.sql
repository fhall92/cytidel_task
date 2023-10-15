-- =============================================
-- Author:		Fran Hall
-- Create date: 11/10/2023
-- Description:	Get All Tasks
-- =============================================
CREATE PROCEDURE [pr_task_get_all] 
AS
BEGIN
	SELECT * FROM task WHERE task_voided_on IS NULL
END
