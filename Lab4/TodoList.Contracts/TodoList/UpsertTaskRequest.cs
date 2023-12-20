namespace TodoList.Contracts.TodoList;

public record UpsertTaskRequest
(
    string Title,
    string Description,
    DateTime Deadline,
    List<string> Tags);