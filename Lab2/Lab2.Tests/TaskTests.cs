using Xunit;
using Lab2;

namespace Lab2.Tests
{
    public class TaskTests
    {
        [Fact]
        public void AddTag_ShouldAddNewTag_WhenTagDoesNotExist()
        {
            Task task = new Task("Title", "Description", DateTime.Now, new List<string>());

            task.AddTag("Tag1");

            Assert.Contains("Tag1", task.Tags);
        }

        [Fact]
        public void AddTag_ShouldNotAddTag_WhenTagAlreadyExists()
        {
            Task task = new Task("Title", "Description", DateTime.Now, new List<string> { "Tag1" });

            task.AddTag("Tag1");

            Assert.Single(task.Tags);
        }

        [Fact]
        public void AddTag_ShouldNotAddTag_WhenTagIsNullOrWhiteSpace()
        {
            Task task = new Task("Title", "Description", DateTime.Now, new List<string>());

            task.AddTag("");

            Assert.Empty(task.Tags);
        }
    }

    public class TodoListTests
    {
        [Fact]
        public void Add_ShouldAddTask_WhenTaskDoesNotExist()
        {
            TodoList todoList = new TodoList();
            Task task = new Task("Title", "Description", DateTime.Now, new List<string>());

            todoList.Add(task);

            Assert.Contains(task, todoList.Tasks);
        }

        [Fact]
        public void Add_ShouldNotAddTask_WhenTaskAlreadyExists()
        {
            TodoList todoList = new TodoList();
            Task task = new Task("Title", "Description", DateTime.Now, new List<string>());
            todoList.Add(task);

            todoList.Add(task);

            Assert.Single(todoList.Tasks);
        }

        [Fact]
        public void Remove_ShouldRemoveTask_WhenTaskExists()
        {
            TodoList todoList = new TodoList();
            Task task = new Task("Title", "Description", DateTime.Now, new List<string>());
            todoList.Add(task);

            todoList.Remove(task);

            Assert.DoesNotContain(task, todoList.Tasks);
        }

        [Fact]
        public void Remove_ShouldNotRemoveTask_WhenTaskDoesNotExist()
        {
            TodoList todoList = new TodoList();
            Task task = new Task("Title", "Description", DateTime.Now, new List<string>());

            todoList.Remove(task);

            Assert.Empty(todoList.Tasks);
        }

        [Fact]
        public void Search_ShouldReturnMatchingTasks_WhenTagsExist()
        {
            TodoList todoList = new TodoList();
            Task task1 = new Task("Task1", "Description1", DateTime.Now, new List<string> { "Tag1", "Tag2" });
            Task task2 = new Task("Task2", "Description2", DateTime.Now, new List<string> { "Tag2", "Tag3" });
            todoList.Add(task1);
            todoList.Add(task2);

            var result = todoList.Search(new string[] { "Tag1", "Tag3" });

            Assert.Collection(result,
                item1 => Assert.Equal("Task1", item1.Title),
                item2 => Assert.Equal("Task2", item2.Title)
            );
        }
    }
}
