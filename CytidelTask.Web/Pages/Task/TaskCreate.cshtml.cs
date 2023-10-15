using CytidelTask.Data;
using CytidelTask.TaskAPI.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
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
		// Call Controller method to create the task
		_tasksController.CreateTask(new TaskDto(
			Request.Form["Title"],
			Request.Form["Description"],
			DateTime.Parse(Request.Form["DueDate"]),
			(Priority)Enum.Parse(typeof(Priority), Request.Form["Priority"]),
			(Status)Enum.Parse(typeof(Status), Request.Form["Status"])));

		// Redirect to the same page or another page after adding the task
		return RedirectToPage("/Task/Tasks");
	}
}
