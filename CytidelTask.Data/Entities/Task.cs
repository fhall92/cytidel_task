namespace CytidelTask.Data
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }

        public Task(string title, string description, Priority priority, DateTime dueDate, Status status)
        {
            this.TaskId = 0;
            this.Title = title;
            this.Description = description;
            this.Priority = priority;
            this.DueDate = dueDate;
            this.Status = status;
        }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed,
        Archived
    }
}