using ReactiveUI;
using System;
using System.Reactive;
using Lab5.Models;

public class AddTaskViewModel : ReactiveObject
{
    private string _title;
    private string _description;
    private string _deadline;
    private string _tags;

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public string Deadline
    {
        get => _deadline;
        set => this.RaiseAndSetIfChanged(ref _deadline, value);
    }

    public string Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }

    public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }
    
    public AddTaskViewModel(Action<TaskDto> onTaskAdded)
    {
        AddTaskCommand = ReactiveCommand.Create(() =>
        {
            var newTask = new TaskDto(Guid.NewGuid(), Title, Description, Deadline, Tags);
            onTaskAdded?.Invoke(newTask);
        });
    }

    public AddTaskViewModel(TaskDto existingTask, Action<TaskDto> onTaskAdded)
    {
        // Populate the properties with existing task details
        Title = existingTask.Title;
        Description = existingTask.Description;
        Deadline = existingTask.Deadline;
        Tags = existingTask.Tags;

        AddTaskCommand = ReactiveCommand.Create(() =>
        {
            var editedTask = new TaskDto(existingTask.Id, Title, Description, Deadline, Tags);
            onTaskAdded?.Invoke(editedTask);
        });
    }
}
