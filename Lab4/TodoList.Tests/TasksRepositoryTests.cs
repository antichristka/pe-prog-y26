using TodoList.Repositories;
using Task = TodoList.Models.Task;

namespace TodoList.Tests;

public class TaskRepositoryTests
{
    [Fact]
    public void GetTask_ReturnsTask_WhenTaskExists()
    {
        var taskId = Guid.NewGuid();
        var mockRepository = new Mock<ITaskRepository>();
        mockRepository.Setup(repo => repo.GetTask(It.IsAny<Guid>()))
            .Returns(new Task { Id = taskId, Title = "Test Task" });
        
        var result = mockRepository.Object.GetTask(taskId);
        
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

        var result = mockRepository.Object.GetTask(taskId);

        Assert.Null(result);
    }
}