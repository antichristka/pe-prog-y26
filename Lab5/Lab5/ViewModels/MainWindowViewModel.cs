using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicData;
using Lab5.Models;
using Lab5.Views;
using ReactiveUI;

namespace Lab5.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private ObservableCollection<TaskDto> _tasks;
    private TaskDto _selectedTask;

    public ObservableCollection<TaskDto> Tasks
    {
        get => _tasks;
        set => this.RaiseAndSetIfChanged(ref _tasks, value);
    }

    public TaskDto SelectedTask
    {
        get => _selectedTask;
        set => this.RaiseAndSetIfChanged( ref _selectedTask, value);
    }
    
    public ReactiveCommand<Unit, Unit> EditTaskCommand { get; }
    public ReactiveCommand<TaskDto, Unit> DeleteTaskCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowAddTaskCommand { get; }

    private AddTaskViewModel _addTaskViewModel;

    public AddTaskViewModel AddTaskViewModel
    {
        get => _addTaskViewModel;
        set => this.RaiseAndSetIfChanged(ref _addTaskViewModel, value);
    }

    public MainWindowViewModel()
    {
        using (var dbContext = new TaskDbContext())
        {
            dbContext.Database.EnsureCreated();
        }
        
        Tasks = new ObservableCollection<TaskDto>();

        // Commands
        EditTaskCommand = ReactiveCommand.Create(EditTask);
        DeleteTaskCommand = ReactiveCommand.Create<TaskDto>(DeleteTask);
        ShowAddTaskCommand = ReactiveCommand.Create(ShowAddTaskDialog);

        LoadTasks();
    }


    private void LoadTasks()
    {
        using var context = new TaskDbContext();
        Tasks.AddRange(context.Tasks);
    }

    private void SaveTask(TaskDto task)
    {
        using var context = new TaskDbContext();
        var existingTask = context.Tasks.Find(task.Id);
    
        if (existingTask == null) context.Tasks.Add(task);
        else context.Entry(existingTask).CurrentValues.SetValues(task);
    
        context.SaveChanges();
    }

    private void ShowAddTaskDialog()
    {
        var addTaskViewModel = new AddTaskViewModel(task =>
        {
            Tasks.Add(task);
            SelectedTask = task;
            AddTaskViewModel = null;
            
            SaveTask(task);
        });
         var addTaskView = new AddTaskView { DataContext = addTaskViewModel };

         if (Avalonia.Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime
             desktopLifetime) return;
         
         var mainWindow = desktopLifetime.MainWindow;

         addTaskView.ShowDialog(mainWindow);
    }

    private void EditTask()
    {
        if (SelectedTask == null) return;
        
        var editTaskViewModel = new AddTaskViewModel(SelectedTask, task =>
        {
            SelectedTask.Title = task.Title;
            SelectedTask.Description = task.Description;
            SelectedTask.Deadline = task.Deadline;
            SelectedTask.Tags = task.Tags;
            
            SaveTask(SelectedTask);
            this.RaisePropertyChanged(nameof(SelectedTask));
        });

        var editTaskView = new AddTaskView { DataContext = editTaskViewModel };

        if (Avalonia.Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktopLifetime)
            return;

        var mainWindow = desktopLifetime.MainWindow;
        editTaskView.ShowDialog(mainWindow);
    }


    private void DeleteTask(TaskDto task)
    {
        using var context = new TaskDbContext();
        var existingTask = context.Tasks.Find(task.Id);

        if (existingTask == null) return;
        
        context.Tasks.Remove(existingTask);
        context.SaveChanges();
        Tasks.Remove(task);
        SelectedTask = Tasks.FirstOrDefault();
    }
}