using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;
namespace TodoList.Repositories;
using Task = TodoList.Models.Task;

public class TaskRepository : ITaskRepository
{
    private readonly string _connectionString;

    public TaskRepository(string databasePath= "C:\\Users\\kater\\source\\repos\\Lab4\\TodoList\\Data\\TodoList.db")
    {
        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = databasePath
        }.ConnectionString;
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        SqlMapper.AddTypeHandler(new StringListTypeHandler());
        CreateDatabaseIfNotExists();
    }


    public void SaveTask(Task task)
    {
        using var dbConnection = new SqliteConnection(_connectionString);
        dbConnection.Open();
        dbConnection.Execute(
            "INSERT INTO Tasks (Id, Title, Description, Deadline, LastModifiedDateTime, Tags) VALUES (@Id, @Title, @Description, @Deadline, @LastModifiedDateTime, @Tags)",
            task);
    }
    
    public void UpdateTask(Task task)
    {
        using var dbConnection = new SqliteConnection(_connectionString);
        dbConnection.Open();
        dbConnection.Execute(
            "UPDATE Tasks SET Title = @Title, Description = @Description, Deadline = @Deadline, LastModifiedDateTime = @LastModifiedDateTime, Tags = @Tags WHERE Id = @Id",
            task);
    }
    
    public void DeleteTask(Guid id)
    {
        using var dbConnection = new SqliteConnection(_connectionString);
        dbConnection.Open();
        dbConnection.Execute(
            "DELETE FROM Tasks WHERE Id = @Id",
            new { Id = id });
    }
    
    public Task? GetTask(Guid id)
    {
        using var dbConnection = new SqliteConnection(_connectionString);
        dbConnection.Open();
        return dbConnection.QueryFirstOrDefault<Task>(
            "SELECT * FROM Tasks WHERE Id = @Id",
            new { Id = id });
    }
    
    public List<Task> GetTasks()
    {
        using var dbConnection = new SqliteConnection(_connectionString);
        dbConnection.Open();

        var tasks = dbConnection.Query<Task>("SELECT * FROM Tasks").ToList();

        return tasks;
    }
    
    private void CreateDatabaseIfNotExists()
    {
        using var dbConnection = new SqliteConnection(_connectionString);
        dbConnection.Open();
        dbConnection.Execute(
            @"CREATE TABLE IF NOT EXISTS Tasks (
                Id TEXT PRIMARY KEY,
                Title TEXT NOT NULL,
                Description TEXT,
                Deadline TEXT NOT NULL,
                LastModifiedDateTime TEXT NOT NULL,
                Tags TEXT NOT NULL
            )");
    }
}

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        return Guid.Parse(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value.ToString();
    }
}

public class StringListTypeHandler : SqlMapper.TypeHandler<List<string>>
{
    public override List<string> Parse(object value)
    {
        if (value == null || value is DBNull)
            return null;

        // Assuming that the tags are stored as a comma-separated string
        return ((string)value).Split(',').ToList();
    }

    public override void SetValue(IDbDataParameter parameter, List<string> value)
    {
        parameter.Value = string.Join(",", value);
    }
}
