using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CytidelTask.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public IEnumerable<Task> GetAllTasks()
        {
            List<Task> tasks = new List<Task>();
            SqlCommand sqlCommand = new SqlCommand();
            var sqlConnection = new SqlConnection(GetConnectionString());
            DataSet dsDataSet = new DataSet();

            //set up Stored Procedure
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_task_get_all";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            var sqlAdapter = new SqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dsDataSet);

            DataTable dtTask = dsDataSet.Tables[0];

            foreach (DataRow row in dtTask.Rows)
                tasks = new List<Task>(tasks.Append(DataRowToTask(row)));

            return tasks.AsEnumerable<Task>();
        }

        public Task? GetTaskById(int taskId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            var sqlConnection = new SqlConnection(GetConnectionString());
            DataSet dsDataSet = new DataSet();
            SqlParameter sqlParameter;

            //set up Stored Procedure
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_task_get_by_id";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            //Add Parameters
            sqlParameter = sqlCommand.Parameters.Add("@TaskID", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = taskId;

            var sqlAdapter = new SqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dsDataSet);

            DataTable dtTask = dsDataSet.Tables[0];

            if (dtTask.Rows.Count == 0)
                return null;

            else
                return DataRowToTask(dtTask.Rows[0]);
        }

        public void CreateTask(Task task)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlParameter sqlParameter;
            var sqlAdapter = new SqlDataAdapter();
            var sqlConnection = new SqlConnection(GetConnectionString());

            //set up SP
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_task_create";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            //Add Parameters
            sqlParameter = sqlCommand.Parameters.Add("@TaskTitle", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Title;
            sqlParameter = sqlCommand.Parameters.Add("@TaskDescription", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Description;
            sqlParameter = sqlCommand.Parameters.Add("@TaskPriority", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Priority;
            sqlParameter = sqlCommand.Parameters.Add("@TaskStatus", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Status;
            sqlParameter = sqlCommand.Parameters.Add("@TaskDueDate", SqlDbType.DateTime);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.DueDate;

            //Execute SP
            sqlAdapter.SelectCommand = sqlCommand;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateTask(Task task)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlParameter sqlParameter;
            var sqlAdapter = new SqlDataAdapter();
            var sqlConnection = new SqlConnection(GetConnectionString());

            //set up SP
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "pr_task_update";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            //Add Parameters
            sqlParameter = sqlCommand.Parameters.Add("@TaskID", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.TaskId;
            sqlParameter = sqlCommand.Parameters.Add("@TaskTitle", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Title;
            sqlParameter = sqlCommand.Parameters.Add("@TaskDescription", SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Description;
            sqlParameter = sqlCommand.Parameters.Add("@TaskPriority", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Priority;
            sqlParameter = sqlCommand.Parameters.Add("@TaskStatus", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.Status;
            sqlParameter = sqlCommand.Parameters.Add("@TaskDueDate", SqlDbType.DateTime);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = task.DueDate;

            //Execute SP
            sqlAdapter.SelectCommand = sqlCommand;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public Task DataRowToTask(DataRow row)
        {
            var task = new Task((string)row["task_title"], (string)row["task_description"], (Priority)row["task_priority"], (DateTime)row["task_due_date"], (Status)row["task_status"]);
            task.TaskId = (int)row["task_id"];
            return task;
        }

        public string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return configuration.GetConnectionString("DefaultConnection") ?? "";
        }
    }
}
