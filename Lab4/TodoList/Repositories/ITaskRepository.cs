namespace TodoList.Repositories;

using Task = TodoList.Models.Task;

public interface ITaskRepository
{
    Task? GetTask(Guid id);
    List<Task> GetTasks();
    void SaveTask(Task task);
    void UpdateTask(Task task);
    void DeleteTask(Guid id);
}