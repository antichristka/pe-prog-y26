namespace TodoList.Contracts.TodoList;

public record CreateTaskRequest
(
    string Title,
    string Description,
    DateTime Deadline,
    List<string> Tags);