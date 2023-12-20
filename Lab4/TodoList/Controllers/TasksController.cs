using Microsoft.AspNetCore.Mvc;
using TodoList.Contracts.TodoList;
using TodoList.Services.Tasks;
using Task = TodoList.Models.Task;

namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public IActionResult CreateTask(CreateTaskRequest request)
    {
        var task = new Task(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Deadline,
            DateTime.UtcNow,
            request.Tags);
    
        _taskService.CreateTask(task);
    
        var response = new TaskResponse(
            task.Id,
            task.Title,
            task.Description,
            task.Deadline,
            task.LastModifiedDateTime,
            task.Tags);
        
        return CreatedAtAction(
            actionName: nameof(GetTask),
            routeValues: new {id = task.Id },
            value: response);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetTask(Guid id)
    {
        var task = _taskService.GetTask(id);
        if (task is null) return NotFound();
    
        var response = new TaskResponse(
            task.Id,
            task.Title,
            task.Description,
            task.Deadline,
            task.LastModifiedDateTime,
            task.Tags);
        
        return Ok(response);
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult PutTask(Guid id, UpsertTaskRequest request)
    {
        var task = _taskService.GetTask(id);
        if (task is null) return NotFound();
    
        var updatedTask = new Task(
            task.Id,
            request.Title,
            request.Description,
            request.Deadline,
            DateTime.UtcNow,
            request.Tags);
    
        _taskService.UpsertTask(updatedTask);
    
        var response = new TaskResponse(
            updatedTask.Id,
            updatedTask.Title,
            updatedTask.Description,
            updatedTask.Deadline,
            updatedTask.LastModifiedDateTime,
            updatedTask.Tags);
        
        return Ok(response);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTask(Guid id)
    {
        var task = _taskService.GetTask(id);
        if (task is null) return NotFound();
    
        _taskService.DeleteTask(id);
    
        return NoContent();
    }
    
    [HttpGet]
    public IActionResult GetTasks()
    {
        var tasks = _taskService.GetTasks();
    
        var response = tasks.Select(task => new TaskResponse(
            task.Id,
            task.Title,
            task.Description,
            task.Deadline,
            task.LastModifiedDateTime,
            task.Tags));
        
        return Ok(response);
    }
}