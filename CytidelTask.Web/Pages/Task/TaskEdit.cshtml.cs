using CytidelTask.Data;
using CytidelTask.Data.Repositories;
using CytidelTask.TaskAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

public class TasksEditModel : PageModel
{
	private readonly TasksController _tasksController;
	public List<SelectListItem> PriorityOptions { get; set; }
	public List<SelectListItem> StatusOptions { get; set; }

	public TasksEditModel(TasksController tasksController)
	{
		_tasksController = tasksController;
	}

	public CytidelTask.Data.Task Task { get; private set; }

	public void OnGet()
	{
		if (RouteData.Values.TryGetValue("id", out var idValue) && int.TryParse(idValue.ToString(), out int id))
		{
			var result = _tasksController.GetTask(id);

			var resultValue = ((ObjectResult)result.Result).Value;

			Task = (CytidelTask.Data.Task)resultValue;

			ViewData["Id"] = id;

			PriorityOptions = new List<SelectListItem>
			{
				new SelectListItem { Text = "Low", Value = "Low" },
				new SelectListItem { Text = "Medium", Value = "Medium" },
				new SelectListItem { Text = "High", Value = "High" }
			};

			StatusOptions = new List<SelectListItem>
			{
				new SelectListItem { Text = "Pending", Value = "Pending" },
				new SelectListItem { Text = "InProgress", Value = "InProgress" },
				new SelectListItem { Text = "Completed", Value = "Completed" },
				new SelectListItem { Text = "Archived", Value = "Archived" }
			};
		}
	}

	public IActionResult OnPost()
	{
		if (RouteData.Values.TryGetValue("id", out var idValue) && int.TryParse(idValue.ToString(), out int id))
		{
			// Call Controller method to create the task
			_tasksController.UpdateTask(id, new TaskDto(
			Request.Form["Title"],
			Request.Form["Description"],
			DateTime.Parse(Request.Form["DueDate"]),
			(Priority)Enum.Parse(typeof(Priority), Request.Form["Task.Priority"]),
			(Status)Enum.Parse(typeof(Status), Request.Form["Task.Status"])));
		}
			// Redirect to the same page or another page after adding the task
			return RedirectToPage("/Task/Tasks");
		
	}

}
