using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            TodoList todoList = new TodoList();
            Task testTask = new Task("a", "adf", DateTime.Parse("11/11/1111"), new List<string>());
            todoList.Add(testTask);
            todoList.Add(testTask);

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Search task");
                Console.WriteLine("3. Last tasks");
                Console.WriteLine("4. Exit");
                Console.Write("> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddTask(todoList);
                        break;
                    case "2":
                        Console.Write("Enter tags to search (separated by commas): ");
                        string tagsInput = Console.ReadLine();
                        string[] tags = tagsInput.Split(',').Select(tag => tag.Trim()).ToArray();
                        IEnumerable<Task> tasks = todoList.Search(tags);

                        if (tasks.Count() == 0)
                        {
                            Console.WriteLine("No such tasks.");
                        }
                        
                        int counter = 1;
                        foreach (Task task in tasks)
                        {
                            if (!task.IsComplete)
                            {
                                Console.WriteLine("(" + counter + ") " + task.Title);
                                counter++;
                            }
                        }
                        break;
                    case "3":
                        todoList.DisplayTasks();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        static void AddTask(TodoList todoList)
        {
            Console.Write("Enter the Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter the Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter the Deadline (DD/MM/YYYY): ");
            string deadlineInput = Console.ReadLine();
            DateTime deadline;
            if (!DateTime.TryParse(deadlineInput, out deadline))
            {
                Console.WriteLine("Invalid date format");
                return;
            }
            Console.Write("Enter tags (separated by commas): ");
            string tagsInput = Console.ReadLine();
            List<string> tags = tagsInput.Split(',').Select(tag => tag.Trim()).ToList();

            Task task = new Task(title, description, deadline, tags);
            todoList.Add(task);
            Console.WriteLine("Task added successfully");
        }
    }
}
