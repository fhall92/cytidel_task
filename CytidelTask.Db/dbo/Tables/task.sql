CREATE TABLE [dbo].[task] (
    [task_id]          INT           IDENTITY (1, 1) NOT NULL,
    [task_title]       VARCHAR (MAX) NOT NULL,
    [task_description] VARCHAR (MAX) NOT NULL,
    [task_priority]    INT           NOT NULL,
    [task_status]      INT           NOT NULL,
    [task_due_date]    DATETIME      NOT NULL,
    [task_created_on]  DATETIME      NOT NULL,
    [task_voided_on]   DATETIME      NULL,
    CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED ([task_id] ASC)
);

