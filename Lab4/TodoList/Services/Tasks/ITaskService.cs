using TodoList.Contracts.TodoList;
using Task = TodoList.Models.Task;

namespace TodoList.Services.Tasks;

public interface ITaskService
{
    void CreateTask(Task task);
    void UpsertTask(Task task);
    void DeleteTask(Guid id);
    Task? GetTask(Guid id);
    List<Task> GetTasks();
}