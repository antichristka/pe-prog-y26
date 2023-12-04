using Xunit;
namespace Lab3.Tests;

public class Test : IDisposable
{
    private const string JsonFilePath = "C:\\Users\\kater\\source\\repos\\Lab3\\Lab3\\json\\todoList.json";
    private const string XmlFilePath = "C:\\Users\\kater\\source\\repos\\Lab3\\Lab3\\xml\\todoList.xml";
    private const string SQLitePath = "C:\\Users\\kater\\source\\repos\\Lab3\\Lab3\\sqlite\\todoList.db";

    [Fact]
    public void SaveAndLoadJson()
    {
        // Arrange
        TodoList todoList = new TodoList();
        todoList.Add(new Task("Task1", "Description1", DateTime.Now.AddDays(1), new List<string> { "Tag1" }));

        // Act
        todoList.Save(JsonFilePath);
        TodoList loadedTodoList = new TodoList();
        loadedTodoList.Load(JsonFilePath);

        // Assert
        Assert.Equal(todoList.Tasks.Count(), loadedTodoList.Tasks.Count());
    }

    [Fact]
    public void SaveAndLoadXml()
    {
        // Arrange
        TodoList todoList = new TodoList();
        todoList.Add(new Task("Task1", "Description1", DateTime.Now.AddDays(1), new List<string> { "Tag1" }));
        todoList.Add(new Task("Task2", "Description2", DateTime.Now.AddDays(2), new List<string> { "Tag2" }));
        
        Assert.Equal(2, todoList.Tasks.Count);
        
        // Act
        todoList.SaveToXml(XmlFilePath);
        todoList.Add(new Task("Task3", "Description3", DateTime.Now.AddDays(3), new List<string> { "Tag3" }));
        
        todoList.LoadFromXml(XmlFilePath);

        // Assert
        Assert.Equal(2, todoList.Tasks.Count);
    }

    [Fact]
    public void SaveAndLoadSqLite()
    {
        // Arrange
        TodoList todoList = new TodoList();
        todoList.Add(new Task("Task1", "Description1", DateTime.Now.AddDays(1), new List<string> { "Tag1" }));
        todoList.Add(new Task("Task2", "Description2", DateTime.Now.AddDays(2), new List<string> { "Tag2" }));
        
        Assert.Equal(2, todoList.Tasks.Count);
        
        // Act
        todoList.SaveToSqLite(SQLitePath);
        todoList.Add(new Task("Task3", "Description3", DateTime.Now.AddDays(3), new List<string> { "Tag3" }));
        todoList.LoadFromSqLite(SQLitePath);

        // Assert
        Assert.Equal(2, todoList.Tasks.Count);

    }

    public void Dispose()
    {
        if (File.Exists(JsonFilePath))
            File.Delete(JsonFilePath);
    
        if (File.Exists(XmlFilePath))
            File.Delete(XmlFilePath);
    
        if (File.Exists(SQLitePath))
            File.Delete(SQLitePath);
    }
}