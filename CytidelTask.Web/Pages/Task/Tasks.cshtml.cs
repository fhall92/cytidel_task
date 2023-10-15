using CytidelTask.Data;
using CytidelTask.TaskAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task = CytidelTask.Data.Task;

public class TasksModel : PageModel
{
    private readonly TasksController _taskController;

    public TasksModel(TasksController tasksController)
    {
        _taskController = tasksController;
    }

    public IEnumerable<Task> Tasks { get; private set; }
    public Dictionary<string, int> StatusCounts { get; private set; }
    public int LoadedTasksCount { get; private set; } = 10;

    public void OnGet(string status, string priority)
    {
        var result = _taskController.GetTasks();

        var resultValue = ((ObjectResult)result.Result).Value;

        Tasks = (IEnumerable<Task>)resultValue ?? new List<Task>();

        StatusCounts = Tasks.GroupBy(task => task.Status.ToString()).ToDictionary(group => group.Key, group => group.Count());

        if (!string.IsNullOrEmpty(status))
        {
            Tasks = Tasks.Where(t => t.Status == (Status)Enum.Parse(typeof(Status), status));
        }

        if (!string.IsNullOrEmpty(priority))
        {
            Tasks = Tasks.Where(t => t.Priority == (Priority)Enum.Parse(typeof(Priority), priority));
        }
    }
}
