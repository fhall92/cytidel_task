CREATE TABLE [dbo].[task_log] (
    [task_log_id]          INT           IDENTITY (1, 1) NOT NULL,
    [task_log_task_id]     INT           NOT NULL,
    [task_log_description] VARCHAR (MAX) NOT NULL,
    [task_log_created_on]  DATETIME      NOT NULL,
    CONSTRAINT [PK_task_log] PRIMARY KEY CLUSTERED ([task_log_id] ASC),
    CONSTRAINT [FK_task_log_task_log] FOREIGN KEY ([task_log_id]) REFERENCES [dbo].[task_log] ([task_log_id])
);

