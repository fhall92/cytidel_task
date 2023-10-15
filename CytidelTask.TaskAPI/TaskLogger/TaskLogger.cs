using Microsoft.Extensions.Configuration;

public class TaskLogger
{
	private readonly IConfiguration _configuration;

	public TaskLogger(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Log(string message, bool isCritical)
	{
		string logPath = isCritical ? _configuration["CriticalTaskLogPath"] : _configuration["TaskLogPath"];

		using (StreamWriter w = File.AppendText(logPath))
			w.WriteLine($"{DateTime.UtcNow}: {message}");
	}
}