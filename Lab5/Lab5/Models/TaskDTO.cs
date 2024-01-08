using System;
using System.Collections.Generic;
using ReactiveUI;

namespace Lab5.Models;

public class TaskDto : ReactiveObject
{
    private Guid _id;
    private string _title;
    private string _description;
    private string _deadline;
    private DateTime _lastModifiedDateTime;
    private string _tags;
    private bool _isCompleted = false;

    public Guid Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

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

    public DateTime LastModifiedDateTime
    {
        get => _lastModifiedDateTime;
        set => this.RaiseAndSetIfChanged(ref _lastModifiedDateTime, value);
    }

    public string Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => this.RaiseAndSetIfChanged( ref _isCompleted, value);
    }

    public TaskDto()
    {
    }

    public TaskDto(Guid id, string title, string description, string deadline, string tags, DateTime lastModifiedDateTime = default, bool isCompleted = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Deadline = deadline;
        LastModifiedDateTime = lastModifiedDateTime;
        Tags = tags;
        IsCompleted = isCompleted;
    }
}