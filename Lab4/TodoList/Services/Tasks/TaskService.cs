using TodoList.Repositories;
using Task = TodoList.Models.Task;

namespace TodoList.Services.Tasks;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public void CreateTask(Task task) => _taskRepository.SaveTask(task);
    
    public void UpsertTask(Task task)
    {
        if (_taskRepository.GetTask(task.Id) is null)
        {
            _taskRepository.SaveTask(task);
            return;
        }
        _taskRepository.UpdateTask(task);
    }

    public void DeleteTask(Guid id) => _taskRepository.DeleteTask(id);

    public Task? GetTask(Guid id) => _taskRepository.GetTask(id);
    
    public List<Task> GetTasks() => _taskRepository.GetTasks();
}