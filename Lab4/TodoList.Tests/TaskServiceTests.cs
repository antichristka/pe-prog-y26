using TodoList.Repositories;
using TodoList.Services.Tasks;
using Task = TodoList.Models.Task;

namespace TodoList.Tests;

public class TaskServiceTests
{
    [Fact]
    public void GetTask_ReturnsTask_WhenTaskExists()
    {
        var taskId = Guid.NewGuid();
        var mockRepository = new Mock<ITaskRepository>();
        mockRepository.Setup(repo => repo.GetTask(It.IsAny<Guid>()))
            .Returns(new Task { Id = taskId, Title = "Test Task" });

        var taskService = new TaskService(mockRepository.Object);
        
        var result = taskService.GetTask(taskId);
        
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);
        Assert.Equal("Test Task", result.Title);
    }

    [Fact]
    public void GetTask_ReturnsNull_WhenTaskDoesNotExist()
    {
        var taskId = Guid.NewGuid();
        var mockRepository = new Mock<ITaskRepository>();
        mockRepository.Setup(repo => repo.GetTask(It.IsAny<Guid>()))
            .Returns((Task)null);

        var taskService = new TaskService(mockRepository.Object);
        
        var result = taskService.GetTask(taskId);
        
        Assert.Null(result);
    }
}