using CytidelTask.Data;
using CytidelTask.TaskAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Task = CytidelTask.Data.Task;

public class TasksCreateModel : PageModel
{
    private readonly TasksController _tasksController;

    public TasksCreateModel(TasksController tasksController)
    {
        _tasksController = tasksController;
    }

    public IEnumerable<Task> Tasks { get; private set; }
    public Dictionary<string, int> StatusCounts { get; private set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        _tasksController.CreateTask(new TaskDto(
            Request.Form["Title"],
            Request.Form["Description"],
            DateTime.Parse(Request.Form["DueDate"]),
            (Priority)Enum.Parse(typeof(Priority), Request.Form["Priority"]),
            (Status)Enum.Parse(typeof(Status), Request.Form["Status"])));

        return RedirectToPage("/Task/Tasks");
    }
}
