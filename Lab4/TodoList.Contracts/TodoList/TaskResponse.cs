namespace TodoList.Contracts.TodoList;

public record TaskResponse
(
    Guid Id,
    string Title,
    string Description,
    DateTime Deadline,
    DateTime LastModifiedDateTime,
    List<string> Tags);