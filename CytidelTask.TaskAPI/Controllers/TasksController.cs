using Microsoft.AspNetCore.Mvc;
using CytidelTask.Data.Repositories;
using CytidelTask.Data;


namespace CytidelTask.TaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository = new TaskRepository();
		private readonly IConfiguration _configuration;
		private readonly TaskLogger _taskLogger;

		public TasksController(IConfiguration configuration, TaskLogger taskLogger)
		{
			_configuration = configuration;
			_taskLogger = taskLogger;
		}

		// GET /api/tasks/GetAll
		[HttpGet("GetAll")]
        public ActionResult<IEnumerable<Data.Task>> GetTasks()
        {
            _taskLogger.Log($"GetTasks  /api/tasks/GetAll", false);
            return Ok(_taskRepository.GetAllTasks());
        }

        // GET /api/tasks/{id}
        [HttpGet("{id}/GetTask")]
        public ActionResult<Data.Task> GetTask(int id)
        {
            if (_taskRepository.GetTaskById(id) == null)
            {
				_taskLogger.Log($"GetTask(int id) GET /api/tasks/GetTask    404: Task {id} Not Found.", false);
				return NotFound();
            }

			_taskLogger.Log($"GetTask(int id) GET /api/tasks/GetTask    200: Task {id}", false);
			return Ok(_taskRepository.GetTaskById(id));
        }

        // POST /api/tasks
        [HttpPost("Create")]
        public ActionResult<Data.Task> CreateTask(TaskDto taskDto)
        {

            _taskRepository.CreateTask(taskDto.ToTask(0));

            if (taskDto.Priority == Priority.High)
                _taskLogger.Log($"CreateTask(TaskDto) POST /api/tasks/Create    200: Critical Task Created: [{taskDto.Title}]", true);

			_taskLogger.Log($"CreateTask(TaskDto) POST /api/tasks/Create    200: Critical Task Created: [{taskDto.Title}]", false);

			return Ok(taskDto);
        }

        // PUT /api/tasks/{id}/Update
        [HttpPut("{id}/Update")]
        public IActionResult UpdateTask(int id, TaskDto taskDto)
        {
            var existingTask = _taskRepository.GetTaskById(id);

            if (existingTask == null)
            {
				_taskLogger.Log($"UpdateTask(int id, TaskDto) PUT /api/tasks/(id)/Update    404: Task {id} Not Found.", false);
				return NotFound($"Task {id} Not Found");
            }

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Priority = taskDto.Priority;
            existingTask.DueDate = taskDto.DueDate;
            existingTask.Status = taskDto.Status;

            _taskRepository.UpdateTask(existingTask);

			if (existingTask.Priority == Priority.High)
				_taskLogger.Log($"UpdateTask(int id, TaskDto) PUT /api/tasks/(id)/Update    200: Critical Task Created: [{existingTask.TaskId}]", true);

			_taskLogger.Log($"UpdateTask(int id, TaskDto) PUT /api/tasks/(id)/Update    200: Critical Task Created: [{existingTask.TaskId}]", false);

			return Ok($"Task {id} updated.");
        }
    }

    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        
        public TaskDto(string title, string description, DateTime dueDate, Priority priority, Status status)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Status = status;
        }

        public Data.Task ToTask(int id)
        {
            var task = new Data.Task(Title, Description, Priority, DueDate, Status);
            task.TaskId = id;
            return task;
        }
    }
}